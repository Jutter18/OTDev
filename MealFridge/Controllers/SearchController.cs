﻿using MealFridge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MealFridge.Controllers
{
    public class SearchController : Controller
    {

        private readonly IConfiguration _configuration;
        // GET: SearchByName
        public SearchController(IConfiguration config)
        {
            _configuration = config;
        }
        public IActionResult Index()
        {

            return View();
        }

    }
}
