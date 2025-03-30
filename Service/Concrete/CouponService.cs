using AutoMapper;
using Domain.Dto;
using Domain.Dto.Create;
using Domain.Dto.View;
using Domain.Entity;
using Domain.Enum;
using FluentValidation;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository couponRepository;
        private readonly IUserCouponMappingRepository userCouponMappingRepository;
        private readonly IOrderCouponMappingRepository orderCouponMappingRepository;
        private readonly IOrderRepository orderRepository;
        private readonly AbstractValidator<Coupon> couponValidator;
        private readonly IMapper _mapper;


        public CouponService(
            ICouponRepository couponRepository, 
            IUserCouponMappingRepository userCouponMappingRepository,
            IOrderCouponMappingRepository orderCouponMappingRepository,
            IOrderRepository orderRepository,
            AbstractValidator<Coupon> couponValidator,
            IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.userCouponMappingRepository = userCouponMappingRepository;
            this.orderCouponMappingRepository = orderCouponMappingRepository;
            this.orderRepository = orderRepository;
            this.couponValidator = couponValidator;
            _mapper = mapper;
        }

        public async Task<List<CouponDto>> GetAllCouponsAsync()
        {
            return (await couponRepository
                .GetCouponsAsync())
                .Select(_mapper.Map<CouponDto>)
                .ToList();
        }

        public async Task<List<CouponDto>> GetCouponsByUserIdAsync(int userId)
        {
            return (await userCouponMappingRepository
                .GetUserCouponMappingsByUserIdAsync(userId))
                .Select(_mapper.Map<CouponDto>)
                .ToList();
        }

        public async Task AddCouponToOrderAsync(AddCouponDto couponDto)
        {
            var order = await orderRepository.GetOrderByIdAsync(couponDto.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            var coupon = await couponRepository.GetCouponByCodeAsync(couponDto.CouponCode);
            if (coupon == null)
            {
                throw new Exception("Coupon not found.");
            }

            var ordercouponMapping = await orderCouponMappingRepository.GetOrderCouponMappingByOrderIdAndCouponIdAsync(couponDto.OrderId, coupon.Id);
            if (ordercouponMapping != null)
            {
                throw new Exception("Coupon already added to order.");
            }

            if (order.OrderCouponMappings.Count >= 3 || order.Deduction >= order.TotalPrice / 2)
            {
                throw new Exception("An order can have a maximum of 3 coupons applied and a deduction of maximum 50%.");
            }

            float priceDeduction = 0;

            if (coupon.Type == CouponType.Rate && coupon.DiscountRate != null)
            {
                priceDeduction = (float)(order.TotalPrice * (coupon.DiscountRate / 100));
            }
            else if (coupon.Type == CouponType.Amount && coupon.DiscountAmount != null)
            {
                priceDeduction = (float)coupon.DiscountAmount;
            }

            if (order.Deduction + priceDeduction >= order.TotalPrice / 2)
            {
                throw new Exception("An order can have a maximum deduction of 50%. You can still choose another coupon to reedem.");
            }

            if (coupon.Availability == CouponAvailability.Public && coupon.MaxUsageLimit != null)
            {
                if (coupon.UsageCount >= coupon.MaxUsageLimit)
                {
                    throw new Exception("Coupon is not available anymore.");
                }

                coupon.UsageCount++;
                
                await couponRepository.UpdateCouponAsync(coupon);
            }
            else if (coupon.Availability == CouponAvailability.Private)
            {
                var userCouponMapping = await userCouponMappingRepository.GetUserCouponMappingByUserIdAndCouponIdAsync(order.UserId, coupon.Id);

                if (userCouponMapping == null)
                {
                    throw new Exception("Coupon is not available for this user.");
                }

                if (userCouponMapping.IsUsed)
                {
                    throw new Exception("Coupon is already used.");
                }

                userCouponMapping.IsUsed = true;
                userCouponMapping.ReedemedAt = DateTime.Now;

                await userCouponMappingRepository.UpdateUserCouponMappingAsync(userCouponMapping);
            }
            var orderCouponMapping = new OrderCouponMapping
            {
                OrderId = couponDto.OrderId,
                CouponId = coupon.Id
            };
            await orderCouponMappingRepository.CreateOrderCouponMappingAsync(orderCouponMapping);

            order.Deduction += priceDeduction;
            await orderRepository.UpdateOrderAsync(order);
        }

        public async Task CreateCouponAsync(CreateCouponDto couponDto)
        {
            var coupon = new Coupon
            {
                Title = couponDto.Title,
                Description = couponDto.Description,
                Code = couponDto.Code,
                Type = couponDto.Type,
                DiscountRate = couponDto.DiscountRate,
                DiscountAmount = couponDto.DiscountAmount,
                Availability = couponDto.Availability,
                MaxUsageLimit = couponDto.MaxUsageLimit,
                UsageCount = couponDto.MaxUsageLimit != null ? 0 : null
            };

            if (!couponValidator.Validate(coupon).IsValid)
            {
                throw new Exception("Coupon is not valid.");
            }

            await couponRepository.CreateCouponAsync(coupon);

            if (coupon.Availability == CouponAvailability.Private && couponDto.Users != null)
            {
                var userCouponMappings = new List<UserCouponMapping>();
                foreach (var userId in couponDto.Users)
                {
                    userCouponMappings.Add(new UserCouponMapping
                    {
                        UserId = userId,
                        CouponId = coupon.Id,
                        IsUsed = false
                    });
                }

                await userCouponMappingRepository.CreateUserCouponMappingsAsync(userCouponMappings);
            }
        }

        public async Task UpdateCouponAsync(CouponDto couponDto)
        {
            await couponRepository.UpdateCouponAsync(_mapper.Map<Coupon>(couponDto));
        }

        public async Task DeleteCouponAsync(int id)
        {
            var coupon = await couponRepository.GetCouponByIdAsync(id);
            if (coupon == null)
            {
                throw new Exception("Coupon not found.");
            }
            await couponRepository.DeleteCouponAsync(coupon);
        }
    }
}
