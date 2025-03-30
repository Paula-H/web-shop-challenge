using Domain.Entity;

namespace Repository.Abstract
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<List<Order>> GetOrdersAsync();
        public Task<List<Order>> GetOrdersByUserIdAsync(int id);
        public Task<Order> CreateOrderAsync(Order order);
        public Task CreateOrdersAsync(ICollection<Order> orders);
        public Task UpdateOrderAsync(Order order);
        public Task DeleteOrderAsync(Order order);
    }
}
