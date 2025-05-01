using Bazario.AspNetCore.Shared.Infrastructure.Behaviors.Validation.DependencyInjection;
using Bazario.AspNetCore.Shared.Infrastructure.MediatR.DependencyInjection;
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
            services.AddMappers();

            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatRServices(assembly);

            services.AddValidators(
                assembly: assembly,
                includeInternalTypes: true);

            services.AddValidationPipelineBehavior();

            return services;
        }
    }
}
