using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Injection.Service
{
    public static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddJobariaService(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
