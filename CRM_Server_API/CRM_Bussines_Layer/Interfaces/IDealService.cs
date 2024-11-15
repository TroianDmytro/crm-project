using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IDealService
    {
        Task<IEnumerable<DealDTO>> GetAllDealsAsync();
        Task<DealDTO> GetDealByIdAsync(Guid id);
        Task AddDealAsync(DealDTO deal);
        Task UpdateDealAsync(DealDTO deal);
        Task DeleteDealAsync(Guid id);
        Task<decimal> GetProductPriceAsync(Guid productId);
        //Task AddProductToDealAsync(Guid dealId, Guid productId, int quantityTransaction);
        void Dispose();
    }
}
