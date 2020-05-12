using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;

namespace KnowledgeBase.Models
{
    public class Theme
    {
        //Sorted list
        [Required(AllowEmptyStrings = true)]

        public List<DateModel> RepeatDates { get; set; }

        public Theme()
        {
            RepeatDates = new List<DateModel>();
        }

        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [StringLength(100, ErrorMessage ="Name is too big")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description is too big")]
        public string Description { get; set; }


       // [StringLength(10000, ErrorMessage = "Notes are too big")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "First time learned")]
        public DateTime DateLearned { get; set; }

        public DateTime NextRepeat
        {
            get{
                var dm = RepeatDates.Find(date => DateTime.Compare(date.Date, DateTime.Now) >= 0);
                if (dm!=null)
                {
                    return dm.Date;
                }
                else
                {
                    return DateTime.MinValue;
                }
               }
        }

        public int TimesRepeated {
            get
            {
                var lastIndex=RepeatDates.FindLastIndex(date => DateTime.Compare(date.Date, DateTime.Now) < 0);
                return (lastIndex == -1) ? 0 : (lastIndex+1);
            }
        }

    }
}
