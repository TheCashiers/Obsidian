using System;
using Obsidian.Domain;

namespace Obsidian.Application.OAuth20
{
    public class AuthorizationCodeContext
    {
        public AuthorizationCodeContext(Client client, User user, string[] scope)
        {
            ClientId = client.Id;
            UserId = user.Id;
            Scope = scope;
        }

        public Guid ClientId { get; }
        public string[] Scope { get; }
        public Guid UserId { get; }

    }
}