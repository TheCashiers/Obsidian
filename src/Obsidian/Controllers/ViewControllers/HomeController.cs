using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Misc;
using Obsidian.Models;
using Obsidian.Services;
using System.Threading.Tasks;

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

        private bool IsMarkerFileExists()
        {
            var markerFilePath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "init.txt");
            return System.IO.File.Exists(markerFilePath);
        }

        [HttpGet("firstrun")]
        public IActionResult Initialize()
        {
            if(IsMarkerFileExists())
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost("firstrun")]
        [ValidateModel]
        public async Task<IActionResult> InitializeAsync(InitializationInfo initializationInfo)
        {
            if (IsMarkerFileExists())
            {
                return NotFound();
            }
            await  _initializationService.InitializeAsync(initializationInfo);
            return Redirect("/manage");
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
