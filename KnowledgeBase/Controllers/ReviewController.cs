using KnowledgeBase.Logic;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            List<Topic> todayTopics = new List<Topic>();
            var allTopics = await _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            todayTopics = allTopics.Where(t => (t.NextRepeat > DateTime.MinValue) && (t.NextRepeat.Date <= DateTime.Now.Date)).ToList();
            return View(todayTopics);
        }

        [HttpGet]
        public async Task<IActionResult> Topic(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = await _topicRepository.GetById(topicId.Value);
                if (!ExistsAndAllowedToUse(topic)) return NotFound();
                return View(topic);
            }
            else
            {
                return NotFound();
            }
            
        }

        public async Task<IActionResult> DoneRepeat(int? topicId)
        {
            if (topicId.HasValue)
            {
                var topic = await _topicRepository.GetById(topicId.Value);
                if (!ExistsAndAllowedToUse(topic)) return NotFound();
                var repeateDate = topic.RepeatDates.FirstOrDefault(dm => !dm.Repeated);
                if (repeateDate!=null)
                {
                    repeateDate.Repeated = true;
                    await _topicRepository.Update(topic);
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