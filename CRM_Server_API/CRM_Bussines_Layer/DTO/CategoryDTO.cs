
namespace CRM_Business_Layer.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}
