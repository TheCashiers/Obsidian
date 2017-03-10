using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public abstract class OAuth20Saga : Saga
    {
        #region External Dependencies

        private readonly IClientRepository _clientRepository;
        private readonly IPermissionScopeRepository _scopeRepository;
        private readonly IUserRepository _userRepository;
        private readonly OAuth20Service _oauth20Service;

        #endregion External Dependencies

        #region State Data

        protected Client _client;
        protected IList<PermissionScope> _requestedScopes;
        protected OAuth20State _state;
        protected User _user;
        protected IList<PermissionScope> _grantedScopes;

        #endregion State Data

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo,
                           OAuth20Service oauth20Service)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
            _oauth20Service = oauth20Service;
        }

        protected bool TypLoadScopeFromNames(IList<string> grantedScopeNames, out IList<PermissionScope> scopes)
        {
            scopes = new List<PermissionScope>();
            foreach (var scopeName in grantedScopeNames)
            {
                var scope = _requestedScopes.SingleOrDefault(s => s.ScopeName == scopeName);
                if (scope != null)
                    scopes.Add(scope);
                else
                    return false;
            }
            return true;
        }

        protected abstract Task<OAuth20Result> GrantPermissionAsync();

        protected async Task SaveUserAsync() => await _userRepository.SaveAsync(_user);

        protected override bool IsProcessCompleted()
            => _state == OAuth20State.Finished || _state == OAuth20State.UserDenied || _state == OAuth20State.Cancelled;

        #region Results

        protected OAuth20Result CurrentStateResult()
                    => new OAuth20Result
                    {
                        SagaId = Id,
                        State = _state
                    };

        protected OAuth20Result AccessTokenResult()
        {
            var result = CurrentStateResult();
            result.Token = new OAuth20Result.TokenResult
            {
                Scope = _grantedScopes,
                ExpireIn = TimeSpan.FromMinutes(5),
                AccessToken = GenerateAccessToken()
            };
            return result;
        }

        #endregion Results

        #region Load Entities

        protected bool TryLoadScopes(string[] scopeNames, out IList<PermissionScope> scopes)
        {
            scopes = scopeNames.Select(sn => _scopeRepository.FindByScopeNameAsync(sn).Result).ToList();
            return scopes.All(s => s != null);
        }

        protected bool TryLoadClient(Guid clientId, out Client client)
        {
            client = _clientRepository.FindByIdAsync(clientId).Result;
            return client != null;
        }

        protected bool TryLoadUser(string userName, out User user)
        {
            user = _userRepository.FindByUserNameAsync(userName).Result;
            return user != null;
        }

        #endregion Load Entities

        #region Percondictions

        protected bool IsClientGranted(User user, Client client, IList<PermissionScope> scopes)
            => user.IsClientGranted(client, scopes.Select(s => s.ScopeName));

        #endregion Percondictions

        private string GenerateAccessToken() => _oauth20Service.GenerateAccessToken(_user, _grantedScopes);

        protected void GoToState(OAuth20State state) => _state = state;
    }
}