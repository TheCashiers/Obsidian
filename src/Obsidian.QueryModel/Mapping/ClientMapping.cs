using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.QueryModel.Mapping
{
    public static class ClientMapping
    {
        [QueryModelMapper]
        public static void Map(IMapperConfigurationExpression cfg) =>
            cfg.CreateMap<Domain.Client, Client>();
    }
}
