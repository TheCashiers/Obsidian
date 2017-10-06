using Obsidian.Application.Dto;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation;
using Obsidian.Foundation.Collections;
using System;
using System.Linq;
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

        public async Task SetUserNameAsync(Guid id, string newUserName)
        {
            var user = await _repo.FindByIdAsync(id);
            if (user == null)
                throw new EntityNotFoundException($"Can not find user with id {id}");

            if ((await _repo.FindByUserNameAsync(newUserName) != null))
                throw new ArgumentException($"User name {newUserName} already exists.", nameof(newUserName));

            user.UpdateUserName(newUserName);
            await _repo.SaveAsync(user);
        }

        public async Task SetPasswordAsync(Guid id, string newPassword)
        {
            var user = await _repo.FindByIdAsync(id);
            if (user == null)
                throw new EntityNotFoundException($"Can not find user with id {id}");

            user.SetPassword(newPassword);
            await _repo.SaveAsync(user);
        }

        public async Task UpdateUserProfileAsync(Guid id, UserProfile profile)
        {
            var user = await _repo.FindByIdAsync(id);
            if (user == null)
                throw new EntityNotFoundException($"Can not find user with id {id}");

            user.UpdateProfile(profile);
            await _repo.SaveAsync(user);
        }

        public async Task UpdateUserClaimsAsync(Guid id, UpdateUserClaimsDto dto)
        {
            var user = await _repo.FindByIdAsync(id);
            if (user == null)
                throw new EntityNotFoundException($"Can not find user with id {id}");

            user.Claims.Clear();
            dto.Claims.Select(c => new ObsidianClaim
            {
                Type = c.ClaimType,
                Value = c.ClaimValue
            })
            .ForEach(user.Claims.Add);
            await _repo.SaveAsync(user);
        }
    }
}
