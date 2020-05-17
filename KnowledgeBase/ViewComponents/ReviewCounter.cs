using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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
        public IViewComponentResult Invoke()
        {
            
            var allTopics = _topicRepository.GetAllForUser(_userManager.GetUserId(HttpContext.User));
            var todayTopics= allTopics.Where(t => (t.NextRepeat > DateTime.MinValue) && (t.NextRepeat.Date <= DateTime.Now.Date)).ToList();
            int todayTopicsCount = todayTopics.Count();
            int obsoleteTopicsCount = todayTopics.Where(t => t.NextRepeat < DateTime.Now).Count();
            if (todayTopicsCount>0)
            {
                string badgeClass = (obsoleteTopicsCount > 0) ? "badge-danger" : "badge-primary";
                return new HtmlContentViewComponentResult(
                new HtmlString($"<span class=\"badge badge-pill {badgeClass}\">{todayTopicsCount.ToString()}</span>")
                );
            }
            else
            {
                return new HtmlContentViewComponentResult(
                new HtmlString($"")
                );
                //return new HtmlContentViewComponentResult(
                //new HtmlString($"<span class=\"badge badge-pill badge-success\"><i class=\"fas fa-check\"></i></span>")
                //);
            }

            
        }
    }
}
