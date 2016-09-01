using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.QueryModel.Mapping
{
    public static class PermissionScopeMapping
    {
        [QueryModelMapper]
        public static void Map() => Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<Domain.PermissionScope, PermissionScope>();
        });
    }
}
