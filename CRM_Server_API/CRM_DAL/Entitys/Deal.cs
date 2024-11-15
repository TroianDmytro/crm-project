using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class Deal
    {
        [Key]
        public Guid DealId { get; set; }

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

        public DateTime CreatedAt { get; set; } // Дата створення запису
        public DateTime? UpdatedAt { get; set; } // Дата оновлення запису

        // Связь с клиентом
        [ForeignKey("Clients")]
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        //список сделок
        public ICollection<DealProduct> DealProducts { get; set; }
    }
}
