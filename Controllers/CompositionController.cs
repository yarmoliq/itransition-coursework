using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Models;

using coursework_itransition.Models;
using coursework_itransition.Data;
using static coursework_itransition.AccessControl;

namespace coursework_itransition.Controllers
{
    [Authorize]
    public class CompositionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompositionController> _logger;
        public readonly UserManager<ApplicationUser> _userManager;


        public CompositionController(ApplicationDbContext context,
            ILogger<CompositionController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }


        private bool UserExist(string id)
        {
            if(id == null)
                return false;

            if(_context.Users.Find(id) == null)
                return false;

            return true;
        }
        private Composition GetComposition(string id, out IActionResult res)
        {
            var comp = this._context.Compositions
                                   .Include(c => c.Chapters)
                                   .Include(c => c.Author)
                                   .FirstOrDefault(c => c.ID == id);

            if ((System.Object)comp == null)
            {
                res = RedirectToAction("Index", "Deadends", new { message = "Composition was not found" });
                return null;
            }

            if (!UserHasAccess(this.User, comp))
            {
                res = RedirectToAction("Index", "Deadends", new { message = "You have no edit rights over this piece of art ..." });
                return null;
            }

            res = null;
            return comp;
        }


        private bool UpdateComposition(Composition comp, Composition updated)
        {
            comp.Title    = updated.Title;
            comp.Genre    = updated.Genre;
            comp.Summary  = updated.Summary;

            foreach (var chapter in comp.Chapters)
            {
                var updatedChapter = updated.Chapters.FirstOrDefault(c => c.ID == chapter.ID);
                if((System.Object)updatedChapter == null)
                    return false;

                UpdateChapter(chapter, updatedChapter);
            }

            comp.LastEditDT = System.DateTime.Now;
            this._context.SaveChanges();

            return true;
        }

        private void UpdateChapter(Chapter chapter, Chapter updated)
        {
            chapter.Title       = updated.Title;
            chapter.Contents    = updated.Contents;
            chapter.Order       = updated.Order;
            // chapter.LastEditDT  = System.DateTime.UtcNow;
        }


        [HttpGet, Route("Composition/New/{UserID?}")]
        public IActionResult New(string UserID = null) => View();


        [HttpGet, Route("Composition/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            IActionResult res = null;
            GetComposition(id, out res);

            if (res != null)
                return res;

            return View();
        }


        [HttpPost, Route("Composition/New/{UserID?}")]
        public IActionResult New([Bind("Title,Summary,Genre")] Composition comp, string UserID = null)
        {
            string AuthorID;

            if(UserID == null)
                AuthorID = Utils.GetUserID(this.User);
            else if(UserExist(UserID))
                AuthorID = UserID;
            else
                return RedirectToAction("Index", "Deadends", new { message = "User with requested ID doesnt exist" });

            var newComp = new Composition()
            {
                CreationDT  = System.DateTime.Now,
                LastEditDT  = System.DateTime.Now,
                AuthorID    = AuthorID,
                Title       = comp.Title,
                Summary     = comp.Summary,
                Genre       = comp.Genre
            };

            _context.Add<Composition>(newComp);
            _context.SaveChanges();

            return RedirectToRoute("composition", new { controller = "Composition", action = "Edit", id = newComp.ID });
        }


        [AllowAnonymous]
        [HttpGet, Route("Composition/Show/{id}")]
        public IActionResult Show(string id)
        {
            IActionResult res = null;
            var comp = GetComposition(id, out res);

            if (res != null)
                return res;

            return View(comp);
        }


        [HttpPost, Route("Composition/Get")]
        public Composition Get([FromBody] string id)
        {
            IActionResult res = null;
            return GetComposition(id, out res);
        }


        [HttpPost, Route("Composition/Update")]
        public string Update([FromBody] Composition updated)
        {
            if((System.Object)updated == null)
                return "Null received";

            IActionResult res = null;
            var comp = GetComposition(updated.ID, out res);

            if(res != null)
                return res.ToString();

            UpdateComposition(comp, updated);

            return "Success";
        }


        [HttpPost, Route("Composition/Delete")]
        public string Delete([FromBody]string id)
        {
            if (id == null)
                return "Null received";

            IActionResult res;
            var comp = GetComposition(id, out res);

            if(res != null)
                return res.ToString();

            this._context.Compositions.Remove(comp);
            this._context.SaveChanges();

            return "Success";
        }


        [HttpPost, Route("Chapter/New/{compID}")]
        public IActionResult CreateNewChapter(string compID, [Bind("Title,Contents,ReturnUrl")] ChapterController.PostModel data)
        {
            IActionResult res = null;
            var comp = GetComposition(compID, out res);

            if (res != null)
                return res;

            var newChapter = new Chapter()
            {
                CreationDT = System.DateTime.UtcNow,
                LastEditDT = System.DateTime.UtcNow,
                CompositionID = comp.ID,
                Title = data.Title,
                Contents = data.Contents,
                Order = comp.Chapters.Count,
                Composition = comp
            };

            comp.LastEditDT = System.DateTime.UtcNow;

            this._context.Chapters.Add(newChapter);
            this._context.SaveChanges();

            if (data.ReturnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(data.ReturnUrl));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
