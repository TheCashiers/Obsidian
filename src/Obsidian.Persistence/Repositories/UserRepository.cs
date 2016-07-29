using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;
using Microsoft.EntityFrameworkCore;

namespace Obsidian.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CommandModelDbContext _dbContext;

        public UserRepository(CommandModelDbContext ctx)
        {
            _dbContext = ctx;
        }

        public async Task AddAsync(User aggregate)
        {
            _dbContext.Users.Add(aggregate);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(User aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByUserNameAsync(string userName)
            => _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == userName);

        public Task<IQueryable<User>> QueryAllAsync()
            => Task.FromResult(_dbContext.Users.AsNoTracking().AsQueryable());

        public Task SaveAsync(User aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
