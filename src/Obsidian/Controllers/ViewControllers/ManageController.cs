using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Controllers.OAuth;
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
                if (Request.Cookies.TryGetValue(CookieKey, out var protectedToken))
                {
                    var token = _dataProtector.Unprotect(protectedToken);
                    return RedirectToAction(nameof(Index), new { access_token = token, path = path });
                }
                else
                {
                    //Redirect to oauth login.
                    var portalUrl = GetPortalUrl(path);
                    var authUrl = _portalService.AuthorizeUriForAdminPortal(portalUrl);
                    return Redirect(authUrl);
                }
            }
            {
                var protectedToken = _dataProtector.Protect(accessToken);
                Response.Cookies.Append(CookieKey, protectedToken);
            }
            ViewData["FrontendRoute"] = path;
            return View();
        }

        private string GetPortalUrl(string path) => Url.Action(nameof(Index), "Manage", new { path = path }, Request.Scheme, Request.Host.Value);


        [Route("[controller]/signout")]
        public IActionResult SignOut([FromQuery(Name = "signout_oauth")]bool signOutOauth = true)
        {
            Response.Cookies.Delete(CookieKey);
            if (signOutOauth)
            {
                var portalUrl = GetPortalUrl(null);
                return RedirectToAction(nameof(OAuth20Controller.SignOut), "OAuth20", new { redurect_uri = portalUrl });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}