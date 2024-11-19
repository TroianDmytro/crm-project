using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AzureDbContext _context;
        public ClientRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetAsync(Guid id)
        {
            var result = await _context.Clients
                                        .Include(c=>c.Deals)
                                        .ThenInclude(d=>d.DealProducts)
                                        .ThenInclude(dp => dp.Product)
                                        .FirstOrDefaultAsync(c => c.Id == id); 
            return result;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var result = await _context.Clients
                                        .Include(c => c.Deals)
                                        .ThenInclude(d => d.DealProducts)
                                        .ThenInclude(dp=>dp.Product)
                                        .ToListAsync();
            return result;
        }

        public async Task CreateAsync(Client item)
        {
            await _context.Clients.AddAsync(item);
        }

        public async Task Update(Client item)
        {
            _context.Clients.Update(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Clients.Where(c=>c.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Client>> Find(Func<Client, bool> predicate)
        {
            var result =  _context.Clients.Where(predicate).ToList();
            return result;
        }

        public async Task<bool> IsExists(Guid id)
        {
            bool result = await _context.Clients.AnyAsync(c=>c.Id == id);
            return result;
        }
    }
}
