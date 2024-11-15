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

        public async Task<IEnumerable<Deal>> GetAll()
        {
            var result = await _context.Deals
                .Include(d=>d.DealProducts)
                .ThenInclude(dp=>dp.Product)
                .ToListAsync();
            return result;
        }

        public async Task<Deal?> Get(Guid id)
        {
            var result = await _context.Deals
                .Include(d => d.DealProducts)
                .ThenInclude(dp => dp.Product)
                .FirstOrDefaultAsync(d => d.DealId == id);
            return result;
        }

        public async Task Create(Deal item)
        {
            await _context.Deals.AddAsync(item);
        }

        public async Task Update(Deal item)
        {
            await Task.Run(() => _context.Deals.Update(item));
        }

        public async Task Delete(Guid id)
        {
            await _context.Deals.Where(d=>d.DealId==id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Deal>> Find(Func<Deal, bool> predicate)
        {
            var result = await Task.Run(() => _context.Deals.Where(predicate).ToList());
            return result;
        }



    }
}
