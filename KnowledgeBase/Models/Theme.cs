using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Theme
    {
        //Sorted list
        //The first date is the date of learning, other dates are scheduled repeats
        [Required(AllowEmptyStrings =true)]
        public List<DateTime> RepeatDates { get; set; }

        public Theme()
        {
            RepeatDates = new List<DateTime>();
        }

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

        public DateTime NextRepeat
        {
            get => RepeatDates.Find(date => DateTime.Compare(date, DateTime.Now) >= 0);
        }

        public int TimesRepeated {
            get => RepeatDates.FindLastIndex(date => DateTime.Compare(date, DateTime.Now) < 0);                
        }

    }
}
