using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;

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

        public IActionResult New() => View();
        public IActionResult NoEditRights() => View();
        public IActionResult CompNotFound() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([Bind("Title,Summary,Genre")] Composition comp)
        {
            var newComp = new Composition();
            newComp.CreationDT = System.DateTime.UtcNow;
            newComp.LastEditDT = System.DateTime.UtcNow;
            newComp.AuthorID = coursework_itransition.Utils.GetUserID(this.User);
            newComp.Title = comp.Title;
            newComp.Summary = comp.Summary;
            newComp.Genre = comp.Genre;

            _context.Add<Composition>(newComp);
            _context.SaveChanges();

            return RedirectToRoute("default", new { controller = "Composition", action = "Edit", id = newComp.ID });
        }

        public IActionResult Edit(string id, string returnUrl)
        {
            var composition = _context.Compositions.Find(id);

            if ((System.Object)composition != null)
            {
                if (!coursework_itransition.Utils.UserIsAuthor(this.User, composition))
                    return RedirectToAction("NoEditRights");

                return View(composition);
            }

            return RedirectToAction("CompNotFound");
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(string id, string returnUrl, [Bind("Title,Summary,Genre")] Composition editedComp)
        {
            var comp = this._context.Compositions.Find(id);
            if((System.Object)comp != null)
            {
                if(coursework_itransition.Utils.UserIsAuthor(this.User, comp))
                {
                    comp.LastEditDT = System.DateTime.UtcNow;
                    comp.Title      = editedComp.Title;
                    comp.Genre      = editedComp.Genre;
                    comp.Summary    = editedComp.Summary;

                    this._context.Compositions.Update(comp);
                    this._context.SaveChanges();

                    if(returnUrl == null)
                        return Redirect("~/");
                    return Redirect(System.Web.HttpUtility.UrlDecode(returnUrl));
                }
                else
                {
                    return RedirectToAction("NoEditRights");
                }
            }

            return RedirectToAction("CompNotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComp(string id, string returnUrl)
        {
            if(id == null)
                return RedirectToAction("CompNotFound");

            var comp = this._context.Compositions.Find(id);
            if((System.Object)comp == null)
                return RedirectToAction("CompNotFound");

            if(!coursework_itransition.Utils.UserIsAuthor(this.User, comp))
                return RedirectToAction("NoEditRights");

            // check if deleted
            this._context.Compositions.Remove(comp);
            this._context.SaveChanges();

            if (returnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(returnUrl));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
