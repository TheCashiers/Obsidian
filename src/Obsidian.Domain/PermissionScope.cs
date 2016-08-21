using Obsidian.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain
{
    public class PermissionScope : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string ScopeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public static PermissionScope Create(Guid id, string scopeName, string displayName, string description)
            => new PermissionScope
            {
                Id = id,
                ScopeName = scopeName,
                DisplayName = displayName,
                Description = description
            };
    }
}
