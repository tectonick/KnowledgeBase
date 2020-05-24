using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class User : IdentityUser
    {

        
        public List<Subject> Subjects;
        public List<Topic> Topics;

        //"1 5 25 120 730"
        [Display(Name = "DefaultIntervals")]
        [DataType(DataType.Text)]
        public string RepeatIntervals { get; set; }

    }
}
