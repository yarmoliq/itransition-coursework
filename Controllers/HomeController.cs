using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using coursework_itransition.Models;
using coursework_itransition.Data;

namespace coursework_itransition.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly ILogger<HomeController> _logger;
        public readonly RoleManager<IdentityRole> _roleManager;

        [HttpPost, Route("Home/Get")]
        public async Task<List<Composition>> Get([FromBody] int start)
        {
            var elements = this._context.Compositions.Include(c => c.Chapters).OrderByDescending(w=>w.LastEditDT);
            return await elements.Skip(start).Take(10).ToListAsync();
        }
        
        [HttpPost, Route("Home/GetAuthorName")]
        public string GetAuthorName([FromBody] string UserID)
        {
            return _context.Users.FirstOrDefault(w=>w.Id == UserID).Name;
        }

        public HomeController(ApplicationDbContext context,
            ILogger<HomeController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
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
