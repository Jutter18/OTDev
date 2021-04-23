using MealFridge.Models.Interfaces;
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
        public async Task<IActionResult> MealPlan() =>
            await Task.FromResult(PartialView("MealPlan", await _recipeDb.GetMealPlanWeekAsync()));
    }
}