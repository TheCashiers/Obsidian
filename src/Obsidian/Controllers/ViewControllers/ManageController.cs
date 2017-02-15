using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {
        [Route("[controller]/{*path}")]
        public IActionResult Index(string path = "")
        {
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}