using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public OrderStatus Status { get; set; }

        public float TotalPrice { get; set; }

        public float Deduction { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<OrderProductMapping>? OrderProductMappings { get; set; }

        public virtual ICollection<OrderCouponMapping>? OrderCouponMappings { get; set; }
    }
}