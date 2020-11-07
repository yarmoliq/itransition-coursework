using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using coursework_itransition.Models;

using Microsoft.AspNetCore.Authorization;

using coursework_itransition.Data;

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

        [HttpPost, ActionName("New")]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewChapter(string compID, string returnUrl, [Bind("Title,Contents")] Chapter data)
        {
            _logger.LogWarning(compID);
            _logger.LogWarning(returnUrl);

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
    }   
}
