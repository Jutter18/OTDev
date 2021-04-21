using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Models.Interfaces
{
    public interface IRestrictionRepo: IRepository<Restriction>
    {
        Task<List<Ingredient>> RemoveRestrictions(List<Ingredient> ingredients);
    }
}
