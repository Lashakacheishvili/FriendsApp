using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Common.Enums.Enums;

namespace Domain.Extensions
{
    public static class DbContextExtensions
    {
        public static void Seed(FriendsAppDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            if (!context.Roles.Any())
            {
                var Roles = Enum.GetValues(typeof(UserRole));

                var roleManager = serviceProvider.GetRequiredService<RoleManager<UserPermission>>();
                foreach (UserRole r in Roles)
                {
                    var role = r.ToString();
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new UserPermission { Name = role, NormalizedName = role.ToUpper() }).Wait();
                    }
                }
            }

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                     new User { Email= "katcheishvili.l@gmail.com", UserName="Lasha", PasswordHash="A1..erti...", PhoneNumber = "591079682", PhoneNumberConfirmed =true, EmailConfirmed = true },
                };
                var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                foreach (var user in users)
                {
                    var pwd = user.PasswordHash;
                    user.PasswordHash = null;
                    var result = _userManager.CreateAsync(user, pwd).Result;
                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, UserRole.User.ToString()).Wait();
                    }
                }
            }
        }
    }
}
