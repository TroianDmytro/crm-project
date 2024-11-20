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

        [MaxLength(50)]
        public string? AvailabilityStatus { get; set; } // например, "В наличии", "Под заказ", "Нет в наличии" 

        public string? PhotoBlob { get; set; }

        [Required]
        public ICollection<DealProduct> DealProducts { get; set; } //  Связь с сделками

        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } // связь со складами
        public Guid CategoryId { get; set; }  // Связь с категориями
        public Category Categorys { get; set; }

    }
}
