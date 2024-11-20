using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AzureDbContext _context;

        public ProductRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _context.Products
                .Include(p => p.Categorys)
                .Include(p => p.DealProducts)
                .ThenInclude(dp => dp.Deal)
                .ToListAsync();

            return result;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var result = await _context.Products
               .Include(p => p.Categorys)
               .Include(p => p.DealProducts)
               .ThenInclude(dp => dp.Deal)
               .FirstOrDefaultAsync(p => p.ProductId == id);

            return result;
        }

        public async Task CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
        }

        public async Task Update(Product item)
        {
            _context.Products.Update(item);
        }

        public async Task<IEnumerable<Product>> Find(Func<Product, bool> predicate)
        {
            var result = _context.Products.Where(predicate).ToList();
            return result;
        }

        public async Task<bool> IsExists(Guid id)
        {
            var result = await _context.Products.AnyAsync(p => p.ProductId == id);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Products.Where(d => d.ProductId == id).ExecuteDeleteAsync();
        }
    }
}
