
using CRM_Business_Layer.Infrastructure;

namespace CRM_Business_Layer.DTO
{
    public class DealUpdate
    {
        public string? Title { get; set; }
        public decimal? Amount { get; set; } // Сумма сделки
        public DateTime? ExpectedCloseDate { get; set; } // Предполагаемая дата закрытия
        public string? Status { get; set; } = "New"; // Этап сделки, например, "новая", "в процессе", "завершена"
    }
}
