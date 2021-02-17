using MealFridge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class SearchByNameController : Controller
    {
        private readonly IConfiguration _configuration;
        // GET: SearchByName
        public SearchByNameController(IConfiguration config)
        {
            _configuration = config;
        }
        public ActionResult Index()
        {
            //remove before commit 
            //var secret = _configuration["SpoonacularPAK:PersonalAccessKey"];
            //Debug.WriteLine("Secret: " + secret);
            //SpoonacularAPI api = new SpoonacularAPI("https://api.spoonacular.com/recipes/complexSearch", secret);
            //IEnumerable<Recipe> test = api.SearchAPI("rice");
            //foreach (var i in test)
            //{
            //    Debug.WriteLine(i.Title);
            //}
            return View();
        }
    }
}
