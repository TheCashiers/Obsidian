using Microsoft.EntityFrameworkCore;
using Obsidian.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Persistence.Maps
{
    public class ClientMap
    {
        public static void MapClient(ModelBuilder builder)
        {
            var typeBuilder = builder.Entity<Client>();

            typeBuilder.HasKey(c => c.Id);
            typeBuilder.ToTable("Clients");
        }
    }
}
