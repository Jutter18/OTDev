using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MealFridge.Models
{
    [Table("MEAL")]
    public partial class Meal
    {
        public string AccountId { get; set; }
        public DateTime Day { get; set; }
        [Column("recipe_id")]
        public int? RecipeId { get; set; }
        [Column("meal")]
        [StringLength(255)]
        public string Meal1 { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Meals")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(RecipeId))]
        [InverseProperty("Meals")]
        public virtual Recipe Recipe { get; set; }
    }
}
