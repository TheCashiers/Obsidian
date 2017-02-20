using MongoDB.Bson.Serialization;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Reflection;

namespace Obsidian.Persistence.Mappings
{
    internal static class UserMapping
    {
        internal static void MapUser()
        {
            BsonClassMap.RegisterClassMap<Claim>(cm =>
            {
                cm.MapCreator(c => new Claim(c.Type, c.Value));
                cm.MapProperty(c => c.Type);
                cm.MapProperty(c => c.Value);
            });

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapField("_passwordHash").SetElementName("PasswordHash");
            });
        }
    }
}
