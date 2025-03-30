using Domain.Dto;
using Domain.Dto.Create;
using Domain.Dto.View;

namespace Service.Abstract
{
    public interface ICouponService
    {
        public Task<List<CouponDto>> GetAllCouponsAsync();
        public Task<List<CouponDto>> GetCouponsByUserIdAsync(int userId);
        public Task CreateCouponAsync(CreateCouponDto couponDto);
        public Task AddCouponToOrderAsync(AddCouponDto couponDto);
        public Task UpdateCouponAsync(CouponDto couponDto);
        public Task DeleteCouponAsync(int id);
    }
}
