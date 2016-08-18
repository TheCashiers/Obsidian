using OAuth20;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;

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

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
        }

        private OAuth20Status _status;
        private User _user;

        public async Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            var client = await _clientRepository.FindByIdAsync(command.ClientId);
            if (client == null)
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
                if (_user.IsClientAuthorized(client, command.ScopeNames))
                {
                    _status = OAuth20Status.CanRequestToken;
                    return new AuthorizeResult { Status = this._status };
                }
                _status = OAuth20Status.RequirePermissionGrant;
                return new AuthorizeResult { Status = this._status };
            }

            _status = OAuth20Status.RequireSignIn;
            return new AuthorizeResult { Status = this._status };
        }

        #region SignIn

        public bool ShouldHandle(SignInMessage message)
        {
            return message.SagaId == this.Id && _status == OAuth20Status.RequireSignIn;
        }

        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            if (_user.UserName != message.UserName || !_user.VaildatePassword(message.Password))
            {
                return Task.FromResult(new SignInResult { Succeed = false });
            }
            return Task.FromResult(new SignInResult { Succeed = true });

        }


        #endregion

        #region Permission Grant

        public bool ShouldHandle(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access Token Request

        public bool ShouldHandle(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override bool IsProcessCompleted()
        {
            return false;
        }

    }
}
