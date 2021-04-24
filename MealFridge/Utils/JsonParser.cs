﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MealFridge.Models;
using MealFridge.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace MealFridge.Utils
{
    public static class JsonParser
    {
        public static void ParseNutrition(List<JToken> nutrition, IFoodItem ingredient)
        {
            foreach (var n in nutrition)
            {
                if (n["name"].ToString() == "Calories")
                {
                    ingredient.Calories = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Saturated Fat")
                {
                    ingredient.SatFat = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Fat")
                {
                    ingredient.TotalFat = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Net Carbohydrates")
                {
                    ingredient.NetCarbs = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Carbohydrates")
                {
                    ingredient.Carbs = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Cholesterol")
                {
                    ingredient.Cholesterol = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Sodium")
                {
                    ingredient.Sodium = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Protein")
                {
                    ingredient.Protein = (float)n["amount"];
                }
                else if (n["name"].ToString() == "Sugar")
                {
                    ingredient.Sugar = (float)n["amount"];
                }
                else
                {
                    Console.WriteLine("Skipped: " + (string)n["name"]);
                }
            }
        }

        public static ICollection<Recipeingred> GetIngredients(JArray ingredients, int recipeId, List<Ingredient> list) //Can Test whole function
        {
            var retingredients = new List<Recipeingred>();
            foreach (var ing in ingredients)
            {
                if (!int.TryParse(ing["id"].ToString(), out int ingId))
                    continue;
                if (retingredients.Any(i => i.IngredId == ingId))
                {
                    retingredients.First(i => i.IngredId == ingId).Amount += ing["amount"]?.Value<double>();
                    continue;
                }
                var newRI = new Recipeingred
                {
                    RecipeId = recipeId,
                    IngredId = ingId,
                    Amount = ing["amount"]?.Value<double>(),
                    ServingUnit = ing["unit"]?.Value<string>(),
                    Ingred = list.FirstOrDefault(i => i.Id == ingId)
                };
                var nutrients = ing["nutrients"].ToList();
                JsonParser.ParseNutrition(nutrients, newRI);
                retingredients.Add(newRI);
            }
            return retingredients;
        }

        public static void ParseDishType(List<JToken> list, Recipe detailedRecipe)
        {
            if (list == null || list.Count < 1)
                return;
            if (detailedRecipe == null || detailedRecipe.Id == 0)
                return;
            List<string> types = new List<string>();
            list.ForEach(t => types.Add(t.Value<string>().ToLower()));
            detailedRecipe.Breakfast = false;
            detailedRecipe.Lunch = false;
            detailedRecipe.Dinner = false;
            detailedRecipe.Dessert = false;
            detailedRecipe.Snack = false;

            if (types.Contains("breakfast"))
                detailedRecipe.Breakfast = true;
            if (types.Contains("lunch"))
                detailedRecipe.Lunch = true;
            if (types.Contains("dinner") || types.Contains("supper"))
                detailedRecipe.Dinner = true;
            if (types.Contains("dessert"))
                detailedRecipe.Dessert = true;
            if (types.Contains("snack"))
                detailedRecipe.Snack = true;
        }

        public static List<Ingredient> IngredientList(JArray ingredients)
        {
            var result = new List<Ingredient>();
            foreach (var ing in ingredients)
            {
                if (ing["id"] == null || ing["id"].Type == JTokenType.Null)
                    continue;
                else
                {
                    Ingredient Ingred = new Ingredient();

                    Ingred.Name = ing["name"]?.Value<string>();
                    Ingred.Id = ing["id"].Value<int>();
                    Ingred.Image = ing["image"].Value<string>();
                    Ingred.Aisle = ing["aisle"].Value<string>();

                    result.Add(Ingred);
                }
            }
            return result;
        }
    }
}