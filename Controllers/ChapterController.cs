using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using coursework_itransition.Models;
using coursework_itransition.Data;
using static coursework_itransition.AccessControl;
using System.Linq;

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

        private Chapter GetChapter(string id, out IActionResult res)
        {
            var chapter = this._context.Chapters.Include(c => c.Composition).FirstOrDefault(c => c.ID == id);

            if ((System.Object)chapter == null)
            {
                res = RedirectToAction("Index", "Deadends", new { message = "Chapter was not found" });
                return null;
            }

            if (!UserHasAccess(this.User, chapter))
            {
                res = RedirectToAction("Index", "Deadends", new { message = "You have no edit rights over this piece of art ..." });
                return null;
            }

            res = null;
            return chapter;
        }

        private RedirectResult RedirectBack(string url)
        {
            // net vremeni pisat proverky na realnost ssilki
            if (url == null || url == "")
                return Redirect("~/");

            return Redirect(System.Web.HttpUtility.UrlDecode(url));
        }

        [HttpGet, Route("Chapter/New/{compID}")]
        public IActionResult NewGet(string compID) => View("New");


        [HttpGet, Route("Chapter/Edit/{id}")]
        public IActionResult EditGet(string id)
        {
            IActionResult res = null;
            var chapter = GetChapter(id, out res);
            
            if(res != null)
                return res;

            return View("Edit", new PostModel{Title = chapter.Title, Contents = chapter.Contents});
        }


        [HttpPost, Route("Chapter/Edit/{id}")]
        public IActionResult EditPost(string id, [Bind("Title,Contents,ReturnUrl")] PostModel data)
        {
            IActionResult res = null;
            var chapter = GetChapter(id, out res);

            if (res != null)
                return res;

            chapter.Title       = data.Title;
            chapter.Contents    = data.Contents;
            chapter.LastEditDT  = chapter.Composition.LastEditDT
                                = System.DateTime.UtcNow;

            this._context.Chapters.Update(chapter);
            this._context.SaveChanges();

            return RedirectBack(data.ReturnUrl);
        }

        [HttpPost, Route("Chapter/Delete/{id}")]
        public IActionResult DeleteChapter(string id, [Bind("ReturnUrl")]PostModel data)
        {
            IActionResult res = null;
            var chapter = GetChapter(id, out res);

            if (res != null)
                return res;

            this._context.Chapters.Remove(chapter);
            this._context.SaveChanges();

            return RedirectBack(data.ReturnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
