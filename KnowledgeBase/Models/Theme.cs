using System;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(100, ErrorMessage ="Name is too big")]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage = "Description is too big")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "First time learned")]
        public DateTime DateLearned { get; set; }

        [Display(Name = "Next repeat")]
        public DateTime NextRepeat { get; set; }
        [Display(Name = "Times repeated")]

        [Range(0, 10000, ErrorMessage = "Invalid number for times repeated")]
        public int TimesRepeated { get; set; }

    }
}
