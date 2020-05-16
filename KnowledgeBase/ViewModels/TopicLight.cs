using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels
{
    public class TopicLight
    {
        public List<DateTime> RepeatDates { get; set; }

        public TopicLight()
        {
            RepeatDates = new List<DateTime>();
        }

        public TopicLight(Topic topic)
        {
            this.Name = topic.Name;
            this.Id = topic.Id;
            this.SubjectId = topic.SubjectId;
            this.Description = topic.Description;
            this.RepeatDates = topic.RepeatDates.Select(el => el.Date).ToList();
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
