using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FriendsAppDbContext : IdentityDbContext<User, UserPermission, int>
    {
        public FriendsAppDbContext(DbContextOptions<FriendsAppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Animal>  Animals { get; set; }
        public DbSet<UserAnimal> UserAnimals { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            var login = builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            login.HasKey("LoginProvider", "ProviderKey");
            var token = builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            token.HasKey("UserId", "LoginProvider", "Name");
            var role = builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            role.HasKey("UserId", "RoleId");
            builder.Entity<UserPermission>().ToTable("UserPermission");

        }
    }
}
