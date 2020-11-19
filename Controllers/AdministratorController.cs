using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Models;
using ReflectionIT.Mvc.Paging;

using coursework_itransition.Data;
using coursework_itransition.Models;

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

        [Route("Administrator/Administrator/{pageindex?}")]
        public async Task<IActionResult> Administrator(int pageindex = 1)
        {
            var sortedUsers = _context.Users.Include(comp=>comp.Compositions).AsNoTracking().OrderBy(s=>s.Email);
            var partofUsers = await PagingList.CreateAsync(sortedUsers, 10, pageindex);
            partofUsers.Action = "Administrator";
            return View("Administrator", partofUsers);
        }


        [HttpPost, Route("Administrator/ActionWithUser/{UserID?}")]
        [ValidateAntiForgeryToken]
        public IActionResult ActionWithUser(string UserID, string stringAction, int pageindex)
        {
            if(UserID != coursework_itransition.Utils.GetUserID(this.User))
            {
                var method = (typeof(AdministratorController)).GetMethod(stringAction);
                string[] objects = new string[1]
                {
                    UserID
                };
                
                ((Task)method.Invoke(this, objects)).Wait();
            }

            return RedirectToRoute(new { controller = "Administrator", action = "Administrator", pageindex = pageindex.ToString()});
        }

        public async Task DeleteUser(string UserID)
        {
            await _userManager.DeleteAsync(_context.Users.FirstOrDefault((u)=>u.Id == UserID));
        }

        public async Task BanUser(string UserID)
        {
            var user = _context.Users.FirstOrDefault(w=>w.Id == UserID);
            user.LockoutEnd = System.DateTime.Now.AddHours(365 * 24 * 150);
            await _context.SaveChangesAsync();
        }

        public async Task UnBanUser(string UserID)
        {
            var user = _context.Users.FirstOrDefault(w=>w.Id == UserID);
            user.LockoutEnd = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task MakeAdmin(string UserID)
        {
            var user = _context.Users.FirstOrDefault(w=>w.Id == UserID);
            var listRoleUser = await _userManager.GetRolesAsync(user);
            if(listRoleUser == null || listRoleUser.IndexOf("Administrator") == -1)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        public async Task RemoveAdminStatus(string UserID)
        {
            var user = _context.Users.FirstOrDefault(w=>w.Id == UserID);
            var listRoleUser = await _userManager.GetRolesAsync(user);
            if(listRoleUser != null && listRoleUser.IndexOf("Administrator") != -1)
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrator");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
