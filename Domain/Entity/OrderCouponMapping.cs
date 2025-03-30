using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("OrderCouponMappings")]
    public class OrderCouponMapping
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Coupon")]
        public int CouponId { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Coupon? Coupon { get; set; }
    }
}