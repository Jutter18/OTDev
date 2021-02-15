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
        // GET: SearchByName
        public ActionResult Index()
        {
            return View();
        }
    }
}
