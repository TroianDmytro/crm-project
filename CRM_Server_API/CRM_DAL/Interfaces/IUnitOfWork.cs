using CRM_DAL.Entitys;

namespace CRM_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Client { get; }
        //IRepository<Category> Categories { get; }

        Task CommitChanges();
    }
}