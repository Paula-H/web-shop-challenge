using Domain.Entity;

namespace Repository.Abstract
{
    public interface IOrderCouponMappingRepository
    {   
        public Task<OrderCouponMapping?> GetOrderCouponMappingByOrderIdAndCouponIdAsync(int orderId, int couponId);
        public Task<List<OrderCouponMapping>> GetOrderCouponMappingsByOrderIdAsync(int orderId);
        public Task<List<OrderCouponMapping>> GetOrderCouponMappingsByCouponIdAsync(int couponId);
        public Task<OrderCouponMapping> CreateOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping);
        public Task CreateOrderCouponMappingsAsync(ICollection<OrderCouponMapping> orderCouponMappings);
        public Task UpdateOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping);
        public Task DeleteOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping);
    }
}
