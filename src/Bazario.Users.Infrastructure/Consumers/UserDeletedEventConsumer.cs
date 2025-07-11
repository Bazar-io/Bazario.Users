using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Contracts.UserDeleted;
using Bazario.Users.Application.UseCases.Users.Commands.DeleteUser;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Infrastructure.Consumers
{
    internal sealed class UserDeletedEventConsumer
        : IMessageConsumer<UserDeletedEvent>
    {
        private readonly ILogger<UserDeletedEventConsumer> _logger;
        private readonly ICommandHandler<DeleteUserCommand> _commandHandler;

        public UserDeletedEventConsumer(
            ILogger<UserDeletedEventConsumer> logger,
            ICommandHandler<DeleteUserCommand> commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        public async Task ConsumeAsync(
            UserDeletedEvent message,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Admin registered event received. UserId: {UserId}",
                message.UserId);

            var command = new DeleteUserCommand(message.UserId);

            var result = await _commandHandler.Handle(
                command, cancellationToken);

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Failed to delete user with UserId: {UserId}. Error: {Error}",
                    message.UserId, result.Error);

                return;
            }

            _logger.LogInformation(
                "Successfully deleted user with UserId: {UserId}",
                message.UserId);
        }
    }
}
