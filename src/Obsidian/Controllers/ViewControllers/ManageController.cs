using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {
        [Route("[controller]/{*path}")]
        public IActionResult Index(string path = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                //TODO: redirect to oauth
            }
            //TODO: check user permission
            ViewData["FrontendRoute"] = path;
            return View();
        }
    }
}