using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }
    }
}