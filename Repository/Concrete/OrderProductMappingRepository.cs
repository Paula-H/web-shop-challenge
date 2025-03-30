using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class OrderProductMappingRepository : IOrderProductMappingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderProductMappingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<OrderProductMapping>> GetOrderProductMappingsByOrderIdAsync(int orderId)
        {
            return _dbContext.OrderProductMappings
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public Task<List<OrderProductMapping>> GetOrderProductMappingsByProductIdAsync(int productId)
        {
            return _dbContext.OrderProductMappings
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<OrderProductMapping> CreateOrderProductMappingAsync(OrderProductMapping orderProductMapping)
        {
            var result = await _dbContext.AddAsync(orderProductMapping);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateOrderProductMappingsAsync(ICollection<OrderProductMapping> orderProductMappings)
        {
            await _dbContext.OrderProductMappings.AddRangeAsync(orderProductMappings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderProductMappingAsync(OrderProductMapping orderProductMapping)
        {
            _dbContext.OrderProductMappings.Update(orderProductMapping);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderProductMappingAsync(OrderProductMapping orderProductMapping)
        {
            _dbContext.OrderProductMappings.Remove(orderProductMapping);
            await _dbContext.SaveChangesAsync();
        }
    }
}
