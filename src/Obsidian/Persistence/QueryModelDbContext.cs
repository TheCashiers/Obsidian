using Microsoft.EntityFrameworkCore;
using Obsidian.QueryModels;

namespace Obsidian.Persistence
{
    public class QueryModelDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instence of <see cref="QueryModelDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public QueryModelDbContext(DbContextOptions<QueryModelDbContext> options)
            :base(options)
        {
            Users = base.Set<User>();
        }

        /// <summary>
        /// Used to query instences of <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; }
    }
}