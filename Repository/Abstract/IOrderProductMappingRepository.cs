using Domain.Entity;

namespace Repository.Abstract
{
    public interface IOrderProductMappingRepository
    {
        public Task<List<OrderProductMapping>> GetOrderProductMappingsByOrderIdAsync(int orderId);
        public Task<List<OrderProductMapping>> GetOrderProductMappingsByProductIdAsync(int productId);
        public Task<OrderProductMapping> CreateOrderProductMappingAsync(OrderProductMapping orderProductMapping);
        public Task CreateOrderProductMappingsAsync(ICollection<OrderProductMapping> orderProductMappings);
        public Task UpdateOrderProductMappingAsync(OrderProductMapping orderProductMapping);
        public Task DeleteOrderProductMappingAsync(OrderProductMapping orderProductMapping);
    }
}
