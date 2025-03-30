using Domain.Dto.Create;
using Domain.Dto.View;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            this.productService = productService;
            _logger = logger;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = await productService.GetAllProductsAsync();
            if (products.Count == 0)
            {
                _logger.LogInformation("No products found");
                return NoContent();
            }
            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            _logger.LogInformation("Creating product: {0}", productDto.Name);
            try
            {
                await productService.CreateProductAsync(productDto);
                _logger.LogInformation("Product {0} created successfully", productDto.Name);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create product {0}", productDto.Name);
                return BadRequest();
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            _logger.LogInformation("Updating product: {0}", productDto.Name);
            try
            {
                await productService.UpdateProductAsync(productDto);
                _logger.LogInformation("Product {0} updated successfully", productDto.Name);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update product {0}", productDto.Name);
                return BadRequest();
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            _logger.LogInformation("Deleting product: {0}", id);
            try
            {
                await productService.DeleteProductAsync(id);
                _logger.LogInformation("Product {0} deleted successfully", id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete product {0}", id);
                return BadRequest();
            }
        }
    }
}
