using KnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Repositories
{
    class DbSubjectRepository : ISubjectRepository
    {
        MyDbContext _dbContext;
        public DbSubjectRepository(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }
        public Subject Add(Subject newSubject)
        {
            _dbContext.Subjects.Add(newSubject);
            _dbContext.SaveChanges();
            return newSubject;
        }

        public Subject Delete(Subject subject)
        {
            _dbContext.Subjects.Remove(subject);
            _dbContext.SaveChanges();
            return subject;
        }

        public List<Subject> GetAll()
        {
            return _dbContext.Subjects.Include(x => x.Themes).ToList();
        }

        public Subject GetById(int id)
        {
            //return _dbContext.Subjects.Find(id)
            return _dbContext.Subjects.Include(x => x.Themes).ToList().Find(a => a.Id == id);
        }

        public Subject GetByName(string name)
        {
            return _dbContext.Subjects.FirstOrDefault(sub => sub.Name == name);
        }

        public Subject Update(Subject updatedSubject)
        {
            _dbContext.Subjects.Update(updatedSubject);
            _dbContext.SaveChanges();
            return updatedSubject;
        }
    }

}
