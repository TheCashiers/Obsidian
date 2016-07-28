using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Obsidian.Controllers.ViewControllers
{
    public class ManageController : Controller
    {
        [Route("[controller]/{path?}")]
        public IActionResult Index(string path = "")
        {
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}