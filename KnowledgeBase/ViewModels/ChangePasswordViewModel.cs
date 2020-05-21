using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }
        

        [Required]
        [Compare("NewPassword", ErrorMessage = "NotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }
    }

}
