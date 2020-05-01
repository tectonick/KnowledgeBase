using System.Collections.Generic;

namespace KnowledgeBase.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
