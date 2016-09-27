using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UpdateUserSaga : Saga, IStartsWith<UpdateUserProfileCommand, MessageResult<UpdateUserProfileCommand>>
                                      , IStartsWith<UpdateUserPasswordCommand, MessageResult<UpdateUserPasswordCommand>>
                                      , IStartsWith<UpdateUserNameCommand, MessageResult<UpdateUserNameCommand>>
    {

        private bool _isCompleted;

        private readonly IUserRepository _repo;

        public UpdateUserSaga(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<MessageResult<UpdateUserProfileCommand>> StartAsync(UpdateUserProfileCommand command)
        {
            _isCompleted = true;
            //check user
            var user = await _repo.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return new MessageResult<UpdateUserProfileCommand>
                {
                    Succeed = false,
                    Message = $"User of user id {command.UserId} doesn't exist."
                };
            }
            //edit profile
            user.UpdateProfile(command.NewProfile);
            await _repo.SaveAsync(user);
            return new MessageResult<UpdateUserProfileCommand>
            {
                Succeed = true,
                Message = $"Profile of User {user.Id} changed."
            };
        }

        public async Task<MessageResult<UpdateUserNameCommand>> StartAsync(UpdateUserNameCommand command)
        {
            _isCompleted = true;
            //check user
            if (!(await _repo.FindByUserNameAsync(command.UserName) == null))
                return new MessageResult<UpdateUserNameCommand>
                {
                    Succeed = false,
                    Message = $"User of user name {command.UserName} exists."
                };
            var user = await _repo.FindByIdAsync(command.UserId);
            if (user == null)
                return new MessageResult<UpdateUserNameCommand>
                {
                    Succeed = false,
                    Message = $"User of user id {command.UserId} doesn't exists."
                };
            //set user name
            user.UpdateUserName(command.UserName);
            return new MessageResult<UpdateUserNameCommand>
            {
                Succeed = true,
                Message = $"UserName of user {user.Id} changed."
            };
        }

        public async Task<MessageResult<UpdateUserPasswordCommand>> StartAsync(UpdateUserPasswordCommand command)
        {
            _isCompleted = true;
            //check user
            var user = await _repo.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return new MessageResult<UpdateUserPasswordCommand>
                {
                    Succeed = false,
                    Message = $"User of user id {command.UserId} doesn't exists."
                };
            }
            user.SetPassword(command.NewPassword);
            await _repo.SaveAsync(user);
            return new MessageResult<UpdateUserPasswordCommand>
            {
                Succeed = true,
                Message = $"Password of User {command.UserId} changed."
            };

        }
        protected override bool IsProcessCompleted()
        {
            return _isCompleted;
        }
    }
}
