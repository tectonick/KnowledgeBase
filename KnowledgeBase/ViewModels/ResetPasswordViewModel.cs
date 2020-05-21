using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredPassword")]
        [StringLength(100, ErrorMessage = "MinimumSymbols", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredConfirmPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "NotMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

}
