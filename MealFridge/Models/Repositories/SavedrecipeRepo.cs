﻿using System;
using MealFridge.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MealFridge.Models;
using MealFridge.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using MealFridge.Utils;


namespace MealFridge.Models.Repositories
{
    public class SavedrecipeRepo : Repository<Savedrecipe>, ISavedrecipeRepo
    {
        public SavedrecipeRepo(MealFridgeDbContext ctx) : base(ctx) { }

        public List<Savedrecipe> FindAccount(string userId, IQueryable<Savedrecipe> other)
        {
            var temp = other.Where(a => a.AccountId == userId).ToList();
            return temp;
        }
        

        public void RemoveSavedRecipe(Savedrecipe recipe)
        {
            _context.Remove(recipe);
            _context.SaveChanges();
        }
        public Savedrecipe Savedrecipe(string userId, int recipeId)
        {
            return _dbSet.Where(r => r.AccountId == userId && r.RecipeId == recipeId).FirstOrDefault();
        }
        public List<Savedrecipe> GetShelvedRecipe(string userId, IQueryable<Savedrecipe> other)
        {
            return other.Where(a => a.AccountId == userId && a.Shelved == true).ToList();
        }
       
        //Should not touch/refactor this one for testing, it is being used for other things 
        public List<Savedrecipe> GetFavoritedRecipe(string userId)
        {
            IQueryable<Savedrecipe> other = GetAll();
            return other.Where(a => a.AccountId == userId && a.Favorited == true).Include(r => r.Recipe).ToList();
        }

        public List<Savedrecipe> GetFavoritedRecipeWithIQueryable(string userId, IQueryable<Savedrecipe> other)
        {
            return other.Where(a => a.AccountId == userId && a.Favorited == true).ToList();
        }
        public List<Savedrecipe> GetAllRecipes(string userId, IQueryable<Savedrecipe> other)
        {
            return other.Where(a => a.AccountId == userId).Include(r => r.Recipe).ToList();
        }

        public Savedrecipe CreateNewSavedRecipe(Recipe recipe, string userId)
        {
            return new Savedrecipe { AccountId = userId, Recipe = recipe, RecipeId = recipe.Id, Favorited = null, Shelved = null };
        }
       
    }
}

        