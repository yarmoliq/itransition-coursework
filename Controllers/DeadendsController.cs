using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using coursework_itransition.Models;

namespace coursework_itransition.Controllers
{
    public class DeadendsController : Controller
    {
        public DeadendsController() { }
        public IActionResult Index(string message) => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
