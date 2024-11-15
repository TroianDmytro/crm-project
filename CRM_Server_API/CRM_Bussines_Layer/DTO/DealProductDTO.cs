
namespace CRM_Business_Layer.DTO
{
    public class DealProductDTO
    {
        public Guid DealId { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityTransaction { get; set; } // Количество продукта в сделке
    }
}
