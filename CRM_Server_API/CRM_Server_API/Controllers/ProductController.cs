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

        /// <summary>
        /// Получаем список всех продуктов.
        /// </summary>
        /// <returns>Список продуктов в формате <see cref="ProductResponce"/></returns>
        /// <returns>Возвращает код 200 при успешном получении списка продуктов</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<ProductDTO> result = await _productService.GetAllProductsAsync();
            result = result.ToList();

            List<ProductResponce> productResponce = _mapper.Map<List<ProductResponce>>(result);

            return Ok(productResponce);
        }


        /// <summary>
        /// Получучаем продукт по ID 
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        /// <returns>Продукт в формате <see cref="ProductResponce"/></returns>
        /// <returns>Возвращает код 200 если есть продукт с таким ID</returns>
        /// <returns>Возвращает код 404 если продукт с таким ID не найден/returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductId(Guid id)
        {
            ProductDTO product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product with this Id not found");

            ProductResponce productResponce = _mapper.Map<ProductResponce>(product);

            return Ok(productResponce);
        }


        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        /// <param name="productRequest">Данные продукта для добавления</param>
        /// <returns>Добавленный продукт в формате <see cref="ProductDTO"/></returns>
        /// <returns>Возвращает код 200 при успешном добавлении продукта</returns>
        /// <returns>Возвращает код 400 при неправильной валидации данных</returns>
        [HttpPost("add/")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest productRequest)
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(productRequest);
            await _productService.AddProductAsync(productDTO);

            return Ok(productDTO);
        }


        /// <summary>
        /// Обновление существующего продукта по ID
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        /// <param name="productUpdate">Данные для обновления продукта</param>
        /// <returns>Возвращает код 204 при успешном обновлении продукта</returns>
        /// <returns>Возвращает код 404 если продукт с указанным ID не найден</returns>
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



        /// <summary>
        /// Удаление продукта по ID
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Возвращает код 200 при успешном удалении продукта</returns>
        /// <returns>Возвращает код 404 если продукт с указанным ID не найден </returns>
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
