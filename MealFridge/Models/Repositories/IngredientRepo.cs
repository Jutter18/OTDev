using MealFridge.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Models.Repositories
{
    public class IngredientRepo: Repository<Ingredient>, IIngredientRepo
    {
        public IngredientRepo(MealFridgeDbContext ctx) : base(ctx) { }

        public List<Ingredient> SearchName(string queryValue)
        {
            return _dbSet.Where(i => i.Name.Contains(queryValue)).ToList();
        }
    }
}
