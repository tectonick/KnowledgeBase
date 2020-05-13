//using KnowledgeBase.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace KnowledgeBase.Repositories
//{
//    public class MemorySubjectRepository : ISubjectRepository
//    {

//        List<Subject> subjects;
//        int _lastAddedThemeId = 0;
//        public MemorySubjectRepository()
//        {
//            subjects = new List<Subject>();
//            List<Theme> mathThemes = new List<Theme> {
//                new Theme { Id=0, Name = "Trigonometry", DateLearned = DateTime.Today, SubjectId=0},
//                new Theme { Id=1, Name = "Calculus", DateLearned = DateTime.Today,SubjectId=0}
//                };
//            subjects.Add(new Subject {Id=0, Name = "Math", Themes = mathThemes });

//            List<Theme> physicsThemes = new List<Theme> {
//                new Theme {Id=2, Name = "Magnetism", DateLearned = DateTime.Today, SubjectId=1},
//                new Theme {Id=3, Name = "Light", DateLearned = DateTime.Today, SubjectId=1},
//                new Theme {Id=4, Name = "Quantum", DateLearned = DateTime.Today, SubjectId=1}
//                };
//            subjects.Add(new Subject {Id=1, Name = "Physics", Themes = physicsThemes });
//            _lastAddedThemeId = 4;
//        }


//        public Subject Add(Subject newSubject)
//        {
//            newSubject.Id = subjects.Count;
//            subjects.Add(newSubject);
//            return newSubject;
//        }

//        public Subject Delete(Subject subject)
//        {
//            subjects.Remove(subject);
//            return subject;
//        }

//        public List<Subject> GetAll()
//        {
//            return subjects;
//        }

//        public Subject GetById(int id)
//        {
//            return subjects.Find(sub => sub.Id == id);
//        }

//        public Subject GetByName(string name)
//        {
//            return subjects.Find(sub => sub.Name == name);
//        }

//        public Subject Update(Subject updatedSubject)
//        {
//            var toUpdate=subjects.Find(sub => sub.Id == updatedSubject.Id);
//            toUpdate.Name = updatedSubject.Name;

//            //All this code to match EF behavior with auto added id`s
//            if ((updatedSubject.Themes!=null)&&(updatedSubject.Themes.Count!=0))
//            {
//                if (updatedSubject.Themes[updatedSubject.Themes.Count - 1].Id == 0)
//                {
//                    updatedSubject.Themes[updatedSubject.Themes.Count - 1].Id = ++_lastAddedThemeId;
//                }
//            }            
//            return updatedSubject;
//        }
//    }

//    public class MemoryThemeRepository : IThemeRepository
//    {
//        ISubjectRepository _subjectRepository;
//        public MemoryThemeRepository(ISubjectRepository subjectRepository)
//        {
//            _subjectRepository = subjectRepository;
//        }
//        public Theme Add(Theme newTheme)
//        {
//            var subject = _subjectRepository.GetById(newTheme.SubjectId);
//            if (subject.Themes==null)
//            {
//                subject.Themes = new List<Theme>();
//            }
//            subject.Themes.Add(newTheme);
//            _subjectRepository.Update(subject);
//            return newTheme;
//        }

//        public Theme Delete(Theme theme)
//        {
//            var subject = _subjectRepository.GetById(theme.SubjectId);
//            subject.Themes.Remove(theme);
//            return theme;
//        }

//        public List<Theme> GetAll()
//        {
//            var allSubjects= _subjectRepository.GetAll();
//            List<Theme> allThemes = new List<Theme>();
//            foreach (var subject in allSubjects)
//            {
//                allThemes.AddRange(subject.Themes);
//            }
//            return allThemes;
//        }

//        public Theme GetById(int id)
//        {
//            var allSubjects = _subjectRepository.GetAll();
//            foreach (var subject in allSubjects)
//            {
//                var found = subject.Themes.Find(th => th.Id == id);
//                if (found!=null)
//                {
//                    return found;
//                }                
//            }
//            return null;            
//        }

//        public Theme GetByName(string name)
//        {
//            var allSubjects = _subjectRepository.GetAll();
//            foreach (var subject in allSubjects)
//            {
//                var found = subject.Themes.Find(th => th.Name == name);
//                if (found != null)
//                {
//                    return found;
//                }
//            }
//            return null;
//        }

//        public Theme Update(Theme updatedTheme)
//        {
//            var subject = _subjectRepository.GetById(updatedTheme.SubjectId);
//            var theme = subject.Themes.Find(th => th.Id == updatedTheme.Id);
//            theme = updatedTheme;
//            return updatedTheme;
//        }
//    }

//}
