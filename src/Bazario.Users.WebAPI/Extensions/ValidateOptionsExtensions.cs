using Bazario.AspNetCore.Shared.Infrastructure.Options.DependencyInjection;
using Bazario.Users.Infrastructure.Persistence.Options;

namespace Bazario.Users.WebAPI.Extensions
{
    public static class ValidateOptionsExtensions
    {
        public static IServiceProvider ValidateAppOptions(this IServiceProvider serviceProvider)
        {
            serviceProvider.ValidateOptionsOnStart<DbSettings>();

            return serviceProvider;
        }
    }
}
