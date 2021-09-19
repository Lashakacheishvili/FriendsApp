using Domain.Models;
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
    }
}
