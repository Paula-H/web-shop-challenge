using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class UserCouponMappingRepository : IUserCouponMappingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserCouponMappingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserCouponMapping?> GetUserCouponMappingByUserIdAndCouponIdAsync(int userId, int couponId)
        {
            return _dbContext.UserCouponMappings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CouponId == couponId);
        }

        public Task<List<UserCouponMapping>> GetUserCouponMappingsByUserIdAsync(int userId)
        {
            return _dbContext.UserCouponMappings
                .AsNoTracking()
                .Include(x => x.Coupon)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public Task<List<UserCouponMapping>> GetUserCouponMappingsByCouponIdAsync(int couponId)
        {
            return _dbContext.UserCouponMappings
                .AsNoTracking()
                .Where(x => x.CouponId == couponId)
                .ToListAsync();
        }

        public async Task<UserCouponMapping> CreateUserCouponMappingAsync(UserCouponMapping userCouponMapping)
        {
            var result = await _dbContext.UserCouponMappings.AddAsync(userCouponMapping);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateUserCouponMappingsAsync(ICollection<UserCouponMapping> userCouponMappings)
        {
            await _dbContext.AddRangeAsync(userCouponMappings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserCouponMappingAsync(UserCouponMapping userCouponMapping)
        {
            _dbContext.UserCouponMappings.Update(userCouponMapping);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserCouponMappingsAsync(ICollection<UserCouponMapping> userCouponMappings)
        {
            _dbContext.UserCouponMappings.UpdateRange(userCouponMappings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserCouponMappingAsync(UserCouponMapping userCouponMapping)
        {
            _dbContext.UserCouponMappings.Remove(userCouponMapping);
            await _dbContext.SaveChangesAsync();
        }
    }
}
