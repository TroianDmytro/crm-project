using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получаем список всех категорий
        /// </summary>
        /// <returns>Возвращает код 200 при успешном получении списка категорий</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet("names")]
        public async Task<IActionResult> GetAllCategoryNames()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories.Select(c => c.Name));
        }

        /// <summary>
        /// Получаем список категорий с продуктами
        /// </summary>
        /// <returns>Возвращает код 200 при успешном получении категорий с продуктами</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet("names_with_products")]
        public async Task<IActionResult> GetAllCategoriesWithProducts()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Получаем категорию по Id вместе с продуктами
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Возвращает код 200 при успешном получении категории по ID </returns>
        /// <returns>Возвращает код 404 если категория с указанным ID не найдена </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryWithProductsById(Guid id)
        {
            var category = await _service.GetCategoryWithProductsAsync(id);
            if (category == null)
                return NotFound("Category with this Id not found");
            return Ok(category);
        }

        /// <summary>
        /// Добавляем новую категорию
        /// </summary>
        /// <param name="categoryDto">Категория в формате <see cref="CategoryDTO"/> </param>
        /// <returns>Возвращает код 200 при успешном добавлении категории</returns>
        /// <returns>Возвращает код 400 если введены некорректные данные</returns>
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            await _service.AddCategoryAsync(categoryDto);
            return Ok();
        }

        /// <summary>
        /// Обновляем категорию по ID
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="categoryDto">Обновленная категория</param>
        /// <returns>Возвращает код 200 при успешном обновлении категории</returns>
        /// <returns>Возвращает код 404 если категория с указанным Id не найдена</returns>
        /// <returns>Возвращает код 400 если введены некорректные данные</returns>
        [HttpPut("EditCategory")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDTO categoryDto)
        {
            categoryDto.Id = id;
            await _service.UpdateCategoryAsync(categoryDto);
            return Ok();
        }

        /// <summary>
        /// Удаляем категорию по ID
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Возвращает код 200 при успешном удалении категории</returns>
        /// <returns>Возвращает код 404 если категория с указанным Id не найдена</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _service.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
