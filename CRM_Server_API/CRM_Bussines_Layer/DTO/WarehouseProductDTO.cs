namespace CRM_Business_Layer.DTO
{
    public class WarehouseProductDTO
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid ProductId { get; set; }

        public int QuantityStock { get; set; } = 0; // Количество продукта на складе

    }
}
