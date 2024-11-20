
using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class WarehouseProduct
    {
        [Key]
        public Guid Id { get; set; }

        public Guid WarehouseId {  get; set; }
        public Warehouse Warehouses { get; set; }

        public Guid ProductId { get; set; }
        public Product Products { get; set; }

        public int QuantityStock { get; set; } = 0; // Количество продукта на складе
    }
}
