using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product?> GetProductByIdAsync(int productId)
        {
            return _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == productId);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _dbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var result = await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateProductsAsync(ICollection<Product> products)
        {
            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductsAsync(ICollection<Product> products)
        {
            _dbContext.Products.UpdateRange(products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
