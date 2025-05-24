using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Contracts.UserRegistered;
using Bazario.Users.Application.UseCases.Users.Commands.InsertUser;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Infrastructure.Consumers
{
    public sealed class UserRegisteredForUserServiceEventConsumer 
        : IMessageConsumer<UserRegisteredForUserServiceEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<UserRegisteredForUserServiceEventConsumer> _logger;

        public UserRegisteredForUserServiceEventConsumer(
            ISender sender,
            ILogger<UserRegisteredForUserServiceEventConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task ConsumeAsync(
            UserRegisteredForUserServiceEvent message,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "User registered event received. UserId: {UserId}",
                message.UserId);

            var command = new InsertUserCommand(
                UserId: message.UserId,
                Email: message.Email,
                FirstName: message.FirstName,
                LastName: message.LastName,
                BirthDate: message.BirthDate,
                PhoneNumber: message.PhoneNumber);

            var result = await _sender.Send(
                command,
                cancellationToken);

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Failed to insert user. UserId: {UserId}, Error: {Error}",
                    message.UserId,
                    result.Error);

                return;
            }

            _logger.LogInformation(
                "User inserted successfully. UserId: {UserId}",
                message.UserId);
        }
    }
}
