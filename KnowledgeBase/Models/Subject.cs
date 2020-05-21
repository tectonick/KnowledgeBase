using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Subject:IUserObject
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="RequiredName")]
        [StringLength(100, ErrorMessage = "BigName")]
        public string Name { get; set; }

        public void AddTopic(Topic newTopic)
        {
            if (Topics==null)
            {
                Topics = new List<Topic>();
            }
            newTopic.Id = 0;
            Topics.Add(newTopic);
        }

        public void DeleteTopic(int topicId)
        {
            Topics.Remove(Topics.Find(th=> th.Id==topicId));
        }
        public List<Topic> Topics { get; set; }
    }
}
