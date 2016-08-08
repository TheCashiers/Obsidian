using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Obsidian.Controllers.ViewControllers
{
    public class ManageController : Controller
    {
        [Route("[controller]/{*path}")]
        public IActionResult Index([FromQuery]Guid code, string path = "")
        {
            if (!User.Identity.IsAuthenticated || code == Guid.Empty)
            {
                //TODO: redirect to oauth
            }
            //TODO: generate access token from code
            //TODO: check user permission
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}