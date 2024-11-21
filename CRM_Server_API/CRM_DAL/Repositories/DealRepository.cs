using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class DealRepository : IRepository<Deal>
    {
        private readonly AzureDbContext _context;

        public DealRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deal>> GetAllAsync()
        {
            var result = await _context.Deals
                .Include(d=>d.Client)
                .Include(d=>d.DealProducts)
                .ThenInclude(p=>p.Product)
                .ToListAsync();

            // Проходим по каждой сделке
            foreach (var deal in result)
            {
                // Проходим по каждому продукту в сделке
                foreach (var dealProduct in deal.DealProducts)
                {
                    var product = dealProduct.Product;

                    // Если продукт найден, присваиваем его количество из QuantityTransaction
                    if (product != null)
                    {
                        product.Quantity = dealProduct.QuantityTransaction;
                    }
                }
            }

            return result;
        }

        public async Task<Deal?> GetByIdAsync(Guid id)
        {
            var result = await _context.Deals
                .Include(d => d.Client)
                .Include(d => d.DealProducts)
                .ThenInclude(dp => dp.Product)
                .FirstOrDefaultAsync(d => d.DealId == id);

            // Проходим по каждому продукту в сделке
            foreach (var dealProduct in result.DealProducts)
            {
                var product = dealProduct.Product;

                // Если продукт найден, присваиваем его количество из QuantityTransaction
                if (product != null)
                {
                    product.Quantity = dealProduct.QuantityTransaction;
                }
            }
            // Проходим по каждой сделке
            
            return result;
        }

        public async Task CreateAsync(Deal item)
        {
            await _context.Deals.AddAsync(item);
        }

        public async Task Update(Deal item)
        {
            await Task.Run(() => _context.Deals.Update(item));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Deals.Where(d=>d.DealId==id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Deal>> Find(Func<Deal, bool> predicate)
        {
            var result = await Task.Run(() => _context.Deals.Where(predicate).ToList());
            return result;
        }

        public async Task<bool> IsExists(Guid id)
        {
            bool result = await _context.Deals.AnyAsync(d=>d.DealId == id);
            return result;
        }
    }
}
