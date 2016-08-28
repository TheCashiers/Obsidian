using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.QueryModel.Mapping
{
    public static class UserMapping
    {
        [QueryModelMapper]
        public static void Map() => Mapper.Initialize(cfg =>
        {
            cfg.CreateMap<Domain.User, User>()
            .ForMember(u => u.DisplayName, c => c.MapFrom(u => u.Profile.GivenName + " " + u.Profile.SurnName))
            .ForMember(u => u.Gender, c => c.MapFrom(u => u.Profile.Gender));
        });
    }
}
