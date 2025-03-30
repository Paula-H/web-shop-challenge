using Domain.Dto;
using Domain.Dto.Create;
using Domain.Dto.View;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService couponService;
        private readonly ILogger<CouponController> _logger;

        public CouponController(ICouponService couponService, ILogger<CouponController> logger)
        {
            this.couponService = couponService;
            _logger = logger;
        }

        [HttpGet("GetAllCoupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
            try
            {
                _logger.LogInformation("Getting all coupons");
                var coupons = await couponService.GetAllCouponsAsync();
                return Ok(coupons);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get all coupons: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetCouponsByUserId")]
        public async Task<IActionResult> GetCouponsByUserId(int userId)
        {
            try
            {
                _logger.LogInformation("Getting all coupons for user {0}", userId);
                var coupons = await couponService.GetACouponsByUserIdAsync(userId);
                return Ok(coupons);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get all coupons for user: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateCoupon")]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDto coupon)
        {
            try
            {
                _logger.LogInformation("Creating coupon {0}", coupon.Code);
                await couponService.CreateCouponAsync(coupon);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to create coupon: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddCouponToOrder")]
        public async Task<IActionResult> AddCouponToOrder([FromBody] AddCouponDto coupon)
        {
            try
            {
                _logger.LogInformation("Adding coupon {0} to order {1}", coupon.CouponCode, coupon.OrderId);
                await couponService.AddCouponToOrderAsync(coupon);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to add coupon to order: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateCoupon")]
        public async Task<IActionResult> UpdateCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                _logger.LogInformation("Updating coupon {0}", coupon.Code);
                await couponService.UpdateCouponAsync(coupon);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update coupon: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteCoupon")]
        public async Task<IActionResult> DeleteCoupon([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation("Deleting coupon {0}", id);
                await couponService.DeleteCouponAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to delete coupon: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
