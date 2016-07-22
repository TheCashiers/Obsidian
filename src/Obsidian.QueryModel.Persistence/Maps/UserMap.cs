using Microsoft.EntityFrameworkCore;

namespace Obsidian.QueryModel.Persistence.Maps
{
    public static class UserMap
    {
        public static void MapUser(ModelBuilder builder)
        {
            var typeBuilder = builder.Entity<User>();

            typeBuilder.HasKey(u => u.Id);
        }
    }
}