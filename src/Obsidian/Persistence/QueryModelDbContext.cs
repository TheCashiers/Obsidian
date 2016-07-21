using Microsoft.EntityFrameworkCore;
using Obsidian.QueryModels;
using System.Linq;

namespace Obsidian.Persistence
{
    public class QueryModelDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instence of <see cref="QueryModelDbContext"/>.
        /// </summary>
        /// <param name="options"></param>
        public QueryModelDbContext(DbContextOptions<QueryModelDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Used to query instences of <see cref="User"/>.
        /// </summary>
        public IQueryable<User> Users { get { return base.Set<User>(); } }
    }
}