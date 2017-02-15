using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System.Threading.Tasks;

namespace Obsidian.Application.ScopeManagement
{
    public class UpdateScopeSaga : Saga, IStartsWith<UpdateScopeCommand, MessageResult>
                                       
    {

        private bool _isCompleted;

        private readonly IPermissionScopeRepository _repo;

        public UpdateScopeSaga(IPermissionScopeRepository repo)
        {
            _repo = repo;
        }


        public async Task<MessageResult> StartAsync(UpdateScopeCommand command)
        {
            _isCompleted = true;
            var scope = await _repo.FindByIdAsync(command.Id);
            if (scope == null)
                return new MessageResult {
                       Succeed = false,
                       Message = $"PermissionScope of Id {command.Id} doesn't exist."
                };
            //update
            scope.Description = command.Description;
            scope.DisplayName = command.DisplayName;
            scope.ClaimTypes = command.ClaimTypes;
            await _repo.SaveAsync(scope);
            return new MessageResult {
                Succeed = true,
                Message = $"Information of PermissionScope {scope.Id} changed."
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;

    }
}
