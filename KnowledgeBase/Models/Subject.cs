﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name cannot be empty")]
        [StringLength(100, ErrorMessage = "Name is too big")]
        public string Name { get; set; }

        public void AddTheme(Theme newTheme)
        {
            if (Themes==null)
            {
                Themes = new List<Theme>();
            }
            newTheme.Id = Themes.Count;
            Themes.Add(newTheme);
        }

        public void DeleteTheme(int themeId)
        {
            Themes.Remove(Themes.Find(th=> th.Id==themeId));
        }
        public List<Theme> Themes { get; set; }
    }
}
