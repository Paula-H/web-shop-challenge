using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class OrderCouponMappingRepository : IOrderCouponMappingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderCouponMappingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<OrderCouponMapping?> GetOrderCouponMappingByOrderIdAndCouponIdAsync(int orderId, int couponId)
        {
            return _dbContext.OrderCouponMappings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == orderId && x.CouponId == couponId);
        }

        public Task<List<OrderCouponMapping>> GetOrderCouponMappingsByCouponIdAsync(int couponId)
        {
            return _dbContext.OrderCouponMappings
                .AsNoTracking()
                .Where(x => x.CouponId == couponId)
                .ToListAsync();
        }

        public Task<List<OrderCouponMapping>> GetOrderCouponMappingsByOrderIdAsync(int orderId)
        {
            return _dbContext.OrderCouponMappings
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderCouponMapping> CreateOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping)
        {
            var result = await _dbContext.OrderCouponMappings.AddAsync(orderCouponMapping);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateOrderCouponMappingsAsync(ICollection<OrderCouponMapping> orderCouponMappings)
        {
            await _dbContext.OrderCouponMappings.AddRangeAsync(orderCouponMappings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping)
        {
            _dbContext.OrderCouponMappings.Update(orderCouponMapping);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderCouponMappingAsync(OrderCouponMapping orderCouponMapping)
        {
            _dbContext.OrderCouponMappings.Remove(orderCouponMapping);
            await _dbContext.SaveChangesAsync();
        }
    }
}
