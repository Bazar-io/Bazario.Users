using Bazario.AspNetCore.Shared.Options.DependencyInjection;
using Bazario.Users.Infrastructure.Persistence.Options;
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

            return services;
        }
    }
}
