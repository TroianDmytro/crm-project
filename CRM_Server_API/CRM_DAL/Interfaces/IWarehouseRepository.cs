using CRM_DAL.Entitys;

namespace CRM_DAL.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<IEnumerable<Warehouse>> GetAllWithProductAsync();
        Task<Warehouse?> GetByIdWithProductsAsync(Guid id);
        Task UpdateProductQuantitySubtracting(Guid id, int quantity);
        Task UpdateProductQuantityAdd(Guid id, int quantity);
        Task AddProductToWarehouse(WarehouseProduct warehouseProduct);
        Task DeleteProductWithWarehouseAsync(Guid warehouseId, Guid productId);
    }

}
