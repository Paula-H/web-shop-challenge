using Domain.Dto.Create;
using Domain.Dto.View;

namespace Service.Abstract
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrdersByUserIdAsync(int id);
        public Task<OrderDto> PlaceOrderAsync(CreateOrderDto createOrderDto);
        public Task ConfirmOrderAsync(int id);
        public Task CancelOrderAsync(int id);
        public Task UpdateOrderAsync(OrderDto orderDto);
        public Task DeleteOrderAsync(int id);
    }
}
