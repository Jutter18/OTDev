﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MealFridge.Models
{
    [Table("RESTRICTIONS")]
    public partial class Restriction
    {
        public string AccountId { get; set; }
        public int IngredId { get; set; }
        [Column("dislike")]
        public bool? Dislike { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Restrictions")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(IngredId))]
        [InverseProperty(nameof(Ingredient.Restrictions))]
        public virtual Ingredient Ingred { get; set; }
    }
}
