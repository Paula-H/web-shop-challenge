using Domain.Entity;

namespace Repository.Abstract
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductByIdAsync(int productId);
        public Task<List<Product>> GetProductsAsync();
        public Task<Product> CreateProductAsync(Product product);
        public Task CreateProductsAsync(ICollection<Product> products);
        public Task UpdateProductAsync(Product product);
        public Task UpdateProductsAsync(ICollection<Product> products);
        public Task DeleteProductAsync(Product product);
    }
}
