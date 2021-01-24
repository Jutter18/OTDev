using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HimalayanExpeditions.Models
{
    public class Search
    {
        [Required]

        public int Year { get; set; }
        public int Count { get; set; }
        public string Peak { get; set; }
        public string Season { get; set; }
        public IEnumerable<Expedition> ExpeditionList { get; set; }
    }
}