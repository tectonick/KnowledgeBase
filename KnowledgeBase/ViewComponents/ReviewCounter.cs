using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.ViewComponents
{
    public class ReviewCounter:ViewComponent
    {
        private ITopicRepository _topicRepository;
        private UserManager<User> _userManager;

        public ReviewCounter(ITopicRepository topicRepository, UserManager<User> userManager)
        {
            _topicRepository = topicRepository;
            _userManager = userManager;
        }
        public string Invoke()
        {
            
            var allTopics = _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            int todayTopicsCount = allTopics.Where(t => (t.NextRepeat>DateTime.MinValue) &&(t.NextRepeat.Date <= DateTime.Now.Date)).Count();
            return todayTopicsCount.ToString();
        }
    }
}
