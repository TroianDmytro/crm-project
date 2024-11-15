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

        public async Task<IEnumerable<Product>> GetAll()
        {
            var result = await _context.Products
                //.Include(p => p.DealProducts)
                //.ThenInclude(dp => dp.Deal)
                .ToListAsync();

            return result;
        }

        public async Task<Product?> Get(Guid id)
        {
            var result = await _context.Products
               .Include(p => p.DealProducts)
               .ThenInclude(dp => dp.Deal)
               .FirstOrDefaultAsync(p => p.ProductId == id);

            return result;
        }

        public async Task Create(Product item)
        {
            await _context.Products.AddAsync(item);
        }
        public async Task Update(Product item)
        {
            _context.Products.Update(item);
        }

        public async Task Delete(Guid id)
        {
            await _context.Products.Where(d => d.ProductId== id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Product>> Find(Func<Product, bool> predicate)
        {
            var result = _context.Products.Where(predicate).ToList();
            return result;
        }
    }
}
