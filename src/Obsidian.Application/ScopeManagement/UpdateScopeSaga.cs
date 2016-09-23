using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ScopeManagement
{
    public class UpdateScopeSaga : Saga, IStartsWith<UpdateScopeInfoCommand, MessageResult<UpdateScopeInfoCommand>>
                                       , IStartsWith<UpdateScopeClaimsCommand,MessageResult<UpdateScopeClaimsCommand>>
    {

        private bool _isCompleted;

        private readonly IPermissionScopeRepository _repo;

        public UpdateScopeSaga(IPermissionScopeRepository repo)
        {
            _repo = repo;
        }

        public async Task<MessageResult<UpdateScopeClaimsCommand>> StartAsync(UpdateScopeClaimsCommand command)
        {
            _isCompleted = true;
            var scope = await _repo.FindByIdAsync(command.Id);
            if (scope == null)
                return new MessageResult<UpdateScopeClaimsCommand>
                {
                    Succeed = false,
                    Message = $"PermissionScope of Id {command.Id} doesn't exist."
                };
            //add
            if (command.IsAdd)
            {
                if (scope.ClaimTypes.Contains(command.Claim))
                    return new MessageResult<UpdateScopeClaimsCommand> {
                        Succeed = false,
                        Message = $"Claim {command.Claim} exists in PermissionScope {scope.Id}"
                    };
                scope.ClaimTypes.Add(command.Claim);
                return new MessageResult<UpdateScopeClaimsCommand>
                {
                    Succeed = true,
                    Message = $"Claims of PermissionScope {scope.Id} changed."
                };
            }
            //remove
            else
            {
                if(!scope.ClaimTypes.Contains(command.Claim))
                    return new MessageResult<UpdateScopeClaimsCommand>
                    {
                        Succeed = false,
                        Message = $"Claim {command.Claim} doesn't exist in PermissionScope {scope.Id}"
                    };
                scope.ClaimTypes.Remove(command.Claim);
                return new MessageResult<UpdateScopeClaimsCommand>
                {
                    Succeed = true,
                    Message = $"Claims of PermissionScope {scope.Id} changed."
                };
            }
        }

        public async Task<MessageResult<UpdateScopeInfoCommand>> StartAsync(UpdateScopeInfoCommand command)
        {
            _isCompleted = true;
            var scope = await _repo.FindByIdAsync(command.Id);
            if (scope == null)
                return new MessageResult<UpdateScopeInfoCommand> {
                       Succeed = false,
                       Message = $"PermissionScope of Id {command.Id} doesn't exist."
                };
            //update
            scope.Description = command.Description;
            scope.DisplayName = command.DisplayName;
            await _repo.SaveAsync(scope);
            return new MessageResult<UpdateScopeInfoCommand> {
                Succeed = true,
                Message = $"Information of PermissionScope {scope.Id} changed."
            };
        }

        protected override bool IsProcessCompleted() => _isCompleted;

    }
}
