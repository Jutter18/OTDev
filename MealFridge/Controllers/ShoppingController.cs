using MealFridge.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFridgeRepo fridgeRepo;
        private readonly IIngredientRepo ingredientRepo;
        private readonly IRestrictionRepo restrictionRepo;
        private readonly UserManager<IdentityUser> _user;

        public ShoppingController(IConfiguration config, IFridgeRepo fridge, IIngredientRepo ingRepo, IRestrictionRepo resRepo, UserManager<IdentityUser> user)
        {
            _configuration = config;
            fridgeRepo = fridge;
            ingredientRepo = ingRepo;
            restrictionRepo = resRepo;
            _user = user;
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _user.GetUserId(User);
                var userInventory = fridgeRepo.FindByAccount(userId).Where(i => i.NeededAmount > i.Quantity);
                foreach (var ingredient in userInventory)
                    ingredient.Ingred = await ingredientRepo.FindByIdAsync(ingredient.IngredId);
                return await Task.FromResult(View("Index", userInventory.ToList()));
            }
            else
                return await Task.FromResult(RedirectToAction("Index", "Home"));
        }
    }
}
