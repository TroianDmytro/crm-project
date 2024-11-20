namespace CRM_DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> Find(Func<T, bool> predicate);
        Task<bool> IsExists(Guid id);
        Task CreateAsync(T item);
        Task Update(T item);
        Task DeleteAsync(Guid id);
    }
}