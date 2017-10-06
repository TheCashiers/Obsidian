using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.Collections;
using Obsidian.Foundation.ProcessManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserSaga : Saga, IStartsWith<UpdateUserClaimCommand, MessageResult>
    {
        private bool _isCompleted;

        private readonly IUserRepository _repo;

        public UpdateUserSaga(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<MessageResult> StartAsync(UpdateUserClaimCommand command)
        {
            _isCompleted = true;
            //check user
            var user = await _repo.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return new MessageResult
                {
                    Succeed = false,
                    Message = $"User of user id {command.UserId} doesn't exist."
                };
            }
            //edit claims
            user.Claims.Clear();
            command.Claims.Select(c => new ObsidianClaim { Type = c.Key, Value = c.Value }).ForEach(user.Claims.Add);
            await _repo.SaveAsync(user);
            return new MessageResult
            {
                Succeed = true,
                Message = $"Claims of User {user.Id} changed."
            };
        }

        protected override bool IsProcessCompleted()
        {
            return _isCompleted;
        }
    }
}