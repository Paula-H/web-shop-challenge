using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CouponRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Coupon?> GetCouponByIdAsync(int couponId)
        {
            return _dbContext.Coupons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == couponId);
        }

        public Task<Coupon?> GetCouponByCodeAsync(string couponCode)
        {
            return _dbContext.Coupons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == couponCode);
        }

        public Task<List<Coupon>> GetCouponsAsync()
        {
            return _dbContext.Coupons
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Coupon> CreateCouponAsync(Coupon coupon)
        {
            var result = await _dbContext.Coupons.AddAsync(coupon);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateCouponsAsync(ICollection<Coupon> coupons)
        {
            await _dbContext.Coupons.AddRangeAsync(coupons);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCouponAsync(Coupon coupon)
        {
            _dbContext.Coupons.Update(coupon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCouponAsync(Coupon coupon)
        {
            _dbContext.Coupons.Remove(coupon);
            await _dbContext.SaveChangesAsync();
        }
    }
}
