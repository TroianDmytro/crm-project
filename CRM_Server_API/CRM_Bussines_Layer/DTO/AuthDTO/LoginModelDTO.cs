using System.ComponentModel.DataAnnotations;

namespace CRM_Business_Layer.DTO.AuthDTO
{
    public class LoginModelDTO
    {
        //Login
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
