
namespace CRM_Business_Layer.DTO
{
    public class ProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string? AvailabilityStatus { get; set; } // например, "В наличии", "Под заказ", "Нет в наличии" 
        public byte[]? PhotoBlob { get; set; }
        public int Quantity { get; set; } // Количество продукта в сделке

    }
}
