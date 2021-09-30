using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.ServiceImplementations;
using Service.ServiceInterfaces;

namespace Common.Injection.Service
{
    public static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddJobariaService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAnimalService, AnimalService>();
            return services;
        }
    }
}
