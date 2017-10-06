using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Obsidian.Application.ScopeManagement
{
    public class ScopeService
    {
        private readonly IPermissionScopeRepository _repo;
        public ScopeService(IPermissionScopeRepository repo)
        {
            _repo = repo;
        }

        public async Task<PermissionScope> CreateScope(string scopeName,string displayName,string description,IList<ObsidianClaim> claims)
        {
            var scope = PermissionScope.Create(Guid.NewGuid(),scopeName,displayName, description);
            scope.Claims = claims;
            await _repo.AddAsync(scope);
            return scope;
        }

        public async Task<PermissionScope> UpdateScope(Guid id, string displayName, string description, IList<ObsidianClaim> claims)
        {
            var scope = await _repo.FindByIdAsync(id);
            if (scope == null) throw new EntityNotFoundException($"Can not find client with id {id}");
            scope.Description = description;
            scope.DisplayName = displayName;
            scope.Claims = claims;
            await _repo.SaveAsync(scope);
            return scope;
        }
    }
}
