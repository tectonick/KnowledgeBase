using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class User : IdentityUser
    {
        public List<Subject> Subjects;
        public List<Topic> Topics;

    }
}
