using CRM_DAL.Entitys;

namespace CRM_Server_API.Models.Responce
{
    public class ProductResponce
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryResponce Categorys { get; set; }
        public string? AvailabilityStatus { get; set; } // например, "В наличии", "Под заказ", "Нет в наличии" 
        public string? PhotoBase64 { get; set; } = null;
        public int Quantity { get; set; } // Количество продукта на складе
    }
}
