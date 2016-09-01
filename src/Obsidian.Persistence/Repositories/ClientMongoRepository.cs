using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Repositories
{
    [Repository(typeof(IClientRepository))]
    public class ClientMongoRepository : MongoRepository<Client>, IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;

        public ClientMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Client>(MongoCollectionNames.Clients);
        }

        protected override IMongoCollection<Client> Collection => _collection;

    }
}