using Domain;
using Domain.Models;
using FriendsApi.AuthConfig;
using FriendsApi.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FriendsApi.Injection.DB
{
    public static class DBInjectionExtensions
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            string apiHost = configuration.GetValue<string>("APIHost").TrimEnd('/') + "/";

            services.AddDbContext<FriendsAppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, UserPermission>(o =>
            {
                o.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
            })
            .AddEntityFrameworkStores<FriendsAppDbContext>()
            .AddDefaultTokenProviders();
            services.AddIdentityServer(a =>
            {
                a.IssuerUri = apiHost;
            })
          .AddDeveloperSigningCredential()
          .AddInMemoryPersistedGrants()
          .AddInMemoryApiResources(ClientConfiguration.GetApiResources())
          .AddInMemoryClients(ClientConfiguration.GetClients())
          .AddInMemoryApiScopes(ClientConfiguration.GetApiScopes())
          .AddResourceOwnerValidator<AppResourceOwnerPasswordValidator>();
            return services;
        }
    }
}
