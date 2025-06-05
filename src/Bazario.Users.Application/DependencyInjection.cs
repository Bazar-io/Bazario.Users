using Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection;
using Bazario.AspNetCore.Shared.Application.Messaging.DependencyInjection;
using Bazario.Users.Application.Extensions.DI;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bazario.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMappers();

            services.AddMessaging(assembly);

            // Reminder to add DomainEventHandlers

            services.AddValidators(
                assembly: assembly,
                includeInternalTypes: true);

            services.AddValidationPipelineBehavior();

            return services;
        }
    }
}
