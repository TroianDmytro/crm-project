using CRM_DAL.EF;
using CRM_DAL.Entitys;
using CRM_DAL.Interfaces;

namespace CRM_DAL.Repositories
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private bool disposed = false;
        private readonly AzureDbContext _context;
        private readonly ClientRepository _clientRepository;
        //private CategoryModelRepository _categoryRepository;

        public IRepository<Client> Client => _clientRepository;
        //public IRepository<News> News => _newsRepository ??= new NewsModelRepository(_context);
        //public IRepository<Category> Categories => _categoryRepository ??= new CategoryModelRepository(_context);


        public UnitOfWorkEF(AzureDbContext context)
        {
            _context = context;
            _clientRepository = new ClientRepository(context); 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public async Task CommitChanges() => await _context.SaveChangesAsync();
    }
}