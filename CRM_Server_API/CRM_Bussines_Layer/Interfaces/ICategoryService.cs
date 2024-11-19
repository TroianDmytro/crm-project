using CRM_Business_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Business_Layer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryWithProductsDTO> GetCategoryWithProductsAsync(Guid id);
        Task AddCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(CategoryDTO categoryDto);
        Task DeleteCategoryAsync(Guid id);
    }
}
