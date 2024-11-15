﻿using System.ComponentModel.DataAnnotations;

namespace CRM_DAL.Entitys
{
    public class LoginModel
    {
        //Login
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}