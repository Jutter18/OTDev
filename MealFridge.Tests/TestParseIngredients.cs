using MealFridge.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealFridge.Utils;

namespace MealFridge.Tests
{
    [TestFixture]
    //INside of SearchSpnApi.cs, Line ~51
    public class TestParseIngredients
    {
        [Test]
        public void ParseIngredientEmptyList_ShouldReturn_EmptyList()
        {
            var result = SearchSpnApi.ParseIngredient(null);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ParseIngredientsSingleIngredient_ShouldReturnOneIngredientWithId()
        {
            var ingredientAsJson = new JObject
            {
                { "id", 1002 },
                { "name", "Test Ingredient" },
                { "image", "apple.jpg" }
            };
            var result = SearchSpnApi.ParseIngredient(ingredientAsJson);

            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1002));
        }

        [Test]
        public void ParseIngredientsSingleIngredient_ShouldReturnOneIngredientWithName()
        {
            var ingredientAsJson = new JObject();
            ingredientAsJson.Add("id", 1002);
            ingredientAsJson.Add("name", "Test Ingredient");
            ingredientAsJson.Add("image", "apple.jpg");
            var result = SearchSpnApi.ParseIngredient(ingredientAsJson);

            Assert.That(result.Name, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Test Ingredient"));
        }

        [Test]
        public void ParseIngredientsSingleIngredient_ShouldReturnOneIngredientWithImage()
        {
            var ingredientAsJson = new JObject();
            ingredientAsJson.Add("id", 1002);
            ingredientAsJson.Add("name", "Test Ingredient");
            ingredientAsJson.Add("image", "apple.jpg");
            var result = SearchSpnApi.ParseIngredient(ingredientAsJson);

            Assert.That(result.Image, Is.Not.Null);
            Assert.That(result.Image, Is.EqualTo("https://spoonacular.com/cdn/ingredients_500x500/" + "apple.jpg"));
        }
    }
}