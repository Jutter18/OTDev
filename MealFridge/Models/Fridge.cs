using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MealFridge.Models
{
    [Table("FRIDGE")]
    public partial class Fridge
    {
        [Column("id")]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public int IngredId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey(nameof(FridgeId))]
        [InverseProperty(nameof(Account.Fridges))]
        public virtual Account FridgeNavigation { get; set; }
        [ForeignKey(nameof(IngredId))]
        [InverseProperty(nameof(Ingredient.Fridges))]
        public virtual Ingredient Ingred { get; set; }
    }
}
