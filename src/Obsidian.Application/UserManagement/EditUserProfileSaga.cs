using Obsidian.Application.ProcessManagement;
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

        public Task<UserProfileEditonResult> StartAsync(EditUserProfileCommand command)
        {
            throw new NotImplementedException();
        }

        protected override bool IsProcessCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
