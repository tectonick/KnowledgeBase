using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers
{
    public class SubjectsController : Controller
    {

        private readonly ISubjectRepository _subjectRepository;
        private readonly IScheduler _scheduler;

        public SubjectsController(ISubjectRepository subjectRepository, IScheduler scheduler)
        {
            _subjectRepository = subjectRepository;
            _scheduler = scheduler;
        }
        // GET: Subjects
        public ActionResult Index()
        {
            List<Subject> subjects = _subjectRepository.GetAll();
            return View(subjects);
        }


        [HttpGet]
        [Route("{controller}/Subject/{subjectId}/{themeId}")]
        public ActionResult Theme(int subjectId, int themeId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            var theme = subject.Themes.Find(t => t.Id == themeId);
            return View(theme);
        }

        //[HttpGet]
        ////[Route("{controller}/Subject/{subjectId}/{themeId}/Delete/{timeIndex}")]
        //public ActionResult DeleteRepeatTime(Theme mytheme, int timeIndex)
        //{
        //    mytheme.RepeatDates.RemoveAt(timeIndex);
        //    return RedirectToAction(nameof(Theme), new { themeId = mytheme.Id });
        //}

        public  ActionResult AddRepeat(int subjectId, int themeId)
        {
            var theme=_subjectRepository.GetById(subjectId).Themes.Find(th => th.Id == themeId);
            _scheduler.AddRepeat(theme, DateTime.Now.AddDays(1));
            return RedirectToAction(nameof(Theme), new { themeId = themeId, subjectId= subjectId });
        }


        [HttpPost]
        [Route("{controller}/Subject/{subjectId}/{themeId}")]
        public ActionResult Theme(int subjectId, int themeId,Theme newTheme)
        {
            var editedSubject = _subjectRepository.GetById(subjectId);
            var theme = editedSubject.Themes.Find(t => t.Id == themeId);
            if (ModelState.IsValid)
            {
                theme.Name = newTheme.Name;
                theme.Description = newTheme.Description;
                theme.Notes = newTheme.Notes;
                theme.DateLearned = newTheme.DateLearned;
                theme.RepeatDates = newTheme.RepeatDates;
                theme.RepeatDates.Sort();
           
                _subjectRepository.Update(editedSubject);

                return RedirectToAction(nameof(Subject), new { subjectId = subjectId });
            }
            return View(theme);
        }

        // GET: Subjects/Details/5
        public ActionResult Subject(int subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult CreateSubject()
        {
            Subject newSubject = new Subject() { Name = "NewSubject" };
            _subjectRepository.Add(newSubject);
            return RedirectToAction(nameof(Subject), new { subjectId = newSubject.Id });
        }

        // GET: Subjects/Create
        public ActionResult CreateTheme(int subjectId)
        {
            Theme newTheme = new Theme() { Name = "NewTheme", DateLearned=DateTime.Now, SubjectId=subjectId };
            var editedSubject=_subjectRepository.GetById(subjectId);
            _scheduler.Schedule(newTheme);
            editedSubject.AddTheme(newTheme);            
            _subjectRepository.Update(editedSubject);
            return RedirectToAction(nameof(Theme), new { subjectId= subjectId, themeId= newTheme.Id });
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject(int subjectId, string name)
        {
            if (ModelState.IsValid)
            {
                var editedSubject=_subjectRepository.GetById(subjectId);
                editedSubject.Name = name;
                _subjectRepository.Update(editedSubject);
                
            }
              return RedirectToAction(nameof(Index));  
        }

        // GET: Subjects/Delete/5
        public ActionResult DeleteSubject(int subjectId)
        {
            var toDelete = _subjectRepository.GetById(subjectId);
            _subjectRepository.Delete(toDelete);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteTheme(int subjectId, int themeId)
        {
           
            var editedSubject = _subjectRepository.GetById(subjectId);
            editedSubject.DeleteTheme(themeId);
            _subjectRepository.Update(editedSubject);

            return RedirectToAction(nameof(Subject), new { subjectId = subjectId }); 
        }
    }
}