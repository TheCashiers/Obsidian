using System;
using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;

namespace Obsidian.Application.OAuth20
{
    public abstract class InteractionGrantSaga : OAuth20Saga,
                                    IHandlerOf<OAuth20SignInMessage, OAuth20Result>,
                                    IHandlerOf<PermissionGrantMessage, OAuth20Result>

    {
        public InteractionGrantSaga(IClientRepository clientRepo, IUserRepository userRepo, IPermissionScopeRepository scopeRepo, OAuth20Service oauth20Service) : base(clientRepo, userRepo, scopeRepo, oauth20Service)
        {
        }


        protected string _redirectUri;

        public abstract Task<OAuth20Result> HandleAsync(PermissionGrantMessage message);

        public async Task<OAuth20Result> HandleAsync(OAuth20SignInMessage message)
        {
            //check user
            if (TryLoadUser(message.UserName, out _user))
            {
                //current state is RequireSignIn
                return CurrentStateResult();
            }
            //next step
            return await VerifyPermissionAsync();
        }


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


    }
}