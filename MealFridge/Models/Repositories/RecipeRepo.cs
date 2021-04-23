using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealFridge.Models;
using MealFridge.Models.Interfaces;
using MealFridge.Models.ViewModels;

namespace MealFridge.Models.Repositories
{
    public class RecipeRepo : Repository<Recipe>, IRecipeRepo
    {
        public RecipeRepo(MealFridgeDbContext ctx) : base(ctx)
        {
        }

        public async Task<Meals> GetMealPlanWeekAsync()
        {
            var meals = new Meals
            {
                Breakfast = Meal.CreateMealsFromRecipes(GetAll()
                .Where(r => r.Breakfast == true)
                .OrderBy(r => Guid.NewGuid())
                .Take(7)
                .ToList()),
                Lunch = Meal.CreateMealsFromRecipes(GetAll()
                .Where(r => r.Lunch == true)
                .OrderBy(r => Guid.NewGuid())
                .Take(7)
                .ToList()),
                Dinner = Meal.CreateMealsFromRecipes(GetAll()
                .Where(r => r.Dinner == true)
                .OrderBy(r => Guid.NewGuid())
                .Take(7)
                .ToList())
            };
            return await Task.FromResult(meals);
        }

        public virtual List<Recipe> GetRandomSix()
        {
            IQueryable<Recipe> recipes = GetAll();
            return recipes
                .OrderBy(r => Guid.NewGuid())
                .Take(6)
                .ToList();
        }
    }
}