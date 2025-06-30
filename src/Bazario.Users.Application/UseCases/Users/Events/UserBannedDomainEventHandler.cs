using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Contracts.UserBanned;
using Bazario.Users.Domain.Users.DomainEvents;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Events
{
    internal sealed class UserBannedDomainEventHandler
        : IDomainEventHandler<UserBannedDomainEvent>
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<UserBannedDomainEventHandler> _logger;

        public UserBannedDomainEventHandler(
            IMessagePublisher messagePublisher,
            ILogger<UserBannedDomainEventHandler> logger)
        {
            _messagePublisher = messagePublisher;
            _logger = logger;
        }

        public async Task Handle(
            UserBannedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing user banned event for user {Id}", domainEvent.UserId);

            await _messagePublisher.PublishAsync(
                new UserBannedEvent(
                    domainEvent.UserId,
                    domainEvent.ExpiresAt
                ),
                exchangeType: MessageBrokerExchangeType.Fanout,
                cancellationToken: cancellationToken);
        }
    }
}
