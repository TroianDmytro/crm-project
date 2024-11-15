using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(ProductDTO product);
        Task UpdateProductAsync(ProductDTO product);
        Task DeleteProductAsync(Guid id);
        void Dispose();
    }
}
