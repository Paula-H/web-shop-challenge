using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Order?> GetOrderByIdAsync(int id)
        {
            return _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.OrderCouponMappings)
                    .ThenInclude(x => x.Coupon)
                .Include(x => x.OrderProductMappings)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return _dbContext.Orders
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Order>> GetOrdersByUserIdAsync(int id)
        {
            return _dbContext.Orders
                .Where(x => x.UserId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var result = await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateOrdersAsync(ICollection<Order> orders)
        {
            await _dbContext.Orders.AddRangeAsync(orders);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
