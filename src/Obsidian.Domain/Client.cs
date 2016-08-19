using System;
using Obsidian.Domain.Shared;

namespace Obsidian.Domain
{
    public class Client : IEntity, IAggregateRoot
    {
        public static Client Create(Guid id, string secret, string redirectUri)
             => new Client { Id = id, Secret = secret, RedirectUri = new Uri(redirectUri) };

        public Guid Id { get; private set; }
        public Uri RedirectUri { get; set; }
        public string Secret { get; private set; }
        public string DisplayName { get; set; }
    }
}