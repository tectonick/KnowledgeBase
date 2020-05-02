using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Theme
    {
        public List<DateTime> RepeatDates;


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(100, ErrorMessage ="Name is too big")]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage = "Description is too big")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "First time learned")]


        public DateTime DateLearned
        {
            get => RepeatDates[0];
            set
            {
                if (RepeatDates==null)
                {
                    RepeatDates = new List<DateTime>();
                }
                if (RepeatDates.Count==0)
                {
                    RepeatDates.Add(value);
                }
            }
                
                
                
        }

        public DateTime NextRepeat
        {
            get => RepeatDates.Find(date => DateTime.Compare(date, DateTime.Now) >= 0);
        }

        public int TimesRepeated {
            get => RepeatDates.FindLastIndex(date => DateTime.Compare(date, DateTime.Now) < 0);                
        }

    }
}
