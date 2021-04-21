using MealFridge.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Models.Repositories
{
    public class RestrictionRepo: Repository<Restriction>, IRestrictionRepo
    {
        public RestrictionRepo(MealFridgeDbContext ctx) : base(ctx) { }

        public async Task<List<Ingredient>> RemoveRestrictions(List<Ingredient> ingredients)
        {
            var t = new List<Ingredient>();
            foreach(var i in ingredients)
            {
                if (await ExistsAsync(i.Id)) { }
                else
                    t.Add(i);
            }
            return t;
        }
    }
}
