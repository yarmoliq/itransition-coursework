using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;
using System.Collections.Generic;
using System.Linq;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coursework_itransition.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly ILogger<HomeController> _logger;
        public readonly RoleManager<IdentityRole> _roleManager;

        [HttpPost, Route("Administrator/Get")]
        public async Task<List<Composition>> Get([FromBody] int start)
        {
            var elements = this._context.Compositions.Include(c => c.Chapters).OrderByDescending(w=>w.LastEditDT);
            return await elements.Skip(start - 1).Take(10).ToListAsync();
        }
        
        [HttpPost, Route("Administrator/GetAuthorName")]
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
