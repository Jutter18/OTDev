using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MealFridge.Utils;
using System;

namespace MealFridge.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _searchByNameEndpoint = "https://api.spoonacular.com/recipes/complexSearch";
        public SearchController(IConfiguration config)
        {
            _config = config;
        }

        [Route("api/SearchByName/{query}")]
        public IActionResult SearchByName(string query)
        {
            var apiQuerier = new SearchSpnApi(_searchByNameEndpoint, _config["SApiKey"]);
            var results = apiQuerier.SearchAPI(query);
            return Json(results);
        }
    }
}
