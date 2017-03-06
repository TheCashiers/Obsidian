using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Services;

#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {

        private readonly PortalService _portalService;

        private readonly IDataProtector _dataProtector;

        const string CookieKey = "Obsidian.Portal";

        public ManageController(PortalService portalService, IDataProtectionProvider dataProtectionProvicer)
        {
            _portalService = portalService;
            _dataProtector = dataProtectionProvicer.CreateProtector("Obsidian.Portal.Token.Key");
        }

        [Route("[controller]/{*path}")]
        public IActionResult Index([FromQuery(Name = "access_token")]string accessToken = "", string path = "")
        {

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                //try load token from cookie
                string protectedToken;
                if (Request.Cookies.TryGetValue(CookieKey, out protectedToken))
                {
                    var token = _dataProtector.Unprotect(protectedToken);
                    return RedirectToAction(nameof(Index), new { access_token = token, path = path });
                }
                else
                {
                    //Redirect to oauth login.
                    var portalUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}";
                    var authUrl = _portalService.AuthorizeUriForAdminPortal(portalUrl);
                    return Redirect(authUrl);
                }
            }

            Response.Cookies.Append(CookieKey, _dataProtector.Protect(accessToken));
            ViewData["FrontendRoute"] = path;
            return View();
        }

        public IActionResult SignOut()
        {
            Response.Cookies.Delete(CookieKey);
            return RedirectToAction(nameof(Index));
        }
    }
}