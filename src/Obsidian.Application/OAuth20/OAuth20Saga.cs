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

        private OAuth20State _state;

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
        }

        private void GoToState(OAuth20State state) => _state = state;

        protected override bool IsProcessCompleted()
            => _state == OAuth20State.Finished || _state == OAuth20State.UserDenied;

        public Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(SignInMessage message) => _state == OAuth20State.RequireSignIn;

        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(PermissionGrantMessage message) => _state == OAuth20State.RequirePermissionGrant;

        public Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(AccessTokenRequestMessage message) => _state == OAuth20State.AuthorizationCodeGenerated;

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}