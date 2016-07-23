using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Obsidian.Domain;
using Obsidian.Persistence;
using System;

namespace Obsidian
{
    internal static class TestData
    {
        internal static void InsertTestData(this IApplicationBuilder builder)
        {
            var db = builder.ApplicationServices.GetService<CommandModelDbContext>();
            var demoUsers = new[]
            {
                User.Create(Guid.NewGuid(),"bob"),
                User.Create(Guid.NewGuid(),"dick"),
                User.Create(Guid.NewGuid(),"sam")
            };

            db.Users.AddRange(demoUsers);
            db.SaveChanges();
        }
    }
}