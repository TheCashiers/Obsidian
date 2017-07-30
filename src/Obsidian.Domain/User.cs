using Obsidian.Domain.Misc;
using Obsidian.Foundation.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Obsidian.Domain
{
    public class User : IEntity, IAggregateRoot
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public static User Create(Guid id, string userName) => new User { Id = id, UserName = userName };

        #region Props

        /// <summary>
        /// Represents the unique identifier of the <see cref="User"/>.
        /// </summary>
        public Guid Id { get; private set; }

        public void SetPassword(string password)
        {
            _passwordHash = _passwordHasher.HashPasword(password);
        }

        public void UpdateUserName(string username)
        {
            UserName = username;
        }

        public void UpdateProfile(UserProfile profile)
        {
            Profile = profile;
        }

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        public string UserName { get; private set; }

        public UserProfile Profile { get; private set; } = new UserProfile();

        public IList<ClientAuthorizationDetail> GrantedClients { get; private set; }
            = new List<ClientAuthorizationDetail>();

        public IList<Claim> Claims { get; private set; }
            = new List<Claim>();

        #endregion Props

        private string _passwordHash;

        public bool VaildatePassword(string password)
        {
            var hash = _passwordHasher.HashPasword(password);
            return hash == _passwordHash;
        }

        public bool IsClientGranted(Client client, IEnumerable<string> scopeNames)
        {
            var grantDetail = GrantedClients.SingleOrDefault(gd => gd.ClientId == client.Id);
            if (grantDetail != null)
            {
                var grantedScopeNames = grantDetail.ScopeNames;
                var inputSet = new HashSet<string>(scopeNames);
                return inputSet.IsSubsetOf(grantedScopeNames);
            }
            return false;
        }

        public void GrantClient(Client client, IEnumerable<PermissionScope> scopes)
        {
            var grantDetail = GrantedClients.SingleOrDefault(gd => gd.ClientId == client.Id);
            if (grantDetail != null)
            {
                grantDetail.ScopeNames = scopes.Select(s => s.ScopeName).ToList();
            }
            else
            {
                GrantedClients.Add(new ClientAuthorizationDetail
                {
                    ClientId = client.Id,
                    ScopeNames = scopes.Select(s => s.ScopeName).ToList()
                });
            }
        }

        public IEnumerable<Claim> GetClaims(IEnumerable<PermissionScope> scopes)
            => GetClaims(scopes.SelectMany(s => s.Claims).Distinct());

        public IEnumerable<Claim> GetClaims(IEnumerable<(string Type, string Value)> requestedClaims)
        {
            var idClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name,UserName)
            };
            return Claims.Where(c => requestedClaims.Any(r => r.Type == c.Type && r.Value == c.Value)).Union(idClaims);
        }

        #region Equality

        public override bool Equals(object obj) => this.EntityEquals(obj);

        public override int GetHashCode() => Id.GetHashCode();

        #endregion Equality
    }
}