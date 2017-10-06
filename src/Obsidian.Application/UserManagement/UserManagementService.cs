using Obsidian.Application.Dto;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Obsidian.Application.UserManagement
{
    public class UserManagementService
    {
        private readonly IUserRepository _repo;

        public UserManagementService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> CreateAsync(UserCreationDto dto)
        {
            if ((await _repo.FindByUserNameAsync(dto.UserName) != null))
                throw new ArgumentException($"User name {dto.UserName} already exists.");

            var user = User.Create(Guid.NewGuid(), dto.UserName);
            user.SetPassword(dto.Password);
            await _repo.AddAsync(user);
            return user;
        }
    }
}
