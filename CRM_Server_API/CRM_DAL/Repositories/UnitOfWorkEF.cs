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
        private readonly ProductRepository _productRepository;
        private readonly DealRepository _dealRepository;
        private readonly DealProductRepository _dealProductRepository;
        public IRepository<Client> Client => _clientRepository;
        public IRepository<Product> Product => _productRepository;
        public IRepository<Deal> Deal => _dealRepository;
        public IRepositoryDealProduct DealProduct =>  _dealProductRepository;

        public UnitOfWorkEF(AzureDbContext context)
        {
            _context = context;
            _clientRepository = new ClientRepository(context); 
            _dealRepository = new DealRepository(context);
            _productRepository = new ProductRepository(context);
            _dealProductRepository = new DealProductRepository(context);
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

        public async Task CommitChangesAsync() => await _context.SaveChangesAsync();
    }
}