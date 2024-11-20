
namespace CRM_Business_Layer.DTO
{
    public class WarehouseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        //public ICollection<ProductDTO> ProductDTOs { get; set; } = [];//список продуктов


    }
}
