namespace CRM_DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> Find(Func<T, bool> predicate);

        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
    }
}