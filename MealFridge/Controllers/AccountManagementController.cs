using MealFridge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class AccountManagementController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MealFridgeDbContext _context;
        private readonly UserManager<IdentityUser> _user;

        public AccountManagementController(IConfiguration config, MealFridgeDbContext context, UserManager<IdentityUser> user)
        {
            _configuration = config;
            _context = context;
            _user = user;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RemoveRestrictedIngredient(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var temp = _context.Restrictions.Where(i => i.IngredId == id && i.AccountId == _user.GetUserId(User)).FirstOrDefault();
                if (temp != null)
                {
                    _context.Remove(temp);
                    _context.SaveChanges();
                }
            }
            return await Task.FromResult(RedirectToAction("DietaryRestrictions"));
        }
        public async Task<IActionResult> DietaryRestrictions()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _user.GetUserId(User);
                var userRestrictions = _context.Restrictions.Where(u => u.AccountId == userId && u.Banned == true).ToList();
                foreach (var restriction in userRestrictions)
                {
                    restriction.Ingred = _context.Ingredients.Where(i => i.Id == restriction.IngredId).FirstOrDefault();
                }
                return await Task.FromResult(View("DietaryRestrictions", userRestrictions));
            }
            else
                return await Task.FromResult(RedirectToAction("Index", "Home"));
        }
        public async Task<IActionResult> FoodPreferences()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _user.GetUserId(User);
                var userRestrictions = _context.Restrictions.Where(u => u.AccountId == userId && u.Dislike == true).ToList();
                foreach (var restriction in userRestrictions)
                {
                    restriction.Ingred = _context.Ingredients.Where(i => i.Id == restriction.IngredId).FirstOrDefault();
                }
                return await Task.FromResult(View("FoodPreferences", userRestrictions));
            }
            else
                return await Task.FromResult(RedirectToAction("Index", "Home"));
        }
        public ActionResult FavoriteRecipes()
        {
            return View();
        }
    }
}