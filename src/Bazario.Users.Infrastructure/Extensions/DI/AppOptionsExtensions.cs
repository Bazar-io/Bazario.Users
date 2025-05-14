using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options;
using Bazario.AspNetCore.Shared.Options.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    internal static class AppOptionsExtensions
    {
        public static IServiceCollection AddAppOptions(
           this IServiceCollection services)
        {
            services.ConfigureValidatableOptions<DbSettings, DbSettingsValidator>(
                DbSettings.SectionName);
            services.ConfigureValidatableOptions<MessageBrokerSettings, MessageBrokerSettingsValidator>(
                MessageBrokerSettings.SectionName);

            return services;
        }
    }
}
