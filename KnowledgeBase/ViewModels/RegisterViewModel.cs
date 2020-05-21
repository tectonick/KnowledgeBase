using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredConfirmPassword")]
        [Compare("Password", ErrorMessage = "NotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string PasswordConfirm { get; set; }
    }


}
