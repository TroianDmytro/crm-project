namespace CRM_Server_API.Models.Request
{
    public class ProductRequest
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public string? AvailabilityStatus { get; set; } // например, "В наличии", "Под заказ", "Нет в наличии" 
        public IFormFile? PhotoBlob { get; set; } = null;
    }
}

