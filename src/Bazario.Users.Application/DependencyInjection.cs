using Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection;
using Bazario.AspNetCore.Shared.Application.DomainEvents.DependencyInjection;
using Bazario.AspNetCore.Shared.Application.Mappers.DependencyInjection;
using Bazario.AspNetCore.Shared.Application.Messaging.DependencyInjection;
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

            services.AddMessaging(assembly);

            services.AddDomainEventHandlers(assembly);

            services.AddValidators(
                assembly: assembly,
                includeInternalTypes: true);

            services.AddValidationPipelineBehavior();

            services.AddMappers(assembly);

            return services;
        }
    }
}
