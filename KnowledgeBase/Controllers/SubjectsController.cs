using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using KnowledgeBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly IThemeRepository _themeRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IScheduler _scheduler;
        private readonly UserManager<User> _userManager;

        public SubjectsController(ISubjectRepository subjectRepository, IThemeRepository themeRepository, IScheduler scheduler, UserManager<User> userManager)
        {
            _subjectRepository = subjectRepository;
            _themeRepository = themeRepository;
            _scheduler = scheduler;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("/api/GetThemes")]
        public ActionResult GetThemes()
        {
            List<ThemeLight> lightThemes = new List<ThemeLight>();
            var allThemes = _themeRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            foreach (var theme in allThemes)
            {
                var themeLight = new ThemeLight(theme);
                lightThemes.Add(themeLight);
            }
            return Json(lightThemes);
        }


        // GET: Subjects
        public ActionResult Index()
        {
            List<Subject> subjects = _subjectRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            return View(subjects);
        }


        [HttpGet]
        public ActionResult Theme(int themeId)
        {
            var theme = _themeRepository.GetById(themeId);
            theme.RepeatDates.Sort();
            if (!ExistsAndAllowedToUse(theme)) return NotFound();
            return View(theme);
        }

        public  ActionResult AddRepeat(int themeId)
        {
            var theme = _themeRepository.GetById(themeId);
            if (!ExistsAndAllowedToUse(theme)) return NotFound();

            _scheduler.AddRepeat(theme, DateTime.Now.AddDays(1));
            _themeRepository.Update(theme);
            return RedirectToAction(nameof(Theme), new { themeId = themeId });
        }


        [HttpPost]
        public ActionResult Theme(int themeId,Theme newTheme)
        {
            if (ModelState.IsValid)
            {
                _themeRepository.Update(newTheme);
                return RedirectToAction(nameof(Subject), new { subjectId = newTheme.SubjectId });
            }
            else
            {
                return View(themeId);
            }
            
        }

        public ActionResult Subject(int subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            
            if (!ExistsAndAllowedToUse(subject)) return NotFound();

            return View(subject);
        }

        public ActionResult CreateSubject()
        {
            Subject newSubject = new Subject() { Name = "", UserId=_userManager.GetUserId(HttpContext.User) };
            _subjectRepository.Add(newSubject);
            return RedirectToAction(nameof(Subject), new { subjectId = newSubject.Id });
        }

        public ActionResult CreateTheme(int subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            if (!ExistsAndAllowedToUse(subject)) return NotFound();

            Theme newTheme = new Theme() { Name = "", DateLearned=DateTime.Now, SubjectId=subjectId, UserId= _userManager.GetUserId(HttpContext.User) };
            newTheme.DateLearned=newTheme.DateLearned.AddMilliseconds(-newTheme.DateLearned.Millisecond);
            newTheme.DateLearned = newTheme.DateLearned.AddSeconds(-newTheme.DateLearned.Second);
            _scheduler.Schedule(newTheme);

            _themeRepository.Add(newTheme);
            return RedirectToAction(nameof(Theme), new { themeId= newTheme.Id });
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
            if (!ExistsAndAllowedToUse(toDelete)) return NotFound();
            _subjectRepository.Delete(toDelete);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteTheme(int themeId)
        {
            var toDelete = _themeRepository.GetById(themeId);
            if (!ExistsAndAllowedToUse(toDelete)) return NotFound();
            
            
            _themeRepository.Delete(toDelete);
            return RedirectToAction(nameof(Subject), new { subjectId = toDelete.SubjectId });
        }


        private bool ExistsAndAllowedToUse(IUserObject userobject)
        {
            return (userobject != null) && (userobject.UserId == _userManager.GetUserId(HttpContext.User));
        }
    }
}