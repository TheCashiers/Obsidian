using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Obsidian.Application.OAuth20;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Obsidian.Application.Authentication;
using Obsidian.Application.OAuth20.AuthorizationCodeGrant;
using Obsidian.Application.OAuth20.ImplicitGrant;
using Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant;
using Obsidian.Models.OAuth;
using Obsidian.Application.Services;
using Obsidian.Application.OAuth20.TokenVerification;

#pragma warning disable CS1591
namespace Obsidian.Controllers.OAuth
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OAuth20Controller : Controller
    {
        private readonly IDataProtector _dataProtector;
        private readonly SagaBus _sagaBus;

        private readonly ISignInService _signinService;

        public OAuth20Controller(IDataProtectionProvider dataProtectionProvicer, SagaBus bus, ISignInService signinService)
        {
            _dataProtector = dataProtectionProvicer.CreateProtector("Obsidian.OAuth.Context.Key");
            _sagaBus = bus;
            _signinService = signinService;
        }


        [HttpGet("oauth20/authorize")]
        [ValidateModel]
        [Authorize(ActiveAuthenticationSchemes = AuthenticationSchemes.OAuth20Cookie)]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize([FromQuery]AuthorizationRequestModel model)
        {
            AuthorizationGrant grantType;
            try
            {
                grantType = ParseGrantTypeFromResponseType(model.ResponseType);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }

            switch (grantType)
            {
                case AuthorizationGrant.AuthorizationCode:
                    return await HandleAuthorizationCodeGrantAsync(model);
                case AuthorizationGrant.Implicit:
                    return await HandleImplicitGrantAsync(model);
                default:
                    return BadRequest();
            }
        }

        private async Task<IActionResult> HandleImplicitGrantAsync(AuthorizationRequestModel model)
        {
            var command = new ImplicitGrantCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
                RedirectUri = model.RedirectUri
            };

            if (User.Identity.IsAuthenticated)
            {
                //add user info to login page
            }

            var result = await _sagaBus.InvokeAsync<ImplicitGrantCommand, OAuth20Result>(command);
            var context = _dataProtector.Protect(result.SagaId.ToString());
            switch (result.State)
            {
                case OAuth20State.RequireSignIn:
                    return View("SignIn", new OAuthSignInModel { ProtectedOAuthContext = context });

                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(result);

                case OAuth20State.Finished:
                    return ImplictRedirect(result);

                default:
                    return BadRequest();
            }
        }

        private async Task<IActionResult> HandleAuthorizationCodeGrantAsync(AuthorizationRequestModel model)
        {
            var command = new AuthorizationCodeGrantCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
                RedirectUri = model.RedirectUri
            };

            if (User.Identity.IsAuthenticated)
            {
                //add user info to login page
            }

            var result = await _sagaBus.InvokeAsync<AuthorizationCodeGrantCommand, OAuth20Result>(command);
            var context = _dataProtector.Protect(result.SagaId.ToString());
            switch (result.State)
            {
                case OAuth20State.RequireSignIn:
                    return View("SignIn", new OAuthSignInModel { ProtectedOAuthContext = context });

                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(result);

                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(result);

                default:
                    return BadRequest();
            }
        }

        [HttpPost("oauth20/authorize")]
        [ValidateModel]
        public async Task<IActionResult> SignIn([FromForm]OAuthSignInModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var command = new PasswordAuthenticateCommand
            {
                UserName = model.UserName,
                Password = model.Password
            };
            var authResult = await _sagaBus.InvokeAsync<PasswordAuthenticateCommand, AuthenticationResult>(command);
            if (!authResult.IsCredentialVaild)
            {
                ModelState.AddModelError(nameof(OAuthSignInModel.UserName), "Invaild user name");
                ModelState.AddModelError(nameof(OAuthSignInModel.Password), "Or invaild password");
                return View("SignIn");
            }
            await _signinService.CookieSignInAsync(AuthenticationSchemes.OAuth20Cookie, authResult.User, model.RememberMe);

            var message = new OAuth20SignInMessage(sagaId)
            {
                UserName = model.UserName,
            };
            
            var oauth20Result = await _sagaBus.SendAsync<OAuth20SignInMessage, OAuth20Result>(message);
            switch (oauth20Result.State)
            {
                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(oauth20Result);

                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(oauth20Result);

                case OAuth20State.Finished:
                    return ImplictRedirect(oauth20Result);

                default:
                    return BadRequest();
            }
        }

        [HttpPost("oauth20/authorize/permission")]
        [ValidateModel]
        public async Task<IActionResult> PermissionGrant([FromForm]PermissionGrantModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var message = new PermissionGrantMessage(sagaId)
            {
                GrantedScopeNames = model.GrantedScopeNames ?? new List<string>()
            };
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

        [HttpPost("oauth20/cancel")]
        [ValidateModel]
        public async Task<IActionResult> Cancel([FromForm]CancelModel model)
        {
            Guid sagaId;
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out sagaId))
            {
                return BadRequest();
            }
            var message = new CancelMessage(sagaId);
            var result = await _sagaBus.SendAsync<CancelMessage, OAuth20Result>(message);
            switch (result.State)
            {
                case OAuth20State.Cancelled:
                    return Redirect(CancelRedirectUrl(result.CancelData));
                default:
                    return BadRequest();
            }
        }

        private string CancelRedirectUrl(OAuth20Result.CancelInfo cancelData)
            => $"/oauth20/authorize?response_type={cancelData.ResponseType}&redirect_uri={cancelData.RedirectUri}&client_id={cancelData.ClientId}&scope={string.Join(" ", cancelData.Scopes)}";

        [HttpPost("oauth20/token")]
        [ValidateModel]
        public async Task<IActionResult> Token([FromBody]AuthorizationCodeGrantRequestModel model)
        {
            if ("authorization_code".Equals(model.GrantType, StringComparison.OrdinalIgnoreCase))
            {
                var message = new AccessTokenRequestMessage(model.Code)
                {
                    ClientId = model.ClientId,
                    ClientSecret = model.ClientSecret,
                    Code = model.Code,
                    RedirectUri = model.RedirectUri
                };
                var result = await _sagaBus.SendAsync<AccessTokenRequestMessage, OAuth20Result>(message);
                switch (result.State)
                {
                    case OAuth20State.AuthorizationCodeGenerated:
                        return BadRequest();

                    case OAuth20State.Finished:
                        return Ok(TokenResponseModel.FromOAuth20Result(result));
                }
            }
            return BadRequest();
        }

        [HttpPost("oauth20/token_resource_owner_credential")]
        [ValidateModel]
        public async Task<IActionResult> Token([FromBody]ResourceOwnerPasswordCredentialsGrantRequestModel model)
        {
            if ("password".Equals(model.GrantType, StringComparison.OrdinalIgnoreCase))
            {
                var signinCommand = new PasswordAuthenticateCommand
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                var authResult = await _sagaBus.InvokeAsync<PasswordAuthenticateCommand, AuthenticationResult>(signinCommand);
                if (!authResult.IsCredentialVaild)
                {
                    return Unauthorized();
                }
                var authorizeCommand = new ResourceOwnerPasswordCredentialsGrantCommand
                {
                    ClientId = model.ClientId,
                    UserName = authResult.User.UserName,
                    ClientSecret = model.ClientSecret,
                    ScopeNames = model.Scope.Split(' ')
                };
                var oauthResult = await _sagaBus.InvokeAsync<ResourceOwnerPasswordCredentialsGrantCommand, OAuth20Result>(authorizeCommand);
                switch (oauthResult.State)
                {
                    case OAuth20State.Finished:
                        return Ok(TokenResponseModel.FromOAuth20Result(oauthResult));
                    default:
                        return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("oauth20/token/verify")]
        [ValidateModel]
        public async Task<IActionResult> VerifyToken(TokenVerificationModel model)
        {
            var command = new VerifyTokenCommand
            {
                ClientId = model.ClientId,
                Token = model.Token
            };
            var result = await _sagaBus.InvokeAsync<VerifyTokenCommand, bool>(command);
            return Ok(result);
        }

        private AuthorizationGrant ParseGrantTypeFromResponseType(string responseType)
        {
            if ("code".Equals(responseType, StringComparison.OrdinalIgnoreCase))
            {
                return AuthorizationGrant.AuthorizationCode;
            }
            else if ("token".Equals(responseType, StringComparison.OrdinalIgnoreCase))
            {
                return AuthorizationGrant.Implicit;
            }
            else
                throw new ArgumentOutOfRangeException(nameof(responseType),
                            "Only code and token can be accepted as response type.");
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

        #region Results

        private IActionResult PermissionGrantView(OAuth20Result result)
        {
            var context = _dataProtector.Protect(result.SagaId.ToString());
            ViewBag.Client = result.PermissionGrant.Client;
            ViewBag.Scopes = result.PermissionGrant.Scopes;
            return View("PermissionGrant", new PermissionGrantModel { ProtectedOAuthContext = context });
        }

        private IActionResult ImplictRedirect(OAuth20Result result)
        {
            var tokenRedirectUri = BuildImplictReturnUri(result);
            return Redirect(tokenRedirectUri);
        }

        private IActionResult AuthorizationCodeRedirect(OAuth20Result result)
        {
            var codeRedirectUri = $"{result.RedirectUri}?code={result.AuthorizationCode}";
            return Redirect(codeRedirectUri);
        }

        #endregion Results

        #region Front-end debug

        [Route("oauth20/authorize/frontend/signin")]
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
            var client = Client.Create(Guid.NewGuid(), "http://za-pt.org/exchange");
            client.DisplayName = "iTech";
            ViewBag.Client = client;
            ViewBag.Scopes = new[] {
                PermissionScope.Create(Guid.NewGuid(),"obsidian.basicinfo","Basic Information","Includes you name and gender."),
                PermissionScope.Create(Guid.NewGuid(),"obsidian.email","Email address","Your email address."),
                PermissionScope.Create(Guid.NewGuid(),"obsidian.admin","Admin access","Manage the system.")
            };
            return View("PermissionGrant", new PermissionGrantModel { ProtectedOAuthContext = context });
        }

        [Route("oauth20/authorize/frontend/denied")]
        [HttpGet]
        public IActionResult FrontEndDeniedDebug()
        {
            return View("UserDenied");
        }

        #endregion Front-end debug
    }
}