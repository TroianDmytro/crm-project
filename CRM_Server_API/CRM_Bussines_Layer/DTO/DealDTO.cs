using CRM_DAL.Entitys;

namespace CRM_Business_Layer.DTO
{
    public class DealDTO
    {
        public Guid DealId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; } // Сумма сделки
        public DateTime ExpectedCloseDate { get; set; } // Предполагаемая дата закрытия
        public string Status { get; set; } // Этап сделки, например, "новая", "в процессе", "завершена"
        public DateTime CreatedAt { get; set; }
        public Guid ClientId { get; set; }// Связь с клиентом
        public Client Client { get; set; }
        public ICollection<Product> Products { get; set; }//список сделок
    }
}
