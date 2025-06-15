using Bazario.AspNetCore.Shared.Authentication.Options;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox.Options;
using Bazario.AspNetCore.Shared.Options.DependencyInjection;

namespace Bazario.Users.WebAPI.Extensions
{
    public static class ValidateOptionsExtensions
    {
        public static IServiceProvider ValidateAppOptions(this IServiceProvider serviceProvider)
        {
            serviceProvider.ValidateOptionsOnStart<DbSettings>();
            serviceProvider.ValidateOptionsOnStart<MessageBrokerSettings>();
            serviceProvider.ValidateOptionsOnStart<JwtSettings>();
            serviceProvider.ValidateOptionsOnStart<OutboxSettings>();

            return serviceProvider;
        }
    }
}
