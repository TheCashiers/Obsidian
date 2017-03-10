using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant
{
    public class ResourceOwnerPasswordCredentialsGrantSaga : OAuth20Saga,
                                                             IStartsWith<ResourceOwnerPasswordCredentialsGrantCommand, OAuth20Result>
    {
        public ResourceOwnerPasswordCredentialsGrantSaga(IClientRepository clientRepo,
                                                         IUserRepository userRepo,
                                                         IPermissionScopeRepository scopeRepo,
                                                         OAuth20Service oauth20Service)
                                                         : base(clientRepo, userRepo, scopeRepo, oauth20Service)
        {
        }

        public async Task<OAuth20Result> StartAsync(ResourceOwnerPasswordCredentialsGrantCommand command)
        {
            _user = command.User;
            //check client and scopes
            var precondiction =
                TryLoadClient(command.ClientId, out _client)
                && TryLoadScopes(command.ScopeNames, out _requestedScopes)
                && _client.Secret == command.ClientSecret;

            if (!precondiction)
            {
                GoToState(OAuth20State.Failed);
                return CurrentStateResult();
            }

            //next step
            GoToState(OAuth20State.Finished);
            return await GrantPermissionAsync();
        }

        protected override async Task<OAuth20Result> GrantPermissionAsync()
        {
            _grantedScopes = _requestedScopes;
            if (!IsClientGranted(_user, _client, _grantedScopes))
            {
                _user.GrantClient(_client, _requestedScopes);
                await SaveUserAsync();
            }
            return AccessTokenResult();
        }
    }
}