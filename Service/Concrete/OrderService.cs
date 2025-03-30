using AutoMapper;
using Domain.Dto.Create;
using Domain.Dto.View;
using Domain.Entity;
using Domain.Enum;
using FluentValidation;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderProductMappingRepository orderProductMappingRepository;
        private readonly IUserCouponMappingRepository userCouponMappingRepository;
        private readonly AbstractValidator<Order> orderValidator;
        private readonly AbstractValidator<CreateOrderDto> createOrderValidator;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IOrderProductMappingRepository orderProductMappingRepository,
            AbstractValidator<Order> orderValidator,
            AbstractValidator<CreateOrderDto> createOrderValidator,
            IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderProductMappingRepository = orderProductMappingRepository;
            this.orderValidator = orderValidator;
            this.createOrderValidator = createOrderValidator;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int id)
        {
            return (await orderRepository.GetOrdersByUserIdAsync(id))
                .Select(_mapper.Map<OrderDto>)
                .ToList();
        }

        public async Task<OrderDto> PlaceOrderAsync(CreateOrderDto placeOrderDto)
        {
            var validation = createOrderValidator.Validate(placeOrderDto);
            if (!validation.IsValid)
            {
                throw new Exception(validation.ToString());
            }

            var productIds = placeOrderDto.ProductIdQuantity.Keys;
            var includedProducts = (await productRepository.GetProductsAsync())
                .Where(x => productIds.Contains(x.Id))
                .ToList();
            var totalPrice = includedProducts
                .Sum(x => x.Price * placeOrderDto.ProductIdQuantity[x.Id]);

            foreach (var product in includedProducts)
            {
                if (product.Stock < placeOrderDto.ProductIdQuantity[product.Id])
                {
                    throw new Exception("Stock is not enough.");
                }
                else
                {
                    product.Stock -= placeOrderDto.ProductIdQuantity[product.Id];
                }
            }

            await productRepository.UpdateProductsAsync(includedProducts);

            var order = new Order
            {
                UserId = placeOrderDto.UserId,
                TotalPrice = totalPrice,
                Deduction = 0,
                Status = OrderStatus.InProgress,
                CreatedAt = DateTime.Now
            };

            var orderValidation = orderValidator.Validate(order);

            if (!orderValidation.IsValid)
            {
                throw new Exception(orderValidation.ToString());
            }

            var orderAdded = await orderRepository.CreateOrderAsync(order);

            var orderProductMappings = productIds
                .Select(x => new OrderProductMapping
                {
                    OrderId = orderAdded.Id,
                    ProductId = x,
                    Quantity = placeOrderDto.ProductIdQuantity[x]
                })
                .ToList();
            await orderProductMappingRepository.CreateOrderProductMappingsAsync(orderProductMappings);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task ConfirmOrderAsync(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            order.Status = OrderStatus.Confirmed;
            await orderRepository.UpdateOrderAsync(order);
        }

        public async Task CancelOrderAsync(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            var productIds = order
                .OrderProductMappings
                .Select(x => x.ProductId)
                .ToList();
            var products = (await productRepository.GetProductsAsync())
                .Where(x => productIds.Contains(x.Id))
                .ToList();

            order.Status = OrderStatus.Cancelled;
            await orderRepository.UpdateOrderAsync(order);

            foreach (var product in products)
            {
                product.Stock += order
                    .OrderProductMappings
                    .First(x => x.ProductId == product.Id)
                    .Quantity;
            }
            await productRepository.UpdateProductsAsync(products);

            var userCouponMappings = await userCouponMappingRepository.GetUserCouponMappingsByUserIdAsync(order.UserId);
            var userCouponMappingsToUpdate = new List<UserCouponMapping>();

            foreach (var orderCouponMapping in order.OrderCouponMappings)
            {
                if (orderCouponMapping.Coupon.Availability == CouponAvailability.Private)
                {
                    var userCouponMapping = userCouponMappings
                        .First(x => x.UserId == order.UserId && x.CouponId == orderCouponMapping.CouponId);
                    userCouponMapping.IsUsed = false;
                    userCouponMapping.ReedemedAt = null;
                    userCouponMappingsToUpdate.Add(userCouponMapping);
                }
            }

            await userCouponMappingRepository.UpdateUserCouponMappingsAsync(userCouponMappingsToUpdate);
        }

        Task<List<OrderDto>> IOrderService.GetOrdersByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
