using Bazario.AspNetCore.Shared.Contracts.UserRegistered;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.DependencyInjection;
using Bazario.Users.Infrastructure.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    public static class MessageConsumersExtensions
    {
        public static IServiceCollection AddMessageConsumers(
            this IServiceCollection services)
        {
            services.AddMessageConsumer<UserRegisteredForUserServiceEvent, UserRegisteredForUserServiceEventConsumer>();

            return services;
        }
    }
}
