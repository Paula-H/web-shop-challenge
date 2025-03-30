using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("UserCouponMappings")]
    public class UserCouponMapping
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Coupon")]
        public int CouponId { get; set; }

        public Boolean IsUsed { get; set; }

        public DateTime? ReedemedAt { get; set; }

        public virtual User? User { get; set; }

        public virtual Coupon? Coupon { get; set; }
    }
}