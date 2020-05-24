using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using KnowledgeBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace KnowledgeBase.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly Scheduler _scheduler;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly UserManager<User> _userManager;

        public SubjectsController(ISubjectRepository subjectRepository, ITopicRepository topicRepository, Scheduler scheduler, UserManager<User> userManager)
        {
            _subjectRepository = subjectRepository;
            _topicRepository = topicRepository;
            _scheduler = scheduler;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddRepeat(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = await _topicRepository.GetById(topicId.Value);
                if (!ExistsAndAllowedToUse(topic)) return NotFound();

                _scheduler.AddRepeat(topic, DateTime.Now.AddDays(1));
                await _topicRepository.Update(topic);
                return RedirectToAction(nameof(Topic), new { topicId = topicId });
            }
            else
            {
                return NotFound();
            }
            
        }

        public async Task<IActionResult> CreateSubject()
        {
            Subject newSubject = new Subject() { Name = "", UserId = _userManager.GetUserId(HttpContext.User) };
            await _subjectRepository.Add(newSubject);
            return RedirectToAction(nameof(Subject), new { subjectId = newSubject.Id });
        }

        public async Task<IActionResult> CreateTopic(int? subjectId)
        {
            if (subjectId.HasValue)
            {
                var subject = await _subjectRepository.GetById(subjectId.Value);
                if (!ExistsAndAllowedToUse(subject)) return NotFound();

                Topic newTopic = new Topic() { Name = "", DateLearned = DateTime.Now, SubjectId = subjectId.Value, UserId = _userManager.GetUserId(HttpContext.User) };
                newTopic.DateLearned = newTopic.DateLearned.AddMilliseconds(-newTopic.DateLearned.Millisecond);
                newTopic.DateLearned = newTopic.DateLearned.AddSeconds(-newTopic.DateLearned.Second);



                var userId = _userManager.GetUserId(HttpContext.User);
                User user = await _userManager.FindByIdAsync(userId);
                _scheduler.Schedule(newTopic, user.RepeatIntervals);

                await _topicRepository.Add(newTopic);
                return RedirectToAction(nameof(Topic), new { topicId = newTopic.Id });
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var toDelete = await _subjectRepository.GetById(subjectId);
            if (!ExistsAndAllowedToUse(toDelete)) return NotFound();
            await _subjectRepository.Delete(toDelete);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            var toDelete = await _topicRepository.GetById(topicId);
            if (!ExistsAndAllowedToUse(toDelete)) return NotFound();

            await _topicRepository.Delete(toDelete);
            return RedirectToAction(nameof(Subject), new { subjectId = toDelete.SubjectId });
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int subjectId, string name)
        {
            if (ModelState.IsValid)
            {
                var editedSubject = await _subjectRepository.GetById(subjectId);
                editedSubject.Name = name;
                await _subjectRepository.Update(editedSubject);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/api/GetTopics")]
        public async Task<IActionResult> GetTopics()
        {
            List<TopicLight> lightTopics = new List<TopicLight>();
            var allTopics = await _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            foreach (var topic in allTopics)
            {
                var topicLight = new TopicLight(topic);
                lightTopics.Add(topicLight);
            }
            return Json(lightTopics);
        }

        
        public async Task<IActionResult> Index()
        {
            List<Subject> subjects = await _subjectRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            return View(subjects);
        }

        public async Task<IActionResult> Subject(int? subjectId)
        {
            if (subjectId.HasValue)
            {
                var subject = await _subjectRepository.GetById(subjectId.Value);
                if (!ExistsAndAllowedToUse(subject)) return NotFound();
                return View(subject);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Topic(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = await _topicRepository.GetById(topicId.Value);
                topic.RepeatDates.Sort();
                if (!ExistsAndAllowedToUse(topic)) return NotFound();
                return View(topic);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Topic(Topic newTopic)
        {
            if (ModelState.IsValid)
            {
                await _topicRepository.Update(newTopic);
                return RedirectToAction(nameof(Subject), new { subjectId = newTopic.SubjectId });
            }
            else
            {
                return View(newTopic);
            }
        }
        private bool ExistsAndAllowedToUse(IUserObject userobject)
        {
            return (userobject != null) && (userobject.UserId == _userManager.GetUserId(HttpContext.User));
        }
    }
}