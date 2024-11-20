namespace CRM_Server_API.Models.Request
{
    public class WarehouseProductRequest
    {
        public Guid WarehouseId { get; set; }

        public Guid ProductId { get; set; }

        public int QuantityStock { get; set; } = 0; // Количество продукта на складе
    }
}
