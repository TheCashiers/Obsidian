using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Domain.Repositories
{
    public interface IPermissionScopeRepository : IRepository<PermissionScope>
    {
        Task<PermissionScope> FindByScopeNameAsync(string scopeName);
    }
}
