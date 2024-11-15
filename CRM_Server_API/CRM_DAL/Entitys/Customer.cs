using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Например, "потенциальный", "активный", "завершенный"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Deal> Deals { get; set; } // Связь с сделками
    }
}
