using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Coupons")]
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Code { get; set; }

        public CouponType Type { get; set; }

        public CouponAvailability Availability { get; set; }

        public float? DiscountRate { get; set; }

        public float? DiscountAmount { get; set; }

        public int? MaxUsageLimit { get; set; }

        public int? UsageCount { get; set; }
    }
}