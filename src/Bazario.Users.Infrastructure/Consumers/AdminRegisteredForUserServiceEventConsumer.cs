using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Contracts.AdminRegistered;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.Users.Application.UseCases.Users.Commands.InsertUser;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Infrastructure.Consumers
{
    internal sealed class AdminRegisteredForUserServiceEventConsumer
        : IMessageConsumer<AdminRegisteredForUserServiceEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<AdminRegisteredForUserServiceEvent> _logger;

        public AdminRegisteredForUserServiceEventConsumer(
            ISender sender,
            ILogger<AdminRegisteredForUserServiceEvent> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task ConsumeAsync(
            AdminRegisteredForUserServiceEvent message,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Admin registered event received. UserId: {UserId}",
                message.UserId);

            var command = new InsertUserCommand(
                UserId: message.UserId,
                Email: message.Email,
                FirstName: message.FirstName,
                LastName: message.LastName,
                BirthDate: message.BirthDate,
                PhoneNumber: message.PhoneNumber,
                Role: Role.Admin);

            var result = await _sender.Send(
                command,
                cancellationToken);

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Failed to insert admin. UserId: {UserId}, Error: {Error}",
                    message.UserId,
                    result.Error);

                return;
            }

            _logger.LogInformation(
                "Admin inserted successfully. UserId: {UserId}",
                message.UserId);
        }
    }
}
