using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.DependencyInjection;
using Bazario.Users.Infrastructure.Extensions.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddAppOptions();

            services.AddMessageBroker(assembly);

            services.AddPersistence();

            services.AddRepositories();

            return services;
        }
    }
}
