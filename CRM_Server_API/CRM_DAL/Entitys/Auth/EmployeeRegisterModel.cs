using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys.Auth
{
    public class EmployeeRegisterModel : IdentityUser
    {
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [StringLength(100, ErrorMessage = "Максимальна довжина імені — 100 символів")]
        public string Name { get; set; } = string.Empty; // Ім'я 

        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [StringLength(100, ErrorMessage = "Максимальна довжина прізвища — 100 символів")]
        public string LastName { get; set; } = string.Empty; // Прізвище

        [StringLength(100, ErrorMessage = "Максимальна довжина прізвища — 100 символів")]
        public string? Patronymic {  get; set; }=string.Empty;
        // Адреса 
        public string? Address { get; set; } = string.Empty; 

        //День рождения
        [Required(ErrorMessage = "Дата народження обов'язкова")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        //Дата начала работы
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        //Должность
        [StringLength(100, ErrorMessage = "Максимальна довжина посади — 100 символів")]
        public string? Position { get; set; }

        public string? Department { get; set; }
    }
}





