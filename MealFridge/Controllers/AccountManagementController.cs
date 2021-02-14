using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MealFridge.Controllers
{
    public class AccountManagementController : Controller
    {
        // GET: AccountManagement
        
        public ActionResult AccountInfo() 
        {
            return View();
        }
        public ActionResult DietaryRestrictions()
        {
            return View();
        }
        public ActionResult FoodPreferences() 
        {
            return View();
        }
        public ActionResult FavoriteRecipes ()
        {
            return View();
        }
    }
}