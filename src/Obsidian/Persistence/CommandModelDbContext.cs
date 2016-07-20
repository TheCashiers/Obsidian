using Microsoft.EntityFrameworkCore;
using Obsidian.Domain;

namespace Obsidian.Persistence
{
    public class CommandModelDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instence of <see cref="CommandModelDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public CommandModelDbContext(DbContextOptions<CommandModelDbContext> options)
            : base(options)
        {
            Users = base.Set<User>();
        }

        /// <summary>
        /// Used to query and save instences of <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; }
    }
}