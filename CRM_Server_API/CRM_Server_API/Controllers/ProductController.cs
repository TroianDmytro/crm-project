using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("product/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<ProductDTO> result = await _productService.GetAllProductsAsync();
            result = result.ToList();

            return Ok(result);
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetProductId(Guid id)
        {
            ProductDTO product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            return Ok(product);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest productRequest)
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(productRequest);
            await _productService.AddProductAsync(productDTO);

            return Ok(productDTO);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id,[FromBody] UpdateProductDTO updateProductDTO) 
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            product.Name = updateProductDTO.Name;
            product.Price = updateProductDTO.Price;
            product.Description = updateProductDTO.Description;
            product.Category = updateProductDTO.Category;
            product.AvailabilityStatus = updateProductDTO.AvailabilityStatus;

            await _productService.UpdateProductAsync(product);
            return NoContent(); 
        }


        [HttpDelete("DeleteProductId")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
