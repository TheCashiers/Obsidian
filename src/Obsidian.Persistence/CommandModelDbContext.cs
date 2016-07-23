using Microsoft.EntityFrameworkCore;
using Obsidian.Domain;
using Obsidian.Persistence.Maps;

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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserMap.MapUser(modelBuilder);
        }

        /// <summary>
        /// Used to query and save instences of <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get { return base.Set<User>(); } }
    }
}