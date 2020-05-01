using KnowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Repositories
{
    public class MemorySubjectRepository : ISubjectRepository
    {

        List<Subject> subjects;
        public MemorySubjectRepository()
        {
            subjects = new List<Subject>();
            List<Theme> mathThemes = new List<Theme> {
                new Theme { Name = "Trigonometry", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=1},
                new Theme { Name = "Calculus", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=1}
                };
            subjects.Add(new Subject { Name = "Math", Themes = mathThemes });

            List<Theme> physicsThemes = new List<Theme> {
                new Theme { Name = "Magnetism", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0},
                new Theme { Name = "Light", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0},
                new Theme { Name = "Quantum", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0}
                };
            subjects.Add(new Subject { Name = "Physics", Themes = physicsThemes });
        }

        public Subject Add(Subject newSubject)
        {
            subjects.Add(newSubject);
            return newSubject;
        }

        public Subject Delete(Subject subject)
        {
            subjects.Remove(subject);
            return subject;
        }

        public List<Subject> GetAll()
        {
            return subjects;
        }

        public Subject GetById(int id)
        {
            return subjects.Find(sub => sub.Id == id);
        }

        public Subject GetByName(string name)
        {
            return subjects.Find(sub => sub.Name == name);
        }
    }
}
