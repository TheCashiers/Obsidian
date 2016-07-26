using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;

namespace Obsidian.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CommandModelDbContext _dbContext;

        public UserRepository(CommandModelDbContext ctx)
        {
            _dbContext = ctx;
        }

        public bool Add(User aggregate)
        {
            _dbContext.Users.Add(aggregate);
            return _dbContext.SaveChanges() == 1;
        }

        public bool Delete(User aggregate)
        {
            throw new NotImplementedException();
        }

        public User FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> QueryAll()
        {
            throw new NotImplementedException();
        }

        public bool Save(User aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
