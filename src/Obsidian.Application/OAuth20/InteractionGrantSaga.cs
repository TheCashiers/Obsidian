using System;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;

namespace Obsidian.Application.OAuth20
{
    public abstract class InteractionGrantSaga : OAuth20Saga,
                                    IHandlerOf<OAuth20SignInMessage, OAuth20Result>,
                                    IHandlerOf<PermissionGrantMessage, OAuth20Result>,
                                    IHandlerOf<CancelMessage, OAuth20Result>

    {
        public InteractionGrantSaga(IClientRepository clientRepo,
                                    IUserRepository userRepo,
                                    IPermissionScopeRepository scopeRepo,
                                    OAuth20Service oauth20Service)
                                    : base(clientRepo, userRepo, scopeRepo, oauth20Service)
        {
        }


        protected string _redirectUri;

        protected Task<OAuth20Result> StartSagaAsync(InteractionGrantCommand command)
        {
            _redirectUri = command.RedirectUri;
            //check client and scopes
            var precondiction = TryLoadClient(command.ClientId, out _client)
               && TryLoadScopes(command.ScopeNames, out _requestedScopes);
            if (!precondiction)
            {
                GoToState(OAuth20State.Failed);
                return Task.FromResult(CurrentStateResult());
            }

            //next step
            GoToState(OAuth20State.RequireSignIn);
            return Task.FromResult(CurrentStateResult());
        }

        public async Task<OAuth20Result> HandleAsync(PermissionGrantMessage message)
        {
            //check granted scopes
            if (message.GrantedScopeNames.Count == 0
                || !TypLoadScopeFromNames(message.GrantedScopeNames, out _grantedScopes))
            {
                GoToState(OAuth20State.UserDenied);
                return CurrentStateResult();
            }

            //next step
            return await GrantPermissionAsync();
        }

        public async Task<OAuth20Result> HandleAsync(OAuth20SignInMessage message)
        {
            _user = message.User;
            //next step
            return await VerifyPermissionAsync();
        }

        public Task<OAuth20Result> HandleAsync(CancelMessage message)
        {
            GoToState(OAuth20State.Cancelled);
            var result = CurrentStateResult();
            result.CancelData = new OAuth20Result.CancelInfo
            {
                ClientId = _client.Id,
                ResponseType = GetResponseType(),
                Scopes = _requestedScopes.Select(s => s.ScopeName).ToList(),
                RedirectUri = _redirectUri
            };
            return Task.FromResult(result);
        }

        protected abstract string GetResponseType();

        protected async Task<OAuth20Result> VerifyPermissionAsync()
        {
            if (IsClientAuthorized(_user, _client, _requestedScopes))
            {
                _grantedScopes = _requestedScopes;
                return await GrantPermissionAsync();
            }
            GoToState(OAuth20State.RequirePermissionGrant);
            return PermissionGrantRequiredResult();
        }

        protected OAuth20Result PermissionGrantRequiredResult()
        {
            var result = CurrentStateResult();
            result.PermissionGrant = new OAuth20Result.PermissionGrantResult
            {
                Client = _client,
                Scopes = _requestedScopes
            };
            return result;
        }


        public bool ShouldHandle(PermissionGrantMessage message) => _state == OAuth20State.RequirePermissionGrant && message.SagaId == Id;

        public bool ShouldHandle(OAuth20SignInMessage message) => _state == OAuth20State.RequireSignIn && message.SagaId == Id;

        public bool ShouldHandle(CancelMessage message) => _state == OAuth20State.RequirePermissionGrant && message.SagaId == Id;

    }
}