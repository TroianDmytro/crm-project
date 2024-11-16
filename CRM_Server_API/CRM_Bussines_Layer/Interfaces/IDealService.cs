using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IDealService
    {
        Task<IEnumerable<DealDTO>> GetAllDealsAsync();
        Task<DealDTO> GetDealByIdAsync(Guid id);
        Task AddDealAsync(DealDTO deal);
        Task UpdateDealAsync(Guid id, DealUpdate dealUpdate);
        Task DeleteDealAsync(Guid id);
        Task<decimal> GetProductPriceAsync(Guid productId);
        Task<bool> DealIsExists(Guid dealId);
        void Dispose();
    }
}
