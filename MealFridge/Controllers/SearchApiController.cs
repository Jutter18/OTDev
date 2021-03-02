﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MealFridge.Utils;
using System;
using MealFridge.Models;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MealFridge.Controllers
{
    public class SearchApiController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _searchByNameEndpoint = "https://api.spoonacular.com/recipes/complexSearch";
        private readonly string _searchByIngredientEndpoint = "https://api.spoonacular.com/recipes/findByIngredients";
        private readonly string _searchByRecipeEndpoint = "https://api.spoonacular.com/recipes/{id}/information";
        private readonly MealFridgeDbContext _db;
        public SearchApiController(IConfiguration config, MealFridgeDbContext context)
        {
            _db = context;
            _config = config;
        }
        public List<Recipe> SearchByIngredient(string query)
        {
            var ingredient = _db.Ingredients.Where(a => a.Name.Contains(query)).FirstOrDefault();
            var recipesWithIngredient = _db.Recipeingreds.Where(a => a.IngredId == ingredient.Id).Take(10);
            List<Recipe> possibleRecipes = new List<Recipe>();

            if (ingredient != null)
            {
                foreach (var recipeIngred in recipesWithIngredient)
                {
                    possibleRecipes.Add(_db.Recipes.Where(a => a.Id == recipeIngred.RecipeId).FirstOrDefault());
                }
            }

            if (possibleRecipes.Count < 10)
            {
                var apiQuerier = new SearchSpnApi(new Query
                {
                    Credentials = _config["SApiKey"],
                    QueryName = "ingredients",
                    QueryValue = query,
                    Url = _searchByIngredientEndpoint,
                    SearchType = "Ingredient"
                });
                possibleRecipes = apiQuerier.SearchAPI();
                if (possibleRecipes == null)
                {
                    possibleRecipes = new List<Recipe>();
                }
                foreach (var recipe in possibleRecipes)
                {
                    if (!_db.Recipes.Any(t => t.Id == recipe.Id))
                    {
                        _db.Recipes.Add(recipe);
                    }
                }
                _db.SaveChanges();
            }
            return possibleRecipes;
        }

        [Route("api/SearchByName/{query}/{type}")]
        public IActionResult SearchByName(string query, string type)
        {
            if (type == "Ingredient")
            {
                var possibleRecipesByIngredient = SearchByIngredient(query);
                return Json(possibleRecipesByIngredient.OrderBy(r => r.Id).ToList());
            }
            var possibleRecipes = _db.Recipes
                .Where(r => r.Title.Contains(query))
                .OrderBy(p => p.Id)
                .Take(10)
                .ToList();
            if (possibleRecipes.Count < 10)
            {
                var apiQuerier = new SearchSpnApi(
                    new Query
                    {
                        Credentials = _config["SApiKey"],
                        QueryName = "query",
                        QueryValue = query,
                        Url = _searchByNameEndpoint,
                        SearchType = "Recipe"
                    }
                );
                possibleRecipes = apiQuerier.SearchAPI();
                foreach (var recipe in possibleRecipes)
                {
                    if (!_db.Recipes.Any(t => t.Id == recipe.Id))
                    {
                        _db.Recipes.Add(recipe);
                    }
                }
                _db.SaveChanges();
            }

            return Json(possibleRecipes.OrderBy(r => r.Id).ToList());
        }
        [Route("/api/RecipeDetails/{id}")]
        public IActionResult RecipeDetails(string id)
        {
            if (!int.TryParse(id, out var rid))
                return StatusCode(400);
            if (!_db.Recipes.Any(recipe => recipe.Recipeingreds == null && recipe.Id == rid))
            {
                var apiDetails = new Query
                {
                    Credentials = _config["SApiKey"],
                    QueryName = "id",
                    QueryValue = id,
                    Url = _searchByRecipeEndpoint.Replace("{id}", id)
                };
                var querier = new SearchSpnApi(apiDetails);
                return Json(querier.SearchAPI().OrderBy(r => r.Id).ToList());
            }
        }
    }
}
