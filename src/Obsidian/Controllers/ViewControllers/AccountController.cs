using Microsoft.AspNetCore.Mvc;


#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}