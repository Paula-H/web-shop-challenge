using Domain.Enum;

namespace Domain.Dto.Create
{
    public class CreateCouponDto
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Code { get; set; }

        public CouponType Type { get; set; }

        public CouponAvailability Availability { get; set; }

        public float? DiscountRate { get; set; }

        public float? DiscountAmount { get; set; }

        public int? MaxUsageLimit { get; set; }

        public ICollection<int>? Users { get; set; }
    }
}
