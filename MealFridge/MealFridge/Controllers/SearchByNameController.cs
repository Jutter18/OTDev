using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class SearchByNameController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(string recipeName)
        {

            return View();
        }
    }
}
