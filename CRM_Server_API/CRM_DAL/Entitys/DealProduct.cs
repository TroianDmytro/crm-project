using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    //Это я хотел сделать промежуточную таблицу для deal and Product. Многие к многим.
    public class DealProduct
    {
        [Key]
        public int DealItemId { get; set; }

        [Required]
        public int DealId { get; set; }
        public Deal Deal { get; set; } // Связь с моделью сделки

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } // Связь с моделью продукта

        [Required]
        public int Quantity { get; set; } // Количество продукта в сделке

        [Required]
        public decimal Price { get; set; }
    }
}
