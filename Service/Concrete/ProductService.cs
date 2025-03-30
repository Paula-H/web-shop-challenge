using AutoMapper;
using Domain.Dto.Create;
using Domain.Dto.View;
using Domain.Entity;
using FluentValidation;
using Repository.Abstract;
using Service.Abstract;

namespace Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly AbstractValidator<Product> productValidator;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository, 
            AbstractValidator<Product> productValidator,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productValidator = productValidator;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return (await productRepository
                .GetProductsAsync())
                .Select(_mapper.Map<ProductDto>)
                .ToList();
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var validation = productValidator.Validate(product);
            if (!validation.IsValid)
            {
                throw new Exception(validation.ToString());
            }

            await productRepository.CreateProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var validation = productValidator.Validate(product);
            if (!validation.IsValid)
            {
                throw new Exception(validation.ToString());
            }
            await productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await productRepository.DeleteProductAsync(product);
        }
    }
}
