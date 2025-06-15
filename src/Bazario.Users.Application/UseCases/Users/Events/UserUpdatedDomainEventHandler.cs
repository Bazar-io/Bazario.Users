using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Contracts.UserUpdated;
using Bazario.Users.Domain.Users.DomainEvents;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Events
{
    internal sealed class UserUpdatedDomainEventHandler
        : IDomainEventHandler<UserUpdatedDomainEvent>
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<UserUpdatedDomainEventHandler> _logger;

        public UserUpdatedDomainEventHandler(
            IMessagePublisher messagePublisher,
            ILogger<UserUpdatedDomainEventHandler> logger)
        {
            _messagePublisher = messagePublisher;
            _logger = logger;
        }

        public async Task Handle(
            UserUpdatedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing user updated for identity service event for user {Id}", domainEvent.UserId);

            await _messagePublisher.PublishAsync(
                new UserUpdatedForIdentityServiceEvent(
                    domainEvent.UserId,
                    domainEvent.FirstName,
                    domainEvent.LastName,
                    domainEvent.Email,
                    domainEvent.PhoneNumber
                ),
                cancellationToken);

            _logger.LogInformation("Publishing user updated for listing service event for user {Id}", domainEvent.UserId);

            await _messagePublisher.PublishAsync(
                new UserUpdatedForListingServiceEvent(
                    domainEvent.UserId,
                    domainEvent.FirstName,
                    domainEvent.LastName,
                    domainEvent.PhoneNumber
                ),
                cancellationToken);
        }
    }
}
