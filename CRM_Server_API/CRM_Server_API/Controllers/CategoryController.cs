using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_Server_API.Models.Responce;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Server_API.Controllers
{
    [Route("category/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _categoryService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Получаем список всех категорий
        /// </summary>
        /// <returns>Возвращает код 200 при успешном получении списка категорий</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var listCategoty = await _categoryService.GetAllCategoriesAsync();

            List<CategoryResponce> categoryResponce = _mapper.Map<List<CategoryResponce>>(listCategoty);

            return Ok(categoryResponce);
        }

        /// <summary>
        /// Получаем список категорий с продуктами
        /// </summary>
        /// <returns>Возвращает код 200 при успешном получении категорий с продуктами</returns>
        /// <returns>Возвращает код 500 сервер не смог обработать данные</returns>
        [HttpGet("with_products/")]
        public async Task<IActionResult> GetAllCategoriesWithProducts()
        {
            var listCategoty = await _categoryService.GetAllCategoriesAsync();
            List<CategoryResponceWithProduct> categoriesResponce = _mapper.Map<List<CategoryResponceWithProduct>>(listCategoty.ToList());

            return Ok(categoriesResponce);
        }

        /// <summary>
        /// Получаем категорию по Id 
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Возвращает код 200 при успешном получении категории по ID </returns>
        /// <returns>Возвращает код 404 если категория с указанным ID не найдена </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if(category == null)
                return NotFound("Category with this Id not found");

            CategoryResponce categoryResponce = _mapper.Map<CategoryResponce>(category);

            return Ok(categoryResponce);
        }

        /// <summary>
        /// Получаем категорию по Id вместе с продуктами
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Возвращает код 200 при успешном получении категории по ID </returns>
        /// <returns>Возвращает код 404 если категория с указанным ID не найдена </returns>
        [HttpGet("with_products/{id}")]
        public async Task<IActionResult> GetCategoryWithProductsById(Guid id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
                return NotFound("Category with this Id not found");

            CategoryResponceWithProduct responceCategory = _mapper.Map<CategoryResponceWithProduct>(category);
            return Ok(responceCategory);
        }


        /// <summary>
        /// Добавляем новую категорию
        /// </summary>
        /// <param name="name">Категория в формате <see cref="CategoryDTO"/> </param>
        /// <returns>Возвращает код 200 при успешном добавлении категории</returns>
        /// <returns>Возвращает код 400 если введены некорректные данные</returns>
        [HttpPost("add/")]
        public async Task<IActionResult> AddCategory([FromBody] string name)
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Name = name
            };

            await _categoryService.AddCategoryAsync(categoryDTO);
            return Ok();
        }

        /// <summary>
        /// Обновляем категорию по ID
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="name">Обновленная категория</param>
        /// <returns>Возвращает код 200 при успешном обновлении категории</returns>
        /// <returns>Возвращает код 404 если категория с указанным Id не найдена</returns>
        /// <returns>Возвращает код 400 если введены некорректные данные</returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] string name)
        {
            CategoryDTO categoryDTO = await _categoryService.GetCategoryAsync(id);
            categoryDTO.Name = name;
            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Ok();
        }

        /// <summary>
        /// Удаляем категорию по ID
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Возвращает код 200 при успешном удалении категории</returns>
        /// <returns>Возвращает код 404 если категория с указанным Id не найдена</returns>
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
