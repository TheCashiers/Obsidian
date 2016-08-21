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
using System.Text;
using System.Threading.Tasks;

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

        [Route("oauth20/authorize/frontend/singin")]
        [HttpGet]
        public IActionResult FrontEndSignInDebug()
        {
            return View("SignIn");
        }


        [Route("oauth20/authorize/frontend/grant")]
        [HttpGet]
        public IActionResult FrontEndGrantDebug()
        {
            var context = _dataProtector.Protect(Guid.NewGuid().ToString());
            var client = Client.Create(Guid.NewGuid(), "XXXX", "http://za-pt.org/exchange");
            client.DisplayName = "iTech";
            ViewBag.Client = client;
            ViewBag.Scopes = new[] {
                PermissionScope.Create(Guid.NewGuid(),"obsidian.basicinfo","Basic Information","Includes you name and gender."),
                PermissionScope.Create(Guid.NewGuid(),"obsidian.email","Email address","Your email address."),
                PermissionScope.Create(Guid.NewGuid(),"obsidian.admin","Admin access","Manage the system."),
            };
            return View("PermissionGrant", new PermissionGrantModel { Grant = true, ProtectedOAuthContext = context });
        }

        [Route("oauth20/authorize/frontend/denied")]
        [HttpGet]
        public IActionResult FrontEndDeniedDebug()
        {
            return View("UserDenied");
        }

        [Route("oauth20/authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {
            AuthorizationGrant grantType;
            if ("code".Equals(model.ResponseType, StringComparison.OrdinalIgnoreCase))
            {
                grantType = AuthorizationGrant.AuthorizationCode;
            }
            else if ("token".Equals(model.ResponseType, StringComparison.OrdinalIgnoreCase))
            {
                grantType = AuthorizationGrant.AuthorizationCode;
            }
            else
                return BadRequest();
            var command = new AuthorizeCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
                GrantType = grantType
            };

            if (User.Identity.IsAuthenticated)
            {
                command.UserName = User.Identity.Name;
            }

            var result = await _sagaBus.InvokeAsync<AuthorizeCommand, OAuth20Result>(command);
            var context = _dataProtector.Protect(result.SagaId.ToString());
            switch (result.State)
            {
                case OAuth20State.RequireSignIn:
                    return View("SignIn", new OAuthSignInModel { ProtectedOAuthContext = context });
                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(result);
                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(result);
                case OAuth20State.Finished:
                    return ImplictRedirect(result);
                default:
                    return BadRequest();
            }
        }

        private IActionResult PermissionGrantView(OAuth20Result result)
        {
            var context = _dataProtector.Protect(result.SagaId.ToString());
            ViewBag.Client = result.PermissionGrant.Client;
            ViewBag.Scopes = result.PermissionGrant.Scopes;
            return View("PermissionGrant", new PermissionGrantModel { Grant = true, ProtectedOAuthContext = context });
        }

        private IActionResult ImplictRedirect(OAuth20Result result)
        {
            string tokenRedirectUri = BuildImplictReturnUri(result);
            return Redirect(tokenRedirectUri);
        }

        private IActionResult AuthorizationCodeRedirect(OAuth20Result result)
        {
            var codeRedirectUri = $"{result.RedirectUri}?code={result.AuthorizationCode}";
            return Redirect(codeRedirectUri);
        }

        private static string BuildImplictReturnUri(OAuth20Result result)
        {
            var sb = new StringBuilder($"{result.RedirectUri}?access_token={result.Token.AccessToken}");
            if (result.Token.AuthrneticationToken != null)
            {
                sb.Append($"&authentication_token={result.Token.AuthrneticationToken}");
            }
            if (result.Token.RefreshToken != null)
            {
                sb.Append($"&refresh_token={result.Token.RefreshToken}");
            }
            var tokenRedirectUri = sb.ToString();
            return tokenRedirectUri;
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
                Password = model.Password,
                RememberMe = model.RememberMe
            };
            var result = await _sagaBus.SendAsync<SignInMessage, OAuth20Result>(message);
            switch (result.State)
            {
                case OAuth20State.RequireSignIn:
                    ModelState.AddModelError(string.Empty, "Singin failed");
                    return View("SignIn");
                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(result);
                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(result);
                case OAuth20State.Finished:
                    return ImplictRedirect(result);
                default:
                    return BadRequest();
            }
        }

        [Route("oauth20/authorize/permission")]
        [HttpPost]
        public async Task<IActionResult> PermissionGrant([FromForm]PermissionGrantModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var message = new PermissionGrantMessage(sagaId) { PermissionGranted = model.Grant };
            var result = await _sagaBus.SendAsync<PermissionGrantMessage, OAuth20Result>(message);
            switch (result.State)
            {
                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(result);
                case OAuth20State.Finished:
                    return ImplictRedirect(result);
                case OAuth20State.UserDenied:
                    return View("UserDenied");
                default:
                    return BadRequest();
            }
        }

        [Route("oauth20/token")]
        [HttpPost]
        public async Task<IActionResult> Token(AccessTokenFromAuthorizationCodeRequestModel model)
        {
            if (model.GrantType == "authorization_code")
            {
                var message = new AccessTokenRequestMessage(model.Code)
                {
                    ClientId = model.ClientId,
                    ClientSecret = model.ClientSecret
                };
                var result = await _sagaBus.SendAsync<AccessTokenRequestMessage, OAuth20Result>(message);
                switch (result.State)
                {
                    case OAuth20State.AuthorizationCodeGenerated:
                        return BadRequest();
                    case OAuth20State.Finished:
                        return Ok(new AccessTokenResponseModel
                        {
                            TokenType = "bearer",
                            AccessToken = result.Token.AccessToken,
                            AuthrneticationToken = result.Token.AuthrneticationToken,
                            ExpireInSecond = (long)result.Token.ExpireIn.TotalSeconds,
                            Scope = string.Join(" ", result.Token.Scope.Select(s => s.ScopeName)),
                            RefreshToken = result.Token.RefreshToken
                        });
                }
            }
            return BadRequest();
        }
    }
}
