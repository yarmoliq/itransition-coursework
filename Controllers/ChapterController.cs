using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;
using Microsoft.EntityFrameworkCore;

namespace coursework_itransition.Controllers
{
    [Authorize]
    public class ChapterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChapterController> _logger;

        public ChapterController(ApplicationDbContext context,
            ILogger<ChapterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult New(string compID, string returnUrl = null) => View();

        [Route("Chapter/Edit/{id}/{returnUrl?}")]
        public IActionResult Edit(string id, string returnUrl = null) => View(this._context.Chapters.Find(id));

        [HttpPost, Route("Chapter/New/{compID}/{returnUrl?}")]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewChapter(string compID, string returnUrl, [Bind("Title,Contents")] Chapter data)
        {
            if(compID == null)
                return Content("no id");

            var comp = this._context.Compositions.Find(compID);
            if((System.Object)comp == null)
                return Content("no composition with such id found");

            var newChapter = new Chapter();
            newChapter.CreationDT = System.DateTime.UtcNow;
            newChapter.LastEditDT = System.DateTime.UtcNow;
            newChapter.CompositionID = compID;
            newChapter.Title = data.Title;
            newChapter.Contents = data.Contents;
            newChapter.Composition = comp;

            this._context.Chapters.Add(newChapter);
            this._context.SaveChanges();

            if (returnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(returnUrl));
        }

        [HttpPost, Route("Chapter/Edit/{id}/{returnUrl?}")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> EditChapter(string id, string returnUrl, [Bind("Title,Contents")] Chapter data)
        {
            var chapter = await this._context.Chapters.Include(c => c.Composition).FirstOrDefaultAsync(c => c.ID == id);
            if((System.Object)chapter == null)
                return Content("chapter not found");

            if(!coursework_itransition.Utils.UserIsAuthor(this.User, chapter.Composition.AuthorID))
                return Content("No rights");

            chapter.Title       = data.Title;
            chapter.Contents    = data.Contents;
            chapter.LastEditDT  = chapter.Composition.LastEditDT
                                = System.DateTime.UtcNow;

            this._context.Chapters.Update(chapter);
            this._context.SaveChanges();

            if (returnUrl == null)
                return Redirect("~/");
            return Redirect(System.Web.HttpUtility.UrlDecode(returnUrl));
        }
    }   
}
