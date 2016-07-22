using System.Linq;

namespace Obsidian.QueryModel
{
    public interface IQueryModelDbContext
    {
        IQueryable<User> Users { get; }
    }
}