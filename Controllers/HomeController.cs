﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;

using Identity.Models;

namespace coursework_itransition.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly ILogger<HomeController> _logger;
        public readonly RoleManager<IdentityRole> _roleManager;
        
        public HomeController(ApplicationDbContext context,
            ILogger<HomeController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
        }

        public bool CurrentUserIsAuthorOf(Composition c)
        {
            if(this.User == null)
                return false;
            
            var x = this.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if(x == null)
                return false;

            return x.Value == c.AuthorID;
        }

        public IActionResult Index()
        {
            return View(this);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
