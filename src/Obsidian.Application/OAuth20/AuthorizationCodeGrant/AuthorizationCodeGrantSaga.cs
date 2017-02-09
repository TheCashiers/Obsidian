using System;
using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;

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
        {
            //check client and scopes
            var precondiction = TryLoadClient(command.ClientId, out _client)
               && TryLoadScopes(command.ScopeNames, out _requestedScopes);
            if (!precondiction)
            {
                GoToState(OAuth20State.Failed);
                return CurrentStateResult();
            }

            //check user
            if (TryLoadUser(command.UserName, out _user))
            {
                //if user logged in, skip next step
                await VerifyPermissionAsync();
            }
            //next step
            GoToState(OAuth20State.RequireSignIn);
            return CurrentStateResult();
        }

    
        public async override Task<OAuth20Result> HandleAsync(PermissionGrantMessage message)
        {
            //check granted scopes
            if (!TypLoadScopeFromNames(message.GrantedScopeNames, out _grantedScopes))
            {
                GoToState(OAuth20State.UserDenied);
                return CurrentStateResult();
            }

            //next step
            return await GrantPermissionAsync();

        }

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
            result.RedirectUri = _client.RedirectUri.OriginalString;
            result.AuthorizationCode = GenerateAuthorizationCode();
            return result;
        }

        private Guid GenerateAuthorizationCode() => Id;


        public Task<OAuth20Result> HandleAsync(AccessTokenRequestMessage message)
        {
            if (VerifyAccessTokenRequest(message.ClientId, message.ClientSecret, message.Code))
            {
                GoToState(OAuth20State.Finished);
                return Task.FromResult(AccessTokenResult());
            }
            return Task.FromResult(CurrentStateResult());
        }

        public bool ShouldHandle(AccessTokenRequestMessage message) => _state == OAuth20State.AuthorizationCodeGenerated && message.SagaId == Id;


        private bool VerifyAccessTokenRequest(Guid clientId, string clientSecret, Guid authorizationCode)
            => clientId == _client.Id
            && clientSecret == _client.Secret
            && authorizationCode == Id;

    }
}