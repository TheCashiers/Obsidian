using Obsidian.Application.ClientManagement;
using Obsidian.Application.ScopeManagement;
using Obsidian.Application.UserManagement;
using Obsidian.Application.Authentication;
using Obsidian.Application.OAuth20.AuthorizationCodeGrant;
using Obsidian.Application.OAuth20.ImplicitGrant;
using Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant;
using Obsidian.Application.OAuth20.TokenVerification;

namespace Obsidian.Application.ProcessManagement
{
    public static class SagaBusExtensions
    {
        public static void RegisterSagas(this SagaBus bus)
        {
            bus.Register<AuthorizationCodeGrantSaga>()
               .Register<ImplicitGrantSaga>()
               .Register<ResourceOwnerPasswordCredentialsGrantSaga>()
               .Register<CreateUserSaga>()
               .Register<CreateClientSaga>()
               .Register<CreateScopeSaga>()
               .Register<UpdateUserSaga>()
               .Register<UpdateClientSaga>()
               .Register<UpdateScopeSaga>()
               .Register<PasswordAuthenticateSaga>()
               .Register<VerifyTokenSaga>();
        }
    }
}
