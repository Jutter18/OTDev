using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MealFridge.Utils;
using System;
using MealFridge.Models;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MealFridge.Controllers
{
    public class SearchApiController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _user;
        private readonly MealFridgeDbContext _db;
        public SearchApiController(IConfiguration config, MealFridgeDbContext context, UserManager<IdentityUser> user)
        {
            _db = context;
            _config = config;
            _user = user;
        }

    }


}

