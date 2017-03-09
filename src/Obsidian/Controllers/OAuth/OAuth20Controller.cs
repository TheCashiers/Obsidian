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
            if (!TryConvertToGrantType(model.ResponseType, out var grantType))
            {
                return BadRequest();
            }

            OAuth20Result result;
            switch (grantType)
            {
                case AuthorizationGrant.AuthorizationCode:
                    result = await StartAuthorizationCodeGrantAsync(model);
                    break;

                case AuthorizationGrant.Implicit:
                    result = await StartImplicitGrantAsync(model);
                    break;

                default:
                    return BadRequest();
            }
            return await SignInPageView(result);
        }

        private async Task<IActionResult> SignInPageView(OAuth20Result result)
        {
            var context = _dataProtector.Protect(result.SagaId.ToString());
            var currentUser = await _signinService.GetCurrentUserAsync();
            var model = new OAuthSignInModel { ProtectedOAuthContext = context };
            if (currentUser != null)
            {
                model.IsAutoSignIn = true;
                model.UserName = currentUser.UserName;
            }
            return base.View("SignIn", model);
        }

        private async Task<OAuth20Result> StartImplicitGrantAsync(AuthorizationRequestModel model)
        {
            var command = new ImplicitGrantCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
                RedirectUri = model.RedirectUri
            };
            return await _sagaBus.InvokeAsync<ImplicitGrantCommand, OAuth20Result>(command);

        }

        private async Task<OAuth20Result> StartAuthorizationCodeGrantAsync(AuthorizationRequestModel model)
        {
            var command = new AuthorizationCodeGrantCommand
            {
                ClientId = model.ClientId,
                ScopeNames = model.Scope.Split(' '),
                RedirectUri = model.RedirectUri
            };
            return await _sagaBus.InvokeAsync<AuthorizationCodeGrantCommand, OAuth20Result>(command);
        }

        [HttpPost("oauth20/authorize")]
        [ValidateModel]
        public async Task<IActionResult> SignIn([FromForm]OAuthSignInModel model)
        {
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out var sagaId))
            {
                return BadRequest();
            }
            User currentUser;
            if (model.IsAutoSignIn)
            {
                currentUser = await _signinService.GetCurrentUserAsync();
                if (currentUser.UserName != model.UserName)
                {
                    return BadRequest();
                }
            }
            else
            {
                var authResult = await PasswordautnenticateAsync(model.UserName, model.Password);
                if (!authResult.IsCredentialValid)
                {
                    ModelState.AddModelError(nameof(OAuthSignInModel.UserName), "Invaild user name");
                    ModelState.AddModelError(nameof(OAuthSignInModel.Password), "Or invaild password");
                    return View("SignIn");
                }
                currentUser = authResult.User;
            }
            return await OAuth20SignInCore(sagaId, currentUser, model.RememberMe);
        }

        private async Task<IActionResult> OAuth20SignInCore(Guid sagaId, User user, bool isPersistent)
        {
            await _signinService.CookieSignInAsync(AuthenticationSchemes.OAuth20Cookie, user, isPersistent);

            var message = new OAuth20SignInMessage(sagaId, user);

            var oauth20Result = await _sagaBus.SendAsync<OAuth20SignInMessage, OAuth20Result>(message);
            switch (oauth20Result.State)
            {
                case OAuth20State.RequirePermissionGrant:
                    return PermissionGrantView(oauth20Result);

                case OAuth20State.AuthorizationCodeGenerated:
                    return AuthorizationCodeRedirect(oauth20Result);

                case OAuth20State.Finished:
                    return ImplictTokenRedirect(oauth20Result);

                default:
                    return BadRequest();
            }
        }

        private async Task<AuthenticationResult> PasswordautnenticateAsync(string userName, string password)
        {
            var command = new PasswordAuthenticateCommand
            {
                UserName = userName,
                Password = password
            };
            return await _sagaBus.InvokeAsync<PasswordAuthenticateCommand, AuthenticationResult>(command);
        }

        [HttpPost("oauth20/authorize/permission")]
        [ValidateModel]
        public async Task<IActionResult> PermissionGrant([FromForm]PermissionGrantModel model)
        {
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out var sagaId))
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
                    return ImplictTokenRedirect(result);

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
            var context = _dataProtector.Unprotect(model.ProtectedOAuthContext);
            if (!Guid.TryParse(context, out var sagaId))
            {
                return BadRequest();
            }
            var message = new CancelMessage(sagaId);
            var result = await _sagaBus.SendAsync<CancelMessage, OAuth20Result>(message);
            switch (result.State)
            {
                case OAuth20State.Cancelled:
                    await _signinService.CookieSignOutCurrentUserAsync(AuthenticationSchemes.OAuth20Cookie);
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
                if (!authResult.IsCredentialValid)
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

        private bool TryConvertToGrantType(string responseType, out AuthorizationGrant grantType)
        {
            if ("code".Equals(responseType, StringComparison.OrdinalIgnoreCase))
            {
                grantType = AuthorizationGrant.AuthorizationCode;
                return true;
            }
            else if ("token".Equals(responseType, StringComparison.OrdinalIgnoreCase))
            {
                grantType = AuthorizationGrant.Implicit;
                return true;
            }
            else
                grantType = default(AuthorizationGrant);
            return false;
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

        private IActionResult ImplictTokenRedirect(OAuth20Result result)
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
    }
}