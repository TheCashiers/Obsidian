using System.Threading.Tasks;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;

namespace Obsidian.Application.OAuth20.ImplicitGrant
{
    public class ImplicitGrantSaga : InteractionGrantSaga,
                                     IStartsWith<ImplicitGrantCommand, OAuth20Result>,
                                     IHandlerOf<OAuth20SignInMessage, OAuth20Result>,
                                     IHandlerOf<PermissionGrantMessage, OAuth20Result>
    {
        public ImplicitGrantSaga(IClientRepository clientRepo,
                                IUserRepository userRepo,
                                IPermissionScopeRepository scopeRepo,
                                OAuth20Service oauth20Service)
                                : base(clientRepo, userRepo, scopeRepo, oauth20Service)
        {
        }

        public async Task<OAuth20Result> StartAsync(ImplicitGrantCommand command)
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
            GoToState(OAuth20State.Finished);
            return AccessTokenResult();
        }

    }
}