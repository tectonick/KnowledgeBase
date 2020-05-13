﻿using KnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Repositories
{
    class DbThemeRepository : IThemeRepository
    {
        MyDbContext _dbContext;
        public DbThemeRepository(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        public Theme Add(Theme newTheme)
        {
            _dbContext.Themes.Add(newTheme);
            _dbContext.SaveChanges();
            return newTheme;
        }

        public Theme Delete(Theme theme)
        {
            _dbContext.Themes.Remove(theme);
            _dbContext.SaveChanges();
            return theme;
        }

        public List<Theme> GetAll()
        {
            return _dbContext.Themes.Include(th => th.RepeatDates).ToList();
        }

        public Theme GetById(int id)
        {
            return _dbContext.Themes.Include(th => th.RepeatDates).ToList().Find(a => a.Id == id);
        }

        public Theme GetByName(string name)
        {
            return _dbContext.Themes.Include(th => th.RepeatDates).FirstOrDefault(th => th.Name == name);
        }

        public Theme Update(Theme updatedTheme)
        {
            //var theme = _dbContext.Themes.Include(th => th.RepeatDates).First(th => th.Id == updatedTheme.Id);
            //theme.RepeatDates = updatedTheme.RepeatDates.Select(t => t).ToList();

            //  _dbContext.Themes.Update(updatedTheme);
            var oldTheme = _dbContext.Themes.Include(th => th.RepeatDates).ToList().Find(a => a.Id == updatedTheme.Id);
            var datesToDelete = oldTheme.RepeatDates.Select(rd => rd).Except(updatedTheme.RepeatDates);
            foreach (var date in datesToDelete)
            {
                _dbContext.DateModels.Remove(date);
            }
            //oldTheme.RepeatDates.AddRange(updatedTheme.RepeatDates);
            oldTheme.CopyFrom(updatedTheme);


            //_dbContext.Themes.Update(updatedTheme);
            //_dbContext.SaveChanges();
            //return updatedTheme;

            //var theme = _dbContext.Themes.Include(th => th.RepeatDates).First(th => th.Id == updatedTheme.Id);
            //theme.CopyFrom(updatedTheme);

            //_dbContext.Themes.Update(theme);            
            //_dbContext.Entry(theme).CurrentValues.SetValues(updatedTheme);
            _dbContext.SaveChanges();
            return updatedTheme;
        }

    }

}