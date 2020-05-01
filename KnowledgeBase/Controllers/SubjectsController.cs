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

        // GET: Subjects/Details/5
        public ActionResult Details(int id)
        {
            return View(_subjectRepository.GetById(id));
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
                return RedirectToAction(nameof(Details), new { Id=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            var toDelete = _subjectRepository.GetById(id);
            _subjectRepository.Delete(toDelete);

            return RedirectToAction(nameof(Index));
        }
    }
}