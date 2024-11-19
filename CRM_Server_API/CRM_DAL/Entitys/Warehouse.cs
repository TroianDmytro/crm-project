using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    //Склад
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

        //Product on warehouse
        //public ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
