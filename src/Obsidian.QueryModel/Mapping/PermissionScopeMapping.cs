using AutoMapper;

namespace Obsidian.QueryModel.Mapping
{
    public static class PermissionScopeMapping
    {
        [QueryModelMapper]
        public static void Map(IMapperConfigurationExpression cfg) =>
            cfg.CreateMap<Domain.PermissionScope, PermissionScope>();
    }
}
