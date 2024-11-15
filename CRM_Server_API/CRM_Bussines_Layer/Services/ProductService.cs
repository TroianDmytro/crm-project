using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_Business_Layer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var result = await _context.Product.GetAll();
            var resultDTO = _mapper.Map<List<ProductDTO>>(result);
            return resultDTO;
        }

        public async Task<ProductDTO?> GetProductByIdAsync(Guid id)
        {
            var result = await _context.Product.Get(id);
            var resultDTO = _mapper.Map<ProductDTO>(result);
            return resultDTO;
        }
        public async Task AddProductAsync(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _context.Product.Create(product);
            await _context.CommitChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _context.Product.Update(product);
            await _context.CommitChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _context.Product.Delete(id);
            await _context.CommitChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
