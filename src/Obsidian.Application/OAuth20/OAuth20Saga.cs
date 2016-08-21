﻿using Microsoft.IdentityModel.Tokens;
using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Saga : Saga,
        IStartsWith<AuthorizeCommand, OAuth20Result>,
        IHandlerOf<SignInMessage, OAuth20Result>,
        IHandlerOf<PermissionGrantMessage, OAuth20Result>,
        IHandlerOf<AccessTokenRequestMessage, OAuth20Result>
    {
        #region External Dependencies

        private readonly IClientRepository _clientRepository;
        private readonly IPermissionScopeRepository _scopeRepository;
        private readonly IUserRepository _userRepository;

        #endregion External Dependencies

        private const string key = "Obsidian.OAuth20.Jwt";

        #region State Data

        private Client _client;
        private AuthorizationGrant _grantType;
        private IList<PermissionScope> _scopes;
        private OAuth20State _state;
        private User _user;

        #endregion State Data

        public OAuth20Saga(IClientRepository clientRepo,
                           IUserRepository userRepo,
                           IPermissionScopeRepository scopeRepo)
        {
            _clientRepository = clientRepo;
            _userRepository = userRepo;
            _scopeRepository = scopeRepo;
        }

        #region Handlers

        public Task<OAuth20Result> StartAsync(AuthorizeCommand command)
        {
            _grantType = command.GrantType;
            if (TryLoadClient(command.ClientId, out _client)
               && TryLoadScopes(command.ScopeNames, out _scopes))
            {
                if (command.UserName != null && TryLoadUser(command.UserName, out _user))
                {
                    return Task.FromResult(VerifyPermission());
                }
                GoToState(OAuth20State.RequireSignIn);
                return Task.FromResult(StateResult());
            }
            GoToState(OAuth20State.Failed);
            return Task.FromResult(StateResult());
        }

        public Task<OAuth20Result> HandleAsync(SignInMessage message)
        {
            if (TryLoadUser(message.UserName, out _user))
            {
                if (_user.VaildatePassword(message.Password))
                {
                    //TODO: signin here
                    return Task.FromResult(VerifyPermission());
                }
            }
            return Task.FromResult(StateResult());
        }

        public Task<OAuth20Result> HandleAsync(PermissionGrantMessage message)
        {
            if (message.PermissionGranted)
            {
                return Task.FromResult(GrantPermission());
            }
            GoToState(OAuth20State.UserDenied);
            return Task.FromResult(StateResult());
        }

        public Task<OAuth20Result> HandleAsync(AccessTokenRequestMessage message)
        {
            if (VerifyAccessTokenRequest(message.ClientId, message.ClientSecret, message.Code))
            {
                GoToState(OAuth20State.Finished);
                return Task.FromResult(AccessTokenResult());
            }
            return Task.FromResult(StateResult());
        }

        #endregion Handlers

        #region Should Handle

        public bool ShouldHandle(SignInMessage message) => _state == OAuth20State.RequireSignIn;

        public bool ShouldHandle(PermissionGrantMessage message) => _state == OAuth20State.RequirePermissionGrant;

        public bool ShouldHandle(AccessTokenRequestMessage message) => _state == OAuth20State.AuthorizationCodeGenerated;

        #endregion Should Handle

        #region Process

        private OAuth20Result VerifyPermission()
        {
            if (IsPermissionGranted(_user, _client, _scopes))
            {
                return GrantPermission();
            }
            GoToState(OAuth20State.RequirePermissionGrant);
            return StateResult();
        }

        private OAuth20Result GrantPermission()
        {
            if (_grantType == AuthorizationGrant.AuthorizationCode)
            {
                GoToState(OAuth20State.AuthorizationCodeGenerated);
                return AuthorizationCodeResult();
            }
            if (_grantType == AuthorizationGrant.Implicit)
            {
                GoToState(OAuth20State.Finished);
                return AccessTokenResult();
            }
            GoToState(OAuth20State.Failed);
            return StateResult();
        }

        #endregion Process

        protected override bool IsProcessCompleted()
            => _state == OAuth20State.Finished || _state == OAuth20State.UserDenied;

        #region Results

        private OAuth20Result StateResult()
                    => new OAuth20Result
                    {
                        SagaId = Id,
                        State = _state
                    };

        private OAuth20Result AuthorizationCodeResult()
        {
            var result = StateResult();
            result.RedirectUri = _client.RedirectUri.OriginalString;
            result.AuthorizationCode = GenerateAuthorizationCode();
            return result;
        }

        private OAuth20Result AccessTokenResult()
        {
            var result = StateResult();
            result.RedirectUri = _client.RedirectUri.OriginalString;
            result.Token = new OAuth20Result.TokenResult
            {
                Scope = _scopes,
                AccessToken = GenerateAccessToken()
            };
            return result;
        }

        #endregion Results

        #region Load Entities

        private bool TryLoadScopes(string[] scopeNames, out IList<PermissionScope> scopes)
        {
            scopes = scopeNames.Select(sn => _scopeRepository.FindByScopeNameAsync(sn).Result).ToList();
            return scopes.All(s => s != null);
        }

        private bool TryLoadClient(Guid clientId, out Client client)
        {
            client = _clientRepository.FindByIdAsync(clientId).Result;
            return client != null;
        }

        private bool TryLoadUser(string userName, out User user)
        {
            user = _userRepository.FindByUserNameAsync(userName).Result;
            return user != null;
        }

        #endregion Load Entities

        #region Percondictions

        private bool VerifyAccessTokenRequest(Guid clientId, string clientSecret, Guid authorizationCode)
            => clientId == _client.Id
            && clientSecret == _client.Secret
            && authorizationCode == Id;

        private bool IsPermissionGranted(User user, Client client, IList<PermissionScope> scopes)
            => user.IsClientAuthorized(client, scopes.Select(s => s.ScopeName));

        #endregion Percondictions

        #region Token Generators

        private string GenerateAccessToken()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = _user.GetClaims();
            var jwt = new JwtSecurityToken(
                issuer: "Obsidian",
                audience: "ObsidianAud",
                claims: claims,
                signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        private Guid GenerateAuthorizationCode() => Id;

        #endregion Token Generators

        private void GoToState(OAuth20State state) => _state = state;
    }
}