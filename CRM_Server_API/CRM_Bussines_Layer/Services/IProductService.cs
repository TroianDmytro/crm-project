using CRM_DAL.Entitys;

namespace CRM_Business_Layer.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
