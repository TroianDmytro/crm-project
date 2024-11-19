using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDTO>> GetAllWarehousesAsync();
        Task<WarehouseDTO> GetWarehousesByIdAsync(Guid id);
        Task AddWarehousesAsync(WarehouseDTO deal);
        Task UpdateWarehousesAsync(Guid id, WarehouseDTO updateWarehouse);
        Task DeleteWarehousesAsync(Guid id);
        Task<bool> IsExists(Guid dealId);
        void Dispose();

    }
}
