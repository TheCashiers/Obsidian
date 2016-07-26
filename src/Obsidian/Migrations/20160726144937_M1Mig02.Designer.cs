using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Obsidian.Persistence;

namespace Obsidian.Migrations
{
    [DbContext(typeof(CommandModelDbContext))]
    [Migration("20160726144937_M1Mig02")]
    partial class M1Mig02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Obsidian.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<int>("Gender");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
