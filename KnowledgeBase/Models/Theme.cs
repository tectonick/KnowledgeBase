using System;

namespace KnowledgeBase.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateLearned { get; set; }
        public DateTime NextRepeat { get; set; }
        public int TimesRepeated { get; set; }

    }
}
