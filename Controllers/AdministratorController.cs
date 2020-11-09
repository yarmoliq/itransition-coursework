using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Identity.Models;
using System.Linq; 

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

        
        // public async Task<string> RoleFind(string UserID)
        // {
        //     var u = await _context.Users.FindAsync(UserID);
        //     var lrn = await _userManager.GetRolesAsync(u);
        //     return lrn[0];
        // }

        [HttpPost, Route("Administrator/DeleteUser")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(string UserID)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault((u)=>u.Id == UserID));
            _context.SaveChanges();
            return RedirectToAction("Administrator", "Administrator");
        }

        // [HttpPost]
        // public IActionResult DeleteUser(ApplicationUser User)
        // {
        //     _context.Users.Remove(User);
        //     _context.SaveChanges();
        //     return Redirect("/");
        // }
    }
}
