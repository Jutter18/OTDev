using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class ShoppingListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
