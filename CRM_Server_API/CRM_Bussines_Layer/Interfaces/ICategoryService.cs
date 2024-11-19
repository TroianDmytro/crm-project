using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryAsync(Guid id);
        Task AddCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(CategoryDTO categoryDto);
        Task DeleteCategoryAsync(Guid id);
    }
}
