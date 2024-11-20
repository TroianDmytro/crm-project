using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AzureDbContext _context;

        public CategoryRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await _context.Categorys.Include(c=>c.Products).ToListAsync();
            return result;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
             var result = await _context.Categorys
               .Include(c => c.Products)
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

       
        public async Task CreateAsync(Category item)
        {
            await _context.Categorys.AddAsync(item);

        }

        public async Task Update(Category item)
        {
            _context.Categorys.Update(item);
        }

        public Task<IEnumerable<Category>> Find(Func<Category, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categorys.FirstOrDefaultAsync(c=>c.Id==id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
