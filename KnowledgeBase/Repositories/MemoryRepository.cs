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
        int _lastAddedThemeId = 0;
        public MemorySubjectRepository()
        {
            subjects = new List<Subject>();
            List<Theme> mathThemes = new List<Theme> {
                new Theme { Id=0, Name = "Trigonometry", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=1},
                new Theme { Id=1, Name = "Calculus", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=1}
                };
            subjects.Add(new Subject {Id=0, Name = "Math", Themes = mathThemes });

            List<Theme> physicsThemes = new List<Theme> {
                new Theme {Id=2, Name = "Magnetism", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0},
                new Theme {Id=3, Name = "Light", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0},
                new Theme {Id=4, Name = "Quantum", DateLearned = DateTime.Today, NextRepeat=DateTime.Today.AddDays(1), TimesRepeated=0}
                };
            subjects.Add(new Subject {Id=1, Name = "Physics", Themes = physicsThemes });
            _lastAddedThemeId = 4;
        }


        public Subject Add(Subject newSubject)
        {
            newSubject.Id = subjects.Count;
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

        public Subject Update(Subject updatedSubject)
        {
            var toUpdate=subjects.Find(sub => sub.Id == updatedSubject.Id);
            toUpdate.Name = updatedSubject.Name;
            if (updatedSubject.Themes[updatedSubject.Themes.Count-1].Id==0)
            {
                updatedSubject.Themes[updatedSubject.Themes.Count - 1].Id = ++_lastAddedThemeId;
            }
            
            return updatedSubject;
        }
    }
}
