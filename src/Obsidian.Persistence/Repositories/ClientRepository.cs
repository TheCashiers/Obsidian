using Obsidian.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obsidian.Domain;
using Microsoft.EntityFrameworkCore;

namespace Obsidian.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CommandModelDbContext _dbContext;

        public ClientRepository(CommandModelDbContext ctx)
        {
            _dbContext = ctx;
        }

        public async Task AddAsync(Client aggregate)
        {
            _dbContext.Clients.Add(aggregate);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Client aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindByIdAsync(Guid id) => _dbContext.Clients.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);

        public Task<IQueryable<Client>> QueryAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Client aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
