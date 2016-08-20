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

        private OAuth20Status _status;

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
        }

        protected override bool IsProcessCompleted()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}