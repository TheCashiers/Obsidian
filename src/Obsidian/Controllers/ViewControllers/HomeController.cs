using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Misc;
using Obsidian.Models;
using Obsidian.Services;
using System;
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

        private async Task CreateMarkerFileAsync()
        {
            var markerFilePath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "init.txt");
            await System.IO.File.WriteAllTextAsync(markerFilePath, DateTime.UtcNow.ToString());
        }

        [HttpGet("firstrun")]
        public IActionResult Initialize()
        {
            if (IsMarkerFileExists())
            {
                // Confuse the attacker by returning 404.
                return NotFound();
            }
            return View();
        }

        [HttpPost("firstrun")]
        [ValidateModel]
        public async Task<IActionResult> Initialize(InitializationInfo initializationInfo)
        {
            if (IsMarkerFileExists())
            {
                // Confuse the attacker by returning 404.
                return NotFound();
            }
            await _initializationService.InitializeAsync(initializationInfo);
            await CreateMarkerFileAsync();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Delay(TimeSpan.FromSeconds(3)).ContinueWith(_ => Program.RestartHostAsync());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            ViewData["PortalUrl"] = Url.Action("Index", "Manage", null, Request.Scheme, Request.Host.Value);
            return View("InitSuccess");
        }
    }
}
