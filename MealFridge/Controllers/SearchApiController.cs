using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MealFridge.Utils;
using System;
using MealFridge.Models;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MealFridge.Controllers
{
    public class SearchApiController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _searchByNameEndpoint = "https://api.spoonacular.com/recipes/complexSearch";
        private readonly string _searchByIngredientEndpoint = "https://api.spoonacular.com/recipes/findByIngredients";
        private readonly string _searchByRecipeEndpoint = "https://api.spoonacular.com/recipes/{id}/information";
        private readonly string _searchIngredientByNameEndpoint = "https://api.spoonacular.com/food/ingredients/search";
        private readonly string _searchIngredientDetailsEndpoint = "https://api.spoonacular.com/food/ingredients/"; // + {id}/information?amount=1

        private readonly UserManager<IdentityUser> _user;
        private readonly MealFridgeDbContext _db;
        public SearchApiController(IConfiguration config, MealFridgeDbContext context, UserManager<IdentityUser> user)
        {
            _db = context;
            _config = config;
            _user = user;
        }

        [Route("/api/RecipeDetails/{id}")]
        public IActionResult RecipeDetails(string id)
        {

            var apiDetails = new Query
            {
                Credentials = _config["SApiKey"],
                QueryName = "id",
                QueryValue = id,
                Url = _searchByRecipeEndpoint.Replace("{id}", id),
                SearchType = "Details"
            };
            var apiCall = new SearchSpnApi(apiDetails);
            var results = apiCall.SearchAPI().OrderBy(r => r.Id).ToList();
            return Json(results);
        }

    }


}

