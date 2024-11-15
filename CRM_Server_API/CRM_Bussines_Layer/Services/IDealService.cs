using CRM_DAL.Entitys;

namespace CRM_Business_Layer.Services
{
    public interface IDealService
    {
        Task<IEnumerable<Deal>> GetAllDealsAsync();
        Task<Deal> GetDealByIdAsync(int id);
        Task AddDealAsync(Deal deal);
        Task UpdateDealAsync(Deal deal);
        Task DeleteDealAsync(int id);
        Task<decimal> GetProductPriceAsync(int productId);
        Task AddProductToDealAsync(int dealId, int productId, int quantity);
    }
}
