
using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IDealProductService
    {
        Task AddProductToDeal(DealProductDTO dealProductDTO);
        Task UpdateQuantityTransaction(DealProductDTO dealProductDTO);
        Task DeleteProductFromDeal(DealProductDTO dealProductDTO);
    }
}
