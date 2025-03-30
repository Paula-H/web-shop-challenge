using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserCouponMapping>? UserCouponMappings { get; set; }
    }
}