using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KnowledgeBase.Models;
using KnowledgeBase.Repositories;
using System.Globalization;

namespace KnowledgeBase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //List<DateTime> RepeatDates=new List<DateTime>();
            //var subjects=_subjectRepository.GetAll();
            //foreach (var subject in subjects)
            //{
            //    foreach (var theme in subject.Themes)
            //    {
            //        if (theme.RepeatDates!=null)
            //        {
            //            RepeatDates.AddRange(theme.RepeatDates);
            //        }
            //    }
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
