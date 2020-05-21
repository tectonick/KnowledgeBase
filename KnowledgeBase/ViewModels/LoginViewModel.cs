﻿using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
