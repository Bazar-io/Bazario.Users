using Bazario.Users.Infrastructure.Extensions.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddAppOptions();

            services.AddPersistence();

            services.AddRepositories();

            return services;
        }
    }
}
