using Microsoft.EntityFrameworkCore;
using Obsidian.QueryModel.Persistence.Maps;
using System.Linq;

namespace Obsidian.QueryModel.Persistence
{
    public class QueryModelDbContext : DbContext, IQueryModelDbContext
    {
        /// <summary>
        /// Initializes a new instence of <see cref="QueryModelDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public QueryModelDbContext(DbContextOptions<QueryModelDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserMap.MapUser(modelBuilder);
        }

        /// <summary>
        /// Used to query instences of <see cref="User"/>.
        /// </summary>
        public IQueryable<User> Users { get { return base.Set<User>(); } }
    }
}