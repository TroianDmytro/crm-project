using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Business_Layer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryWithProductsDTO> GetCategoryWithProductsAsync(Guid id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryWithProductsDTO>(category);
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _repository.DeleteCategoryAsync(id);
        }
    }
}
