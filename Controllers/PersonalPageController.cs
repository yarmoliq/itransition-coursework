using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReflectionIT.Mvc.Paging;

using coursework_itransition.Models;
using coursework_itransition.Data;
using Identity.Models;

namespace coursework_itransition.Controllers
{
    [Authorize]
    public class PersonalPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser user { get; set; }

        public ReflectionIT.Mvc.Paging.PagingList<Composition> PartOfCompsition;

        [Route("PersonalPage/PersonalPage/{UserID}/{pageindex?}")]
        public async Task<IActionResult> PersonalPage(string UserID, string sortOrder, string currentFilter,  int pageindex = 1)
        {            
            user = await _context.Users.Include(comp=>comp.Compositions).FirstOrDefaultAsync((user)=>user.Id == UserID);
            if(user != null)
            {
                ViewData["currentFilter"] = currentFilter;
                var sorted = from s in user.Compositions
                            select s;
                if (!String.IsNullOrEmpty(currentFilter))
                {
                    sorted = sorted.Where(comp=>comp.Title.Contains(currentFilter));
                }
                switch (sortOrder)
                {
                    case "title":
                        sorted = sorted.OrderBy(s => s.Title);
                        break;
                    default:
                        sorted = sorted.OrderBy(s => s.LastEditDT);
                        break;
                }
                PartOfCompsition = PagingList.Create(sorted, 10, pageindex);
                PartOfCompsition.Action = "PersonalPage";
                return View(this);
            }
            else
            {
                return RedirectToRoute("deadends", new{message = "User is not found"});
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
