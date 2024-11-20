using CRM_Business_Layer.DTO;

namespace CRM_Business_Layer.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDTO>> GetAllWarehousesAsync();
        Task<IEnumerable<WarehouseDTO>> GetAllWarehouseWithProductAsync();
        Task<WarehouseDTO> GetWarehouseByIdAsync(Guid id);
        Task<WarehouseDTO?> GetWarehouseByIdWithProductsAsync(Guid id);
        Task AddWarehousesAsync(WarehouseDTO warehouseDTO);
        Task AddProductToWarehouse(WarehouseProductDTO warehouseProductDTO);
        Task UpdateWarehousesAsync(Guid id, WarehouseDTO updateWarehouse);
        Task UpdateProductQuantitySubtracting(Guid id, int quantity);
        Task UpdateProductQuantityAdd(Guid id, int quantity);
        Task DeleteWarehousesAsync(Guid id);
        Task DeleteProductWithWarehouseAsync(Guid warehouseId, Guid productId);
        Task<bool> IsExists(Guid dealId);
        void Dispose();

    }
}
