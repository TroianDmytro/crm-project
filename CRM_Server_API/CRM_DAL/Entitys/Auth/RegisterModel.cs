﻿using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys.Auth
{
    public class RegisterModel
    {
        //Login
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
