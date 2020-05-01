using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name cannot be empty")]
        [StringLength(100, ErrorMessage = "Name is too big")]
        public string Name { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
