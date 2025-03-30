namespace Domain.Dto.Create
{
    public class CreateProductDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }
    }
}

