using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels
{
    public class ThemeLight
    {
        public List<DateTime> RepeatDates { get; set; }

        public ThemeLight()
        {
            RepeatDates = new List<DateTime>();
        }

        public ThemeLight(Theme theme)
        {
            this.Name = theme.Name;
            this.Id = theme.Id;
            this.SubjectId = theme.SubjectId;
            this.Description = theme.Description;
            this.RepeatDates = theme.RepeatDates.Select(el => el.Date).ToList();
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
