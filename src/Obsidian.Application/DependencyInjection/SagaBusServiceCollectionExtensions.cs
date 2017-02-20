using Microsoft.Extensions.DependencyInjection;
using Obsidian.Application.Authentication;
using Obsidian.Application.ClientManagement;
using Obsidian.Application.OAuth20;
using Obsidian.Application.OAuth20.AuthorizationCodeGrant;
using Obsidian.Application.OAuth20.ImplicitGrant;
using Obsidian.Application.OAuth20.ResourceOwnerPasswordCredentialsGrant;
using Obsidian.Application.OAuth20.TokenVerification;
using Obsidian.Application.ProcessManagement;
using Obsidian.Application.ScopeManagement;
using Obsidian.Application.UserManagement;

namespace Obsidian.Application.DependencyInjection
{
    public static class SagaBusServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="SagaBus"/> as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSagaBus(this IServiceCollection services) => services.AddSingleton<SagaBus>();

        /// <summary>
        /// Register sagas as a service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add servicesto.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSaga(this IServiceCollection services)
            => services.AddTransient<AuthorizationCodeGrantSaga>()
                       .AddTransient<ImplicitGrantSaga>()
                       .AddTransient<ResourceOwnerPasswordCredentialsGrantSaga>()
                       .AddTransient<OAuth20Service>().AddTransient<OAuth20Configuration>()
                       .AddTransient<CreateUserSaga>()
                       .AddTransient<CreateClientSaga>()
                       .AddTransient<CreateScopeSaga>()
                       .AddTransient<UpdateUserSaga>()
                       .AddTransient<UpdateClientSaga>()
                       .AddTransient<UpdateScopeSaga>()
                       .AddTransient<PasswordAuthenticateSaga>()
                       .AddTransient<VerifyTokenSaga>();
    }
}