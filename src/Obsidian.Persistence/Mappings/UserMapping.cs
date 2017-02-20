using MongoDB.Bson.Serialization;
using Obsidian.Domain;
using System.Security.Claims;

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
