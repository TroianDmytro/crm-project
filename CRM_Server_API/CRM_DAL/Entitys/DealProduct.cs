using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class DealProduct
    {
        [Required]
        public Guid DealId { get; set; }
        public Deal Deal { get; set; } // Связь с моделью сделки

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } // Связь с моделью продукта

        [Required]
        public int QuantityTransaction { get; set; } // Количество продукта в сделке
    }
}
