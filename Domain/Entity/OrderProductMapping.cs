using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("OrderProductMappings")]
    public class OrderProductMapping
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Product? Product { get; set; }
    }
}