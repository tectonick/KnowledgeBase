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
        [Route("{controller}/Details/{subjectId}/{themeId}")]
        public ActionResult Theme(int subjectId, int themeId)
        {
            var theme = _subjectRepository.GetById(subjectId).Themes.Find(t => t.Id == themeId);
            return View(theme);
        }

        [HttpPost]
        [Route("{controller}/Details/{subjectId}/{themeId}")]
        public ActionResult Theme(int subjectId, int themeId, string name, string description ,DateTime dateLearned, DateTime nextRepeat, int timesRepeated)
        {
            var theme = _subjectRepository.GetById(subjectId).Themes.Find(t => t.Id == themeId);
            theme.Name = name;
            theme.Description = description;
            theme.DateLearned = dateLearned;
            theme.NextRepeat = nextRepeat;
            theme.TimesRepeated = timesRepeated;
            return View(theme);
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int subjectId)
        {
            return View(_subjectRepository.GetById(subjectId));
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Name)
        {
            try
            {
                var subject = _subjectRepository.GetById(id);
                subject.Name = Name;
                return RedirectToAction(nameof(Details), new { subjectId=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int subjectId)
        {
            var toDelete = _subjectRepository.GetById(subjectId);
            _subjectRepository.Delete(toDelete);

            return RedirectToAction(nameof(Index));
        }
    }
}