using CRM_DAL.EF;
using CRM_DAL.Entitys;
using Microsoft.EntityFrameworkCore;

namespace CRM_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Client { get; }
        IRepository<Product> Product { get; }
        IRepository<Deal> Deal { get; }
        IRepositoryDealProduct DealProduct { get; }
        IRepository<Warehouse> Warehouse { get; }
        AzureDbContext DbContext { get; }
        Task CommitChangesAsync();
    }
}