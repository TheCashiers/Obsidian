using Microsoft.EntityFrameworkCore;
using Obsidian.Domain;

namespace Obsidian.Persistence.Maps
{
    public static class UserMap
    {
        public static void MapUser(ModelBuilder builder)
        {
            var typeBuilder = builder.Entity<User>();

            typeBuilder.HasKey(u => u.Id);
            typeBuilder.Property<string>("PasswordHash");
            typeBuilder.ToTable("Users");
        }
    }
}