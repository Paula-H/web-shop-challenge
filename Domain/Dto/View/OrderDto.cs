using Domain.Enum;

namespace Domain.Dto.View
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public OrderStatus Status { get; set; }

        public float TotalPrice { get; set; }

        public float Deduction { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
