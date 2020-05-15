using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Subject:IUserObject
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [Required(ErrorMessage ="Name cannot be empty")]
        [StringLength(100, ErrorMessage = "Name is too big")]
        public string Name { get; set; }

        public void AddTheme(Theme newTheme)
        {
            if (Themes==null)
            {
                Themes = new List<Theme>();
            }
            newTheme.Id = 0;
            Themes.Add(newTheme);
        }

        public void DeleteTheme(int themeId)
        {
            Themes.Remove(Themes.Find(th=> th.Id==themeId));
        }
        public List<Theme> Themes { get; set; }
    }
}
