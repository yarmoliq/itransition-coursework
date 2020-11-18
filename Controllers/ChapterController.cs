using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using coursework_itransition.Models;
using coursework_itransition.Data;

namespace coursework_itransition.Controllers
{
    [Authorize]
    public class ChapterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChapterController> _logger;

        public class PostModel
        {
            public string Title { get; set; }
            public string Contents { get; set; }
            public string ReturnUrl { get; set; }
        }

        public ChapterController(ApplicationDbContext context,
            ILogger<ChapterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult New(string compID) => View();

        [Route("Chapter/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var chapter = this._context.Chapters.Find(id);

            if((System.Object)chapter == null)
                return RedirectToAction("Index", "Deadends", new { message = "Chapter was not found" });

            return View(new PostModel{Title = chapter.Title, Contents = chapter.Contents});
        }

        [HttpPost, Route("Chapter/New/{compID}")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> AddNewChapter(string compID, [Bind("Title,Contents,ReturnUrl")] PostModel data)
        {
            var comp = await this._context.Compositions
                                                .Include(c => c.Chapters)
                                                .FirstOrDefaultAsync(c => c.ID == compID);
            if((System.Object)comp == null)
                return RedirectToAction("Index", "Deadends", new { message = "Composition for the chapter was not found." });

            var newChapter = new Chapter();
            newChapter.CreationDT       = System.DateTime.UtcNow;
            newChapter.LastEditDT       = System.DateTime.UtcNow;
            newChapter.CompositionID    = compID;
            newChapter.Title            = data.Title;
            newChapter.Contents         = data.Contents;
            newChapter.Order            = comp.Chapters.Count;
            newChapter.Composition      = comp;

            comp.LastEditDT             = System.DateTime.UtcNow;

            this._context.Chapters.Add(newChapter);
            this._context.SaveChanges();

            if (data.ReturnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(data.ReturnUrl));
        }

        [HttpPost, Route("Chapter/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> EditChapter(string id, [Bind("Title,Contents,ReturnUrl")] PostModel data)
        {
            var chapter = await this._context.Chapters.Include(c => c.Composition).FirstOrDefaultAsync(c => c.ID == id);
            if((System.Object)chapter == null)
                return RedirectToAction("Index", "Deadends", new { message = "Chapter was not found" });

            if(!coursework_itransition.Utils.UserIsAuthor(this.User, chapter.Composition.AuthorID))
                return RedirectToAction("Index", "Deadends", new { message = "You have no edit rights over this piece of art ..." });

            chapter.Title       = data.Title;
            chapter.Contents    = data.Contents;
            chapter.LastEditDT  = chapter.Composition.LastEditDT
                                = System.DateTime.UtcNow;

            this._context.Chapters.Update(chapter);
            this._context.SaveChanges();

            if (data.ReturnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(data.ReturnUrl));
        }

        [HttpPost, Route("Chapter/Delete/{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteChapter(string id, [Bind("ReturnUrl")]PostModel data)
        {
            var chapter = await this._context.Chapters.Include(c => c.Composition).FirstOrDefaultAsync(c => c.ID == id);
            if ((System.Object)chapter == null)
                return RedirectToAction("Index", "Deadends", new { message = "Chapter was not found" });

            if (!coursework_itransition.Utils.UserIsAuthor(this.User, chapter.Composition.AuthorID))
                return RedirectToAction("Index", "Deadends", new { message = "You have no edit rights over this piece of art ..." });

            this._context.Chapters.Remove(chapter);
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
