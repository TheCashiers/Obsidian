using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {
        [Route("[controller]/{*path}")]
        public IActionResult Index([FromQuery(Name = "access_token")]string accessToken = "", string path = "")
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                //Redirect to oauth login.
            }
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}