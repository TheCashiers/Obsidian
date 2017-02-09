using Obsidian.Application.ProcessManagement;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ScopeManagement
{
    public class CreateScopeSaga : Saga, IStartsWith<CreateScopeCommand, ScopeCreationResult>
    {

        private bool _isCompleted;

        private readonly IPermissionScopeRepository _repo;

        public CreateScopeSaga(IPermissionScopeRepository repo)
        {
            _repo = repo;
        }

        public async Task<ScopeCreationResult> StartAsync(CreateScopeCommand command)
        {
            _isCompleted = true;
            if(await _repo.FindByScopeNameAsync(command.ScopeName) != null) {
                return new ScopeCreationResult
                {
                    Succeed = false,
                    Message = $"Scope of scope name {command.ScopeName} already exists."
                };
            }
            var scope = PermissionScope.Create(Guid.NewGuid(), command.ScopeName, command.DisplayName, command.Description);
            command.ClaimTypes.ToList().ForEach(s => scope.ClaimTypes.Add(s));
            await _repo.AddAsync(scope);
            return new ScopeCreationResult
            {
                Succeed = true,
                Message = $"Scope successfully created.",
                Id = scope.Id
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;
    }
}
