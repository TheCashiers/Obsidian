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
            //TODO: check if current user is admin
            ViewData["Code"] = code;
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}