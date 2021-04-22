using MealFridge.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Models.Repositories
{
    public class FridgeRepo: Repository<Fridge>, IFridgeRepo
    {
        public FridgeRepo(MealFridgeDbContext ctx) : base(ctx) { }

        public async Task<bool> ExistsAsync(string userId, int ingredId)
        {
            return await FindByIdAsync(userId, ingredId) != null;
        }

        public async Task<Fridge> FindByIdAsync(string UserId, int ingredId)
        {
            return await _dbSet.FindAsync(UserId, ingredId);
        }

        public List<Fridge> FindByAccount(string userId)
        {
            return _dbSet.Where(f => f.AccountId == userId).ToList();
        }

        public List<Ingredient> FindByIngredient(int ingredId)
        {
            var ingreds = new List<Ingredient>();
            foreach(var i in _dbSet.Where(i => i.IngredId == ingredId))
            {
                ingreds.Add(i.Ingred);
            }
            return ingreds;
        }

        public async Task AddAsync(Fridge fridgeIngredient)
        {
            if (fridgeIngredient == null)
            {
                throw new ArgumentNullException("Entity must not be null to add or update");
            }
            _context.Update(fridgeIngredient);
            _context.SaveChanges();
        }
    }
}
