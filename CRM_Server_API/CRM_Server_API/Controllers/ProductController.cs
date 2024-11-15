using CRM_Business_Layer.Services;
using CRM_DAL.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductId")]
        public async Task<IActionResult> GetProductId(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(
            [FromForm] string name,
            [FromForm] decimal price,
            [FromForm] string description,
            [FromForm] string category,
            [FromForm] string availabilityStatus)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                Category = category,
                AvailabilityStatus = availabilityStatus
            };

            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductId), new { id = product.ProductId }, product);
        }

        [HttpPut("UpdateProductId")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromForm] string name,
            [FromForm] decimal price,
            [FromForm] string description,
            [FromForm] string category,
            [FromForm] string availabilityStatus)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Category = category;
            product.AvailabilityStatus = availabilityStatus;

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("DeleteProductId")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
