using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class ProfileUserViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

}
