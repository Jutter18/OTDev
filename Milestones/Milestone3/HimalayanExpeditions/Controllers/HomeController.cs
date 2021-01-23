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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Find(){
            return View();
        }
        [HttpGet]
        public IActionResult Find(Search search)
        {
            if (ModelState.IsValid)
            {
                var temp = search.Year;
                var expeditionList = _context.Expeditions.Where(c => c.Year == temp).Include(p => p.Peak).ToList();
                search.ExpeditionList = expeditionList;
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
