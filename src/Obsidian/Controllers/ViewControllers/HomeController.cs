using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Services;

#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly InitializationService _initializationService;

        public HomeController(IHostingEnvironment hostingEnvironment,
                              InitializationService initializationService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._initializationService = initializationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("firstrun")]
        public IActionResult Initialize()
        {
            var markFile = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "init.txt");
            if (System.IO.File.Exists(markFile))
            {
                return NotFound();
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
