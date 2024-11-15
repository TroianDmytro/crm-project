using CRM_DAL.Entitys;

namespace CRM_DAL.Interfaces
{
    public interface IDealRepository
    {
        Task<IEnumerable<Deal>> GetAllDealsAsync();
        Task<Deal> GetDealByIdAsync(int id);
        Task AddDealAsync(Deal deal);
        Task UpdateDealAsync(Deal deal);
        Task DeleteDealAsync(int id);
        Task<decimal> GetProductPriceAsync(int productId);
    }
}
