using Obsidian.Domain.Misc;
using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Linq;

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
        [ClaimType(ClaimTypes.NameIdentifier)]
        public Guid Id { get; private set; }

        public void SetPassword(string password)
        {
            _passwordHash = _passwordHasher.HashPasword(password);
        }

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        [ClaimType(ClaimTypes.Name)]
        public string UserName { get; private set; }

        public UserProfile Profile { get; private set; } = new UserProfile();

        public IList<ClientAuthorizationDetail> AuthorizedClients { get; private set; }

        #endregion Props

        private static Dictionary<string, MethodInfo> profileClaimGetters
            = MetadataHelper.GetClaimPropertyGetters<UserProfile>();

        private static Dictionary<string, MethodInfo> userClaimGetters
            = MetadataHelper.GetClaimPropertyGetters<User>();

        private string _passwordHash;


        public bool VaildatePassword(string password)
        {
            var hash = _passwordHasher.HashPasword(password);
            return hash == _passwordHash;
        }

        public bool IsClientAuthorized(Client client, IEnumerable<string> scopeNames)
        {
            var grantDetail = AuthorizedClients.SingleOrDefault(gd => gd.Client == client);
            if (grantDetail != null)
            {
                var grantedScopeNames = grantDetail.Scopes.Select(s => s.ScopeName);
                var inputSet = new HashSet<string>(scopeNames);
                return inputSet.IsSubsetOf(grantedScopeNames);
            }
            return false;
        }

        public IEnumerable<Claim> GetClaims(IEnumerable<PermissionScope> scopes)
        {
            var types = scopes.SelectMany(s => s.ClaimTypes);
            var userClaims = GetClaimsFromObject(types, this);
            var profileClaims = GetClaimsFromObject(types, Profile);
            return Enumerable.Union(userClaims, profileClaims);
        }

        private static IEnumerable<Claim> GetClaimsFromObject(IEnumerable<string> requestedTypes, object obj) =>
            userClaimGetters.Where(g => requestedTypes.Contains(g.Key)).Select(g =>
             {
                 var value = g.Value.Invoke(obj, null);
                 return new Claim(g.Key, value?.ToString() ?? "");
             });


        public void GrantClient(Client client, IEnumerable<PermissionScope> scopes)
        {
            var grantDetail = AuthorizedClients.SingleOrDefault(gd => gd.Client == client);
            if (grantDetail != null)
            {
                grantDetail.Scopes = scopes.ToList();
            }
            else
            {
                AuthorizedClients.Add(new ClientAuthorizationDetail
                {
                    Client = client,
                    Scopes = scopes.ToList()
                });
            }
        }

        #region Equality

        public override bool Equals(object obj) => this.EntityEquals(obj);

        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}