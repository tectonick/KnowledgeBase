using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewModels
{
    public class TopicLight
    {
        public List<DateModel> RepeatDates { get; set; }

        public TopicLight()
        {
            RepeatDates = new List<DateModel>();
        }

        public TopicLight(Topic topic)
        {
            this.Name = topic.Name;
            this.Id = topic.Id;
            this.SubjectId = topic.SubjectId;
            this.RepeatDates = topic.RepeatDates;
        }

        public int Id { get; set; }
        public int SubjectId { get; set; }

        public string Name { get; set; }
    }
}
