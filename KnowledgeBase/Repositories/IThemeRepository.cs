﻿using KnowledgeBase.Models;
using System.Collections.Generic;

namespace KnowledgeBase.Repositories
{
    public interface IThemeRepository
    {
        List<Theme> GetAll();
        Theme GetByName(string name);
        Theme GetById(int id);
        Theme Add(Theme newTheme);
        Theme Update(Theme updatedTheme);
        Theme Delete(Theme theme);
    }

}