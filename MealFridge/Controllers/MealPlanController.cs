using MealFridge.Models;
using MealFridge.Models.Interfaces;
using MealFridge.Models.ViewModels;
using MealFridge.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class MealPlanController : Controller
    {
        private IRecipeRepo _recipeDb;

        public MealPlanController(IRecipeRepo ctx)
        {
            _recipeDb = ctx;
        }

        public async Task<IActionResult> Index() =>
            await Task.FromResult(View());

        [HttpPost]
        public async Task<IActionResult> MealPlan()
        {
            var meals = new Meals
            {
                Breakfast = Meal.CreateMealsFromRecipes(_recipeDb.GetAll()
               .Where(r => r.Breakfast == true)
               .OrderBy(r => Guid.NewGuid())
               .Take(7)
               .ToList()),
                Lunch = Meal.CreateMealsFromRecipes(_recipeDb.GetAll()
               .Where(r => r.Lunch == true)
               .OrderBy(r => Guid.NewGuid())
               .Take(7)
               .ToList()),
                Dinner = Meal.CreateMealsFromRecipes(_recipeDb.GetAll()
               .Where(r => r.Dinner == true)
               .OrderBy(r => Guid.NewGuid())
               .Take(7)
               .ToList()),
                Days = DatesGenerator.GetDays(7)
            };
            return await Task.FromResult(PartialView("MealPlan", meals));
        }
    }
}