namespace Domain.Dto.Create
{
    public class CreateOrderDto
    {
        public required int UserId { get; set; }
        public required Dictionary<int, int> ProductIdQuantity { get; set; }
    }
}
