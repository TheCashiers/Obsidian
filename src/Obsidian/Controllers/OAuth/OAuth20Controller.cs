using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Obsidian.Application.OAuth20;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OAuth20SignInResult = Obsidian.Application.OAuth20.SignInResult;

//TODO: remove this when implemented
#pragma warning disable CS1998


namespace Obsidian.Controllers.OAuth
{
    public class OAuth20Controller : Controller
    {
        private readonly IDataProtector _dataProtector;
        private readonly SagaBus _sagaBus;

        public OAuth20Controller(IDataProtectionProvider dataProtectionProvicer, SagaBus bus)
        {
            _dataProtector = dataProtectionProvicer.CreateProtector("Obsidian.OAuth.Context.Key");
            _sagaBus = bus;
        }

        [Route("oauth20/authorize/frontend/debug")]
        [HttpGet]
        public IActionResult FrontEndDebug()
        {
            return View("SignIn");
        }

        [Route("oauth20/authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {
            var command = new AuthorizeCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
            };

            if (User.Identity.IsAuthenticated)
            {
                command.UserName = User.Identity.Name;
            }

            var result = await _sagaBus.InvokeAsync<AuthorizeCommand, AuthorizeResult>(command);
            var protectedContext = _dataProtector.Protect(result.SagaId.ToString());
            switch (result.Status)
            {
                case OAuth20Status.Fail:
                    return BadRequest(result.ErrorMessage);

                case OAuth20Status.RequireSignIn:
                    return View("SignIn", new OAuthSignInModel { ProtectedOAuthContext = protectedContext });

                case OAuth20Status.CanRequestToken:
                case OAuth20Status.ImplicitTokenReturned:
                    return Redirect(result.RedirectUri);

                case OAuth20Status.RequirePermissionGrant:
                    ViewBag.Client = result.Client;
                    ViewBag.Scopes = result.Scopes;
                    return View("PermissionGrant", new PermissionGrantModel { ProtectedOAuthContext = protectedContext });

                default:
                    return BadRequest();
            }
        }

        [Route("oauth20/authorize")]
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]OAuthSignInModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var message = new SignInMessage(sagaId)
            {
                UserName = model.UserName,
                Password = model.Password
            };
            var result = await _sagaBus.SendAsync<SignInMessage, OAuth20SignInResult>(message);
            throw new NotImplementedException();
        }

        [Route("oauth20/authorize")]
        [HttpPost]
        public async Task<IActionResult> PermissionGrant([FromForm]PermissionGrantModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var message = new PermissionGrantMessage(sagaId) { PermissionGranted = model.Grant };
            var result = await _sagaBus.SendAsync<PermissionGrantMessage, PermissionGrantResult>(message);
            if (model.Grant)
            {
                return Redirect(result.RedirectUri);
            }
            return BadRequest();
        }

        [Route("oauth20/token")]
        [HttpPost]
        public async Task<IActionResult> Token(AccessTokenFromAuthorizationCodeRequestModel model)
        {
            return StatusCode(501);
        }
    }
}
