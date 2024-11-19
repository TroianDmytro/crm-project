using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Models.Request;
using CRM_Server_API.Models.Responce;
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

            List<ProductResponce> productResponce = _mapper.Map<List<ProductResponce>>(result);

            return Ok(productResponce);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductId(Guid id)
        {
            ProductDTO product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            ProductResponce productResponce = _mapper.Map<ProductResponce>(product);

            return Ok(productResponce);
        }


        [HttpPost("add/")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest productRequest)
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(productRequest);
            await _productService.AddProductAsync(productDTO);

            return Ok(productDTO);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest productUpdate)
        {
            bool productIsExists = await _productService.ProductIsExists(id);
            if (productIsExists)
                return NotFound("Product with this Id not found");

            ProductDTO productDTO;
            if (productUpdate.PhotoBlob != null)
            {
                productDTO = _mapper.Map<ProductDTO>(productUpdate);
            }
            else
            {
                productDTO = await _productService.GetProductByIdAsync(id);

                productDTO.Name = productUpdate.Name;
                productDTO.Price = productUpdate.Price;
                productDTO.Description = productUpdate.Description;
                productDTO.Category = productUpdate.Category;
                productDTO.AvailabilityStatus = productUpdate.AvailabilityStatus;

            }
            productDTO.ProductId = id;

            await _productService.UpdateProductAsync(productDTO);
            return NoContent();
        }


        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            bool isExists = await _productService.ProductIsExists(id);
            if (isExists)
                return NotFound("Product with this Id not found");

            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
