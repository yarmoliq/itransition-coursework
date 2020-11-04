using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;

using Identity.Models;

namespace coursework_itransition.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly ILogger<HomeController> _logger;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<ApplicationUser> _userManager;


        public AdministratorController(ApplicationDbContext context,
            ILogger<HomeController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Administrator()
        {
            return View(this);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public /*async*/ string RoleFind(string UserID)
        {
            // string rolename = await _userManager.GetRoles(userId).FirstOrDefault();
            // return rolename;
            return "";
        }
    }
}
