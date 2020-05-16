using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;

namespace KnowledgeBase.Models
{

    public class Topic : IUserObject
    {

        [Required(AllowEmptyStrings = true)]

        public List<DateModel> RepeatDates { get; set; }         //Should be always sorted

        public Topic()
        {
            RepeatDates = new List<DateModel>();
        }

        public void CopyFrom(Topic topic)
        {
            if (topic!=null)
            {
                this.Id = topic.Id;
                this.Name = topic.Name;
                this.Description = topic.Description;
                this.Notes = topic.Notes;
                this.DateLearned = topic.DateLearned;
                this.RepeatDates = topic.RepeatDates;
                this.RepeatDates.Sort();
            }            
        }

        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string UserId { get; set; }

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
                var dm = RepeatDates.Where(dt=>dt.Date>DateTime.Now).Min<DateModel>();
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
                int count = 0;
                foreach (var date in RepeatDates)
                {
                    if (DateTime.Compare(date.Date, DateTime.Now) < 0)
                    {
                        count++;
                    }
                }
                
                return count;
            }
        }

    }
}
