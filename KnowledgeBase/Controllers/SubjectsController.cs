using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers
{
    public class SubjectsController : Controller
    {

        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
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
            var theme = _subjectRepository.GetById(subjectId).Themes.Find(t => t.Id == themeId);
            return View(theme);
        }

        [HttpPost]
        [Route("{controller}/Subject/{subjectId}/{themeId}")]
        public ActionResult Theme(int subjectId, int themeId,Theme newTheme)
        {
            var theme = _subjectRepository.GetById(subjectId).Themes.Find(t => t.Id == themeId);
            if (ModelState.IsValid)
            {
                theme.Name = newTheme.Name;
                theme.Description = newTheme.Description;
                theme.DateLearned = newTheme.DateLearned;
                theme.NextRepeat = newTheme.NextRepeat;
                theme.TimesRepeated = newTheme.TimesRepeated;
                return RedirectToAction(nameof(Subject), new { subjectId = subjectId });
            }
            return View(theme);
        }

        // GET: Subjects/Details/5
        public ActionResult Subject(int subjectId)
        {
            return View(_subjectRepository.GetById(subjectId));
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
            Theme newTheme = new Theme() { Name = "NewTheme", DateLearned=DateTime.Now, NextRepeat=DateTime.Now.AddDays(1) };
            _subjectRepository.GetById(subjectId).AddTheme(newTheme);
            return RedirectToAction(nameof(Theme), new { subjectId= subjectId, themeId= newTheme.Id });
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubject(int id, string Name)
        {
            if (ModelState.IsValid)
            {
                var subject = _subjectRepository.GetById(id);
                subject.Name = Name;
                
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
            _subjectRepository.GetById(subjectId).DeleteTheme(themeId);
            return RedirectToAction(nameof(Subject), new { subjectId = subjectId }); 
        }
    }
}