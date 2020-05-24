using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.ViewModels
{
    public class ProfileUserViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //"1 5 25 120 730"
        [Display(Name = "DefaultIntervals")]
        [DataType(DataType.Text)]
        [RegularExpression(@"([0-9]+\s?)*", ErrorMessage = "IncorrectIntervals")]
        public string RepeatIntervals { get; set; }
    }

}
