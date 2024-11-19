using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
