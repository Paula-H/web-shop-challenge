using Domain.Dto.Create;
using Domain.Dto.View;
using Domain.Entity;

namespace Service.Abstract
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetAllProductsAsync();
        public Task CreateProductAsync(CreateProductDto product);
        public Task UpdateProductAsync(ProductDto productDto);
        public Task DeleteProductAsync(int id);
    }
}
