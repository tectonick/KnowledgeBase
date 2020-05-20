using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IScheduler _scheduler;
        private readonly UserManager<User> _userManager;

        public ReviewController(ISubjectRepository subjectRepository, ITopicRepository topicRepository, IScheduler scheduler, UserManager<User> userManager)
        {
            _subjectRepository = subjectRepository;
            _topicRepository = topicRepository;
            _scheduler = scheduler;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Topic> todayTopics = new List<Topic>();
            var allTopics = _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            todayTopics = allTopics.Where(t => (t.NextRepeat > DateTime.MinValue) && (t.NextRepeat.Date <= DateTime.Now.Date)).ToList();
            return View(todayTopics);
        }

        [HttpGet]
        public ActionResult Topic(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = _topicRepository.GetById(topicId.Value);
                if (!ExistsAndAllowedToUse(topic)) return NotFound();
                return View(topic);
            }
            else
            {
                return NotFound();
            }
            
        }

        public ActionResult DoneRepeat(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = _topicRepository.GetById(topicId.Value);
                if (!ExistsAndAllowedToUse(topic)) return NotFound();
                var repeateDate = topic.RepeatDates.FirstOrDefault(dm => !dm.Repeated);
                if (repeateDate==null)
                {
                    repeateDate.Repeated = true;
                    _topicRepository.Update(topic);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
            
        }

        private bool ExistsAndAllowedToUse(IUserObject userobject)
        {
            return (userobject != null) && (userobject.UserId == _userManager.GetUserId(HttpContext.User));
        }
    }
}