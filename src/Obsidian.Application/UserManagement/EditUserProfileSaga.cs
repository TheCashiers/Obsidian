﻿using Obsidian.Application.ProcessManagement;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class EditUserProfileSaga : Saga, IStartsWith<EditUserProfileCommand, UserProfileEditonResult>
    {

        private bool _isCompleted;

        private readonly IUserRepository _repo;

        public EditUserProfileSaga(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserProfileEditonResult> StartAsync(EditUserProfileCommand command)
        {
            _isCompleted = true;
            //check user
            var user = await _repo.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return new UserProfileEditonResult {
                    Succeed = false,
                    Message = $"User of user id {command.UserId} doesn't exist."
                };
            }
            //edit profile
            user.UpdateProfile(command.NewProfile);
            await _repo.SaveAsync(user);
            return new UserProfileEditonResult {
                Succeed = true,
                Message = $"Profile of User {user.Id} changed."
            };
        }

        protected override bool IsProcessCompleted()
        {
            return _isCompleted;
        }
    }
}