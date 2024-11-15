using CRM_DAL.Entitys;

namespace CRM_DAL.Interfaces
{
    public interface IRepositoryDealProduct 
    {
        Task Add(DealProduct item);
        Task Update(DealProduct item);
        Task Delete(DealProduct item);
        Task<bool> DealExists(Guid item);
        Task<bool> ProductExists(Guid item);
    }
}
