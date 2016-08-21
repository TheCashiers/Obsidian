using Obsidian.Domain.Misc;
using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Represents the username used to login.
        /// </summary>
        public string UserName { get; private set; }

        public UserProfile Profile { get; private set; }

        public IList<ClientAuthorizationDetail> AuthorizedClients { get; private set; }

        #endregion Props

        private string _passwordHash;


        public bool VaildatePassword(string password)
        {
            var hash = _passwordHasher.HashPasword(password);
            return hash == _passwordHash;
        }

        public bool IsClientAuthorized(Client client, IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public IList<Claim> GetClaims()
        {
            return new List<Claim>();
        }
    }
}