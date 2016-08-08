using System;
using Obsidian.Domain.Shared;

namespace Obsidian.Domain
{
    public class Client : IEntity
    {
        public Guid Id { get; private set; }
        public Uri RedirectUri { get; set; }
    }
}