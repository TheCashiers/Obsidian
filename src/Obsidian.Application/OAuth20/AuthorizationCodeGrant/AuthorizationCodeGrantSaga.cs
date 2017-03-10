using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20.AuthorizationCodeGrant
{
    public class AuthorizationCodeGrantSaga : InteractionGrantSaga,
                                              IStartsWith<AuthorizationCodeGrantCommand, OAuth20Result>,
                                              IHandlerOf<OAuth20SignInMessage, OAuth20Result>,
                                              IHandlerOf<PermissionGrantMessage, OAuth20Result>,
                                              IHandlerOf<AccessTokenRequestMessage, OAuth20Result>

    {
        public AuthorizationCodeGrantSaga(IClientRepository clientRepo,
                                IUserRepository userRepo,
                                IPermissionScopeRepository scopeRepo,
                                OAuth20Service oauth20Service)
                                : base(clientRepo, userRepo, scopeRepo, oauth20Service)
        {
        }

        public async Task<OAuth20Result> StartAsync(AuthorizationCodeGrantCommand command)
            => await StartSagaAsync(command);

        protected override async Task<OAuth20Result> GrantPermissionAsync()
        {
            _user.GrantClient(_client, _grantedScopes);
            await SaveUserAsync();
            GoToState(OAuth20State.AuthorizationCodeGenerated);
            return AuthorizationCodeResult();
        }

        private OAuth20Result AuthorizationCodeResult()
        {
            var result = CurrentStateResult();
            result.RedirectUri = _redirectUri;
            result.AuthorizationCode = GenerateAuthorizationCode();
            return result;
        }

        private Guid GenerateAuthorizationCode() => Id;

        public Task<OAuth20Result> HandleAsync(AccessTokenRequestMessage message)
        {
            if (VerifyAccessTokenRequest(message.ClientId, message.ClientSecret, message.Code, message.RedirectUri))
            {
                GoToState(OAuth20State.Finished);
                var result = AccessTokenResult();
                result.RedirectUri = _redirectUri;
                return Task.FromResult(result);
            }
            return Task.FromResult(CurrentStateResult());
        }

        public bool ShouldHandle(AccessTokenRequestMessage message) => _state == OAuth20State.AuthorizationCodeGenerated && message.SagaId == Id;

        private bool VerifyAccessTokenRequest(Guid clientId, string clientSecret, Guid authorizationCode, string redirectUri)
            => clientId == _client.Id
            && clientSecret == _client.Secret
            && authorizationCode == Id
            && redirectUri == _redirectUri;

        protected override string GetResponseType() => "code";
    }
}