using HimalayanExpeditions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HimalayanExpeditions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HimalayanExpeditionDbContext _context;

        public HomeController(ILogger<HomeController> logger, HimalayanExpeditionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var articles = CreateArticle(_context.Expeditions.Include(p => p.Peak).ToList());
            return View("Index", articles);
        }
        public IActionResult AddExpeditionSubmit(ExpeditionAdder data)
        {
            if(ModelState.IsValid)
            {
                data.Expedition.OxygenUsed = data.OxyCheck;
                data.Expedition.TrekkingAgency = _context.TrekkingAgencies.Where(a => a.Id == data.Expedition.TrekkingAgencyId).FirstOrDefault();
                data.Expedition.Peak = _context.Peaks.Where(p => p.Id == data.Expedition.PeakId).FirstOrDefault();
                _context.Expeditions.Add(data.Expedition);
                _context.SaveChanges();
                return RedirectToAction("AddExpedition");
            }
            return View("AddExpedition");
        }
        public IActionResult AddExpedition()
        {
            ExpeditionAdder data = new ExpeditionAdder();
            data.Peaks = _context.Peaks.OrderBy(a => a.Name);
            data.TrekkingAgencies = _context.TrekkingAgencies.OrderBy(a => a.Name);
            return View(data);
        }

        private IEnumerable<string> CreateArticle(List<Expedition> expeditions)
        {
            var articles = new List<string>();
            expeditions.Reverse();
            foreach (var exp in expeditions)
            {
                var oxygen = (bool)exp.OxygenUsed ? "used oxygen" : "did not use oxygen";
                var succes = exp.TerminationReason.Contains("Success") ? "The expedition was a success!" : "The expedition failed because of " + exp.TerminationReason.ToLower();
                var article = $"In the {exp.Season} of {exp.StartDate.Value.Year}, a team " + $"of expeditioners embarked on their journey to summit {exp.Peak.Name}."
                  + $"During this adventure, the expeditioners {oxygen}. {succes}";
                articles.Add(article);
            }
            return articles;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FindPeaks(Search search)
        {
            if (ModelState.IsValid)
            {
                var expeditionList = _context.Expeditions.Where(s => s.TerminationReason != "Success (main peak)").Include(p => p.Peak).ToList();

                if (search.Season != "Any")
                {
                    expeditionList = expeditionList.Where(s => s.Season == search.Season).ToList();
                }
                expeditionList = expeditionList.GroupBy(s => s.Peak).Select(p => p.First()).ToList();
                search.ExpeditionList = expeditionList;
                search.Count = expeditionList.Count();
                return View("FindPeaks", search);
            }
            else return View("FindPeaks", null);
        }
        public IActionResult Find(){
            return View();
        }
        [HttpGet]
        public IActionResult Find(Search search)
        {
            if (ModelState.IsValid)
            {
                var expeditionList = _context.Expeditions.Include(p => p.Peak).ToList();
                if (search.Year != null)
                {
                    expeditionList = expeditionList.Where(c => c.Year == search.Year).ToList();
                }
                if (search.Peak != null)
                {
                    expeditionList = expeditionList.Where(p => p.Peak.Name.Contains(search.Peak)).ToList();
                }
                if (search.Season != "Any")
                {
                    expeditionList = expeditionList.Where(s => s.Season == search.Season).ToList();
                }
                if (search.TerminationReason != "Any")
                {
                    expeditionList = expeditionList.Where(s => s.TerminationReason == search.TerminationReason).ToList();
                }
                search.ExpeditionList = expeditionList.OrderBy(s => s.StartDate);
                search.Count = expeditionList.Count();
                return View("Find", search);
            }
            else
            {
                return View("Find", null);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
