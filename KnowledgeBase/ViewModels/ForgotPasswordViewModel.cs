using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
