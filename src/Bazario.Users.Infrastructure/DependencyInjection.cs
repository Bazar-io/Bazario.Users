using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.DependencyInjection;
using Bazario.AspNetCore.Shared.Infrastructure.Services.DependencyInjection;
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

            services.AddMessageBroker();

            services.AddMessageConsumers();

            services.AddPersistence();

            services.AddRepositories();

            services.AddUserContextServiceWithHttpContextAccessor();

            services.ConfigureAppBackgroundJobs();

            services.ConfigureAppHealthChecks();

            return services;
        }
    }
}
