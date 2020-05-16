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
        private readonly ITopicRepository _topicRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IScheduler _scheduler;
        private readonly UserManager<User> _userManager;

        public SubjectsController(ISubjectRepository subjectRepository, ITopicRepository topicRepository, IScheduler scheduler, UserManager<User> userManager)
        {
            _subjectRepository = subjectRepository;
            _topicRepository = topicRepository;
            _scheduler = scheduler;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("/api/GetTopics")]
        public ActionResult GetTopics()
        {
            List<TopicLight> lightTopics = new List<TopicLight>();
            var allTopics = _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            foreach (var topic in allTopics)
            {
                var topicLight = new TopicLight(topic);
                lightTopics.Add(topicLight);
            }
            return Json(lightTopics);
        }


        // GET: Subjects
        public ActionResult Index()
        {
            List<Subject> subjects = _subjectRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            return View(subjects);
        }


        [HttpGet]
        public ActionResult Topic(int topicId)
        {
            var topic = _topicRepository.GetById(topicId);
            topic.RepeatDates.Sort();
            if (!ExistsAndAllowedToUse(topic)) return NotFound();
            return View(topic);
        }

        public  ActionResult AddRepeat(int topicId)
        {
            var topic = _topicRepository.GetById(topicId);
            if (!ExistsAndAllowedToUse(topic)) return NotFound();

            _scheduler.AddRepeat(topic, DateTime.Now.AddDays(1));
            _topicRepository.Update(topic);
            return RedirectToAction(nameof(Topic), new { topicId = topicId });
        }


        [HttpPost]
        public ActionResult Topic(int topicId,Topic newTopic)
        {
            if (ModelState.IsValid)
            {
                _topicRepository.Update(newTopic);
                return RedirectToAction(nameof(Subject), new { subjectId = newTopic.SubjectId });
            }
            else
            {
                return View(topicId);
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

        public ActionResult CreateTopic(int subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            if (!ExistsAndAllowedToUse(subject)) return NotFound();

            Topic newTopic = new Topic() { Name = "", DateLearned=DateTime.Now, SubjectId=subjectId, UserId= _userManager.GetUserId(HttpContext.User) };
            newTopic.DateLearned=newTopic.DateLearned.AddMilliseconds(-newTopic.DateLearned.Millisecond);
            newTopic.DateLearned = newTopic.DateLearned.AddSeconds(-newTopic.DateLearned.Second);
            _scheduler.Schedule(newTopic);

            _topicRepository.Add(newTopic);
            return RedirectToAction(nameof(Topic), new { topicId= newTopic.Id });
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

        public ActionResult DeleteTopic(int topicId)
        {
            var toDelete = _topicRepository.GetById(topicId);
            if (!ExistsAndAllowedToUse(toDelete)) return NotFound();
            
            
            _topicRepository.Delete(toDelete);
            return RedirectToAction(nameof(Subject), new { subjectId = toDelete.SubjectId });
        }


        private bool ExistsAndAllowedToUse(IUserObject userobject)
        {
            return (userobject != null) && (userobject.UserId == _userManager.GetUserId(HttpContext.User));
        }
    }
}