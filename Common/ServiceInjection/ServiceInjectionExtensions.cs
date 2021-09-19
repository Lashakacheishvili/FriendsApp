using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.ServiceInjection
{
    public static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddJobariaService(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
