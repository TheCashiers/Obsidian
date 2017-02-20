using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.Authentication;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.Services;
using Obsidian.Misc;
using Obsidian.Models.Account;


#pragma warning disable CS1591
namespace Obsidian.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SagaBus _sagaBus;

        private readonly ISignInService _signinService;

        public AccountController(SagaBus bus, ISignInService signinService)
        {
            _sagaBus = bus;
            _signinService = signinService;
        }

        [HttpGet]
        public IActionResult SignIn([FromQuery][Url]string returnUrl = "/")
        {
            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> SignIn([FromForm]SignInViewModel model)
        {
            var command = new PasswordAuthenticateCommand
            {
                UserName = model.UserName,
                Password = model.Password
            };
            var authResult = await _sagaBus.InvokeAsync<PasswordAuthenticateCommand, AuthenticationResult>(command);
            if (!authResult.IsCredentialVaild)
            {
                ModelState.AddModelError(nameof(SignInViewModel.UserName), "Invaild user name");
                ModelState.AddModelError(nameof(SignInViewModel.Password), "Or invaild password");
                return View("SignIn");
            }
            await _signinService.CookieSignInAsync(AuthenticationSchemes.PortalCookie, authResult.User, model.RememberMe);
            return Redirect(model.ReturnUrl);
        }
    }
}