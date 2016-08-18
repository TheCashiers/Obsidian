﻿using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
        }

        private OAuth20Status _status;
        private User _user;
        private Client _client;
        private PermissionScope[] _requestedScopes;

        public async Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            _requestedScopes = await Task.WhenAll(command.ScopeNames.Select(s => _scopeRepository.FindByScopeNameAsync(s)));
            _client = await _clientRepository.FindByIdAsync(command.ClientId);
            if (_client == null)
            {
                _status = OAuth20Status.Fail;
                return new AuthorizeResult
                {
                    Status = this._status,
                    ErrorMessage = "Client not found."
                };
            }
            if (command.UserName != null)
            {
                _user = await _userRepository.FindByUserNameAsync(command.UserName);
                if (_user == null)
                {
                    _status = OAuth20Status.Fail;
                    return new AuthorizeResult
                    {
                        Status = this._status,
                        ErrorMessage = "User not found."
                    };
                }
                if (_user.IsClientAuthorized(_client, command.ScopeNames))
                {
                    _status = OAuth20Status.CanRequestToken;
                    return new AuthorizeResult { Status = this._status };
                }
                _status = OAuth20Status.RequirePermissionGrant;
                return new AuthorizeResult { Status = this._status, SagaId = Id };
            }

            _status = OAuth20Status.RequireSignIn;
            return new AuthorizeResult { Status = this._status, SagaId = Id };
        }

        #region SignIn

        public bool ShouldHandle(SignInMessage message) => _status == OAuth20Status.RequireSignIn;


        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            if (_user.UserName != message.UserName || !_user.VaildatePassword(message.Password))
            {
                return Task.FromResult(new SignInResult { Succeed = false });
            }

            var clientAuthorized = _user.IsClientAuthorized(_client, _requestedScopes.Select(s => s.ScopeName));
            _status = clientAuthorized ? OAuth20Status.CanRequestToken : OAuth20Status.RequirePermissionGrant;

            var result = new SignInResult { Succeed = true, Status = _status };

            if (clientAuthorized)//and if response type is code
            {
                result.RedirectUri = $"{_client.RedirectUri}?code={Id}";
            }
            else
            {
                result.Scopes = _requestedScopes;
            }
            return Task.FromResult(result);
        }

        #endregion SignIn

        #region Permission Grant

        public bool ShouldHandle(PermissionGrantMessage message) => _status == OAuth20Status.RequirePermissionGrant;

        public async Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            if (message.PermissionGranted)
            {
                _user.AuthorizedClients.Add(new ClientAuthorizationDetail { Client = _client, Scopes = _requestedScopes });
                await _userRepository.SaveAsync(_user);
                _status = OAuth20Status.CanRequestToken;
                var result = new PermissionGrantResult { RedirectUri = $"{_client.RedirectUri}?code={Id}" };
                return result;
            }
            return new PermissionGrantResult();
        }

        #endregion Permission Grant

        #region Access Token Request

        public bool ShouldHandle(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion Access Token Request

        protected override bool IsProcessCompleted()
            => _status == OAuth20Status.Fail || _status == OAuth20Status.Finished;
    }
}