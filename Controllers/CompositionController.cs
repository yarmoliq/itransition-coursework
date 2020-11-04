using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.AspNetCore.Identity;
using Identity.Models;
using System.Security.Claims;


namespace coursework_itransition.Controllers
{
    [Authorize]
    public class CompositionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompositionController> _logger;

        public CompositionController(ApplicationDbContext context,
            ILogger<CompositionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private string CurrentUserID()
        {
            var c = this.User.FindFirst(ClaimTypes.NameIdentifier);
            
            if(c == null)
                return System.String.Empty;
                
            return c.Value;
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void New([Bind("Title,Summary,Genre")] Composition comp)
        {
            var newComp = new Composition();
            newComp.CreationDT = System.DateTime.UtcNow;
            newComp.LastEditDT = System.DateTime.UtcNow;
            newComp.AuthorID = CurrentUserID();
            newComp.Title = comp.Title;
            newComp.Summary = comp.Summary;
            newComp.Genre = comp.Genre;

            _context.Add<Composition>(newComp);
            _context.SaveChanges();
        }

        public IActionResult Edit(string id)
        {
            var composition = _context.Compositions.Find(id);
            
            if(composition.AuthorID != CurrentUserID())
            {
                return View();
            }

            return View(composition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit(string id, [Bind("Title,Summary,Genre")] Composition editedComp)
        {
            var comp = this._context.Compositions.Find(id);
            if((System.Object)comp != null)
            {
                if(comp.AuthorID == CurrentUserID())
                {
                    comp.LastEditDT = System.DateTime.UtcNow;
                    comp.Title      = editedComp.Title;
                    comp.Genre      = editedComp.Genre;
                    comp.Summary    = editedComp.Summary;

                    this._context.Compositions.Update(comp);
                    this._context.SaveChanges();
                }
                else
                {
                    // idk wtf do i do here
                }
            }
            else
            {
                // and here too...
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
