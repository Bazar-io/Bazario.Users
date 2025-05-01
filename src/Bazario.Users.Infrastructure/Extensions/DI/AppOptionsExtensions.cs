using Bazario.Users.Infrastructure.Persistence.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    internal static class AppOptionsExtensions
    {
        public static IServiceCollection AddAppOptions(
           this IServiceCollection services)
        {
            services.AddOptions<DbSettings>()
                .BindConfiguration(DbSettings.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}
