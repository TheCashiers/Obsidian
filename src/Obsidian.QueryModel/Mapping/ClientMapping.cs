﻿using AutoMapper;

namespace Obsidian.QueryModel.Mapping
{
    public static class ClientMapping
    {
        [QueryModelMapper]
        public static void Map(IMapperConfigurationExpression cfg) =>
            cfg.CreateMap<Domain.Client, Client>();
    }
}
