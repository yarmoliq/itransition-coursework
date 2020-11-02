using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Http; // httpcontext
using System.Security.Claims;


namespace coursework_itransition.Controllers
{
    [Authorize]
    public class CompositionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompositionController> _logger;
        private IHttpContextAccessor _h;

        public CompositionController(ApplicationDbContext context,
            ILogger<CompositionController> logger,
            IHttpContextAccessor h)
        {
            _context = context;
            _logger = logger;
            _h = h;
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void New([Bind("Title,Summary,Genre")] Composition comp)
        {
            var currentUserId = _h.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newComp = new Composition();
            newComp.AuthorID = currentUserId;
            newComp.Title = comp.Title;
            newComp.Summary = comp.Summary;
            newComp.Genre = comp.Genre;

            _context.Add<Composition>(newComp);
            _context.SaveChanges();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
