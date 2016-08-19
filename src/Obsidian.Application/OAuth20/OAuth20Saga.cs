using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Saga : Saga,
        IStartsWith<AuthorizeCommand, AuthorizeResult>,
        IHandlerOf<SignInMessage, SignInResult>,
        IHandlerOf<PermissionGrantMessage, PermissionGrantResult>,
        IHandlerOf<AccessTokenRequestMessage, AccessTokenResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionScopeRepository _scopeRepository;

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
        }

        private OAuth20Status _status;
        private User _user;
        private Client _client;
        private PermissionScope[] _requestedScopes;

        public async Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            if (command.GrantType != AuthorizationGrant.AuthorizationCode &&
                command.GrantType != AuthorizationGrant.Implicit)
            {
                throw new NotSupportedException();
            }

            _requestedScopes =
                await Task.WhenAll(command.ScopeNames.Select(s => _scopeRepository.FindByScopeNameAsync(s)));

            _client = await _clientRepository.FindByIdAsync(command.ClientId);
            if (_client == null)
            {
                _status = OAuth20Status.Fail;
                return new AuthorizeResult
                {
                    Status = _status,
                    ErrorMessage = "Client not found."
                };
            }
            if (command.UserName != null)
            {
                if (!await LoadUserAsync(command.UserName))
                {
                    return new AuthorizeResult
                    {
                        Status = _status,
                        ErrorMessage = "User not found."
                    };
                }
                if (_user.IsClientAuthorized(_client, command.ScopeNames))
                {
                    _status = OAuth20Status.AuthorizationCodeReturned;
                    return new AuthorizeResult { Status = _status };
                }
                _status = OAuth20Status.RequirePermissionGrant;
                return new AuthorizeResult { Status = _status, SagaId = Id, Client = _client, Scopes = _requestedScopes };
            }

            _status = OAuth20Status.RequireSignIn;
            return new AuthorizeResult { Status = _status, SagaId = Id };
        }

        private async Task<bool> LoadUserAsync(string userName)
        {
            if (_user == null)
            {
                _user = await _userRepository.FindByUserNameAsync(userName);
                if (_user == null)
                {
                    _status = OAuth20Status.Fail;
                    return false;
                }
                return true;
            }
            else
            {
                return userName == _user.UserName;
            }
        }

        #region SignIn

        public bool ShouldHandle(SignInMessage message) => _status == OAuth20Status.RequireSignIn;


        public async Task<SignInResult> HandleAsync(SignInMessage message)
        {
            if (!await LoadUserAsync(message.UserName) || !_user.VaildatePassword(message.Password))
            {
                return new SignInResult { Succeed = false };
            }

            var clientAuthorized = _user.IsClientAuthorized(_client, _requestedScopes.Select(s => s.ScopeName));
            _status = clientAuthorized ? OAuth20Status.AuthorizationCodeReturned : OAuth20Status.RequirePermissionGrant;

            var result = new SignInResult { Succeed = true, Status = _status };

            if (_status==OAuth20Status.AuthorizationCodeReturned)
            {
                result.RedirectUri = $"{_client.RedirectUri}?code={Id}";
            }
            else
            {
                result.Scopes = _requestedScopes;
            }
            return result;
        }

        #endregion SignIn

        #region Permission Grant

        public bool ShouldHandle(PermissionGrantMessage message) => _status == OAuth20Status.RequirePermissionGrant;

        public async Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            if (message.PermissionGranted)
            {
                _user.AuthorizedClients.Add(new ClientAuthorizationDetail { Client = _client, Scopes = _requestedScopes });
                await _userRepository.SaveAsync(_user);
                _status = OAuth20Status.AuthorizationCodeReturned;
                var result = new PermissionGrantResult { RedirectUri = $"{_client.RedirectUri}?code={Id}" };
                return result;
            }
            return new PermissionGrantResult();
        }

        #endregion Permission Grant

        #region Access Token Request

        public bool ShouldHandle(AccessTokenRequestMessage message) => _status == OAuth20Status.AuthorizationCodeReturned;

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            //vaildate client
            return Task.FromResult(new AccessTokenResult { AccessToken = GenerateAccessToken() });
        }

        const string key = "Obsidian.OAuth20.Jwt";

        private string GenerateAccessToken()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = _user.GetClaims();
            var jwt = new JwtSecurityToken(
                issuer: "Obsidian",
                audience: "ObsidianAud",
                claims: claims,
                signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        #endregion Access Token Request

        protected override bool IsProcessCompleted()
            => _status == OAuth20Status.Fail ||
            _status == OAuth20Status.Finished ||
            _status == OAuth20Status.ImplicitTokenReturned;
    }
}