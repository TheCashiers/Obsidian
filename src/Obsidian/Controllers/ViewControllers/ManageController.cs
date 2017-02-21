using Microsoft.AspNetCore.Mvc;
using Obsidian.Services;

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
                var portalUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}";
                var authUrl = _portalService.AuthorizeUriForAdminPortal(portalUrl);
                return Redirect(authUrl);
            }
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}