using MongoDB.Bson.Serialization;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Mappings
{
    internal static class UserMapping
    {
        internal static void MapUser()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapField("_passwordHash").SetElementName("PasswordHash");
            });
        }
    }
}
