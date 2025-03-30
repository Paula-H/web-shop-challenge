using Domain.Entity;

namespace Repository.Abstract
{
    public interface ICouponRepository
    {
        public Task<Coupon?> GetCouponByIdAsync(int couponId);
        public Task<Coupon?> GetCouponByCodeAsync(string couponCode);
        public Task<List<Coupon>> GetCouponsAsync();
        public Task<Coupon> CreateCouponAsync(Coupon coupon);
        public Task CreateCouponsAsync(ICollection<Coupon> coupons);
        public Task UpdateCouponAsync(Coupon coupon);
        public Task DeleteCouponAsync(Coupon coupon);

    }
}