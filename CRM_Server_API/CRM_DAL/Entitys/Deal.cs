using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRM_DAL.Entitys
{
    public class Deal
    {
        [Key]
        public int DealId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; } // Сумма сделки

        [Required]
        public DateTime ExpectedCloseDate { get; set; } // Предполагаемая дата закрытия

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Этап сделки, например, "новая", "в процессе", "завершена"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Связь с клиентом
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [JsonIgnore] // Добавил игнор так как была проблема с Серелизацией и циклической зависимостью между customer and deal
        public Customer Customer { get; set; } // Навигационное свойство

        public ICollection<DealProduct> DealProducts { get; set; }
    }
}
