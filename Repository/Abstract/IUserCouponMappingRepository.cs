using Domain.Entity;

namespace Repository.Abstract
{
    public interface IUserCouponMappingRepository
    {
        public Task<UserCouponMapping?> GetUserCouponMappingByUserIdAndCouponIdAsync(int userId, int couponId);
        public Task<List<UserCouponMapping>> GetUserCouponMappingsByUserIdAsync(int userId);
        public Task<List<UserCouponMapping>> GetUserCouponMappingsByCouponIdAsync(int couponId);
        public Task<UserCouponMapping> CreateUserCouponMappingAsync(UserCouponMapping userCouponMapping);
        public Task CreateUserCouponMappingsAsync(ICollection<UserCouponMapping> userCouponMappings);
        public Task UpdateUserCouponMappingAsync(UserCouponMapping userCouponMapping);
        public Task UpdateUserCouponMappingsAsync(ICollection<UserCouponMapping> userCouponMappings);
        public Task DeleteUserCouponMappingAsync(UserCouponMapping userCouponMapping);
    }
}
