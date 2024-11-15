using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(int id) => await _productRepository.GetProductByIdAsync(id);

        public async Task AddProductAsync(Product product) => await _productRepository.AddProductAsync(product);

        public async Task UpdateProductAsync(Product product) => await _productRepository.UpdateProductAsync(product);

        public async Task DeleteProductAsync(int id) => await _productRepository.DeleteProductAsync(id);
    }
}
