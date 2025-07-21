using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options;
using Bazario.AspNetCore.Shared.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    public static class HealthChecksExtensions
    {
        public static IServiceCollection ConfigureAppHealthChecks(
            this IServiceCollection services)
        {
            var dbSettings = services.BuildServiceProvider().GetOptions<DbSettings>();
            var messageBrokerSettings = services.BuildServiceProvider().GetOptions<MessageBrokerSettings>();

            services
                .AddHealthChecks()
                .AddNpgSql(dbSettings.ConnectionString)
                .AddRabbitMQ(sp =>
                {
                    var messageBrokerConnection = sp.GetRequiredService<IRabbitMqConnection>();
                    return messageBrokerConnection.Connection;
                });

            return services;
        }
    }
}
