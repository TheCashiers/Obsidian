using Microsoft.AspNetCore.Mvc;
using Obsidian.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {

        private readonly PortalService _portalService;

        public ManageController(PortalService portalService)
        {
            _portalService = portalService;
        }

        [Route("[controller]/{*path}")]
        public IActionResult Index([FromQuery(Name = "access_token")]string accessToken = "", string path = "")
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                //Redirect to oauth login.
                return Ok("Execution\nExecution\nExecution\nExecution\nExecution\nPlease Provide Access Token\nExecution\nExecution\nExecution\nExecution\nExecution\nExecution\n");
            }
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}