using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly AzureDbContext _context;

        public DealRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deal>> GetAllDealsAsync()
        {
            return await _context.Deals
                .Include(d => d.Customer)
                .Include(d => d.DealProducts)
                    .ThenInclude(dp => dp.Product)
                .ToListAsync();
        }

        public async Task<Deal> GetDealByIdAsync(int id)
        {
            return await _context.Deals
                .Include(d => d.Customer)
                .Include(d => d.DealProducts)
                    .ThenInclude(dp => dp.Product)
                .FirstOrDefaultAsync(d => d.DealId == id);
        }

        public async Task AddDealAsync(Deal deal)
        {
            await _context.Deals.AddAsync(deal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDealAsync(Deal deal)
        {
            _context.Deals.Update(deal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDealAsync(int id)
        {
            var deal = await _context.Deals.FindAsync(id);
            if (deal != null)
            {
                _context.Deals.Remove(deal);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<decimal> GetProductPriceAsync(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found");

            return product.Price;
        }
    }
}
