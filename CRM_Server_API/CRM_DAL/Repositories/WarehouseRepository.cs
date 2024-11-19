using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    internal class WarehouseRepository : IRepository<Warehouse>
    {
        private readonly AzureDbContext _context;

        public WarehouseRepository(AzureDbContext context) => _context = context;


        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            var result = await _context.Warehouses.ToListAsync();
            return result;
        }

        public async Task<Warehouse?> GetAsync(Guid id)
        {
            var result = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task Update(Warehouse item)
        {
            _context.Warehouses.Update(item);
        }

        public async Task CreateAsync(Warehouse item)
        {
            await _context.Warehouses.AddAsync(item);
        }
        public async Task<IEnumerable<Warehouse>> Find(Func<Warehouse, bool> predicate)
        {
            List<Warehouse> result = _context.Warehouses.Where(predicate).ToList();
            return result;
        }

        public async Task<bool> IsExists(Guid id)
        {
            var result = await _context.Warehouses.AnyAsync(p => p.Id == id);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Warehouses.Where(d => d.Id == id).ExecuteDeleteAsync();
        }

    }
}
