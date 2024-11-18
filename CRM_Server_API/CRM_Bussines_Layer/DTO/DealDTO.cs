using CRM_Business_Layer.Infrastructure;

namespace CRM_Business_Layer.DTO
{
    public class DealDTO
    {
        public Guid DealId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; } = 0; // Сумма сделки
        public DateTime ExpectedCloseDate { get; set; }= TimeUA.CurrentTimeAsync().Result; // Предполагаемая дата закрытия
        public string Status { get; set; } = "New"; // Этап сделки, например, "новая", "в процессе", "завершена"
        public DateTime CreatedAt { get; set; } = TimeUA.CurrentTimeAsync().Result;
        public Guid ClientId { get; set; }// Связь с клиентом
        public ClientDTO Client { get; set; }
        public ICollection<ProductDTO> ProductDTOs { get; set; } = [];//список продуктов
    }
}
