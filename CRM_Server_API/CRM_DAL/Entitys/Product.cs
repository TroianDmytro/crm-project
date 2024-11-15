using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(50)]
        public string? AvailabilityStatus { get; set; } // например, "В наличии", "Под заказ", "Нет в наличии" 

        public byte[]? PhotoBlob { get; set; }

        public int QuantityStock { get; set; } = 0; // Количество продукта на складе

        [Required]
        public ICollection<DealProduct> DealProducts { get; set; } //  Связь с сделками
    }
}
