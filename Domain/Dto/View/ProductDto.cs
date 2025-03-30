namespace Domain.Dto.View
{
    public class ProductDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }
    }
}
