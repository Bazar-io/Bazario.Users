using Bazario.AspNetCore.Shared.Contracts.UserRegistered;
using Bazario.Users.Application.UseCases.Users.Commands.InsertUser;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Infrastructure.Consumers
{
    public sealed class UserRegisteredConsumer : IConsumer<UserRegisteredForUserServiceEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<UserRegisteredConsumer> _logger;

        public UserRegisteredConsumer(
            ISender sender,
            ILogger<UserRegisteredConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(
            ConsumeContext<UserRegisteredForUserServiceEvent> context)
        {
            _logger.LogInformation(
                "User registered event received. UserId: {UserId}",
                context.Message.UserId);

            var message = context.Message;

            var command = new InsertUserCommand(
                UserId: message.UserId,
                Email: message.Email,
                FirstName: message.FirstName,
                LastName: message.LastName,
                BirthDate: message.BirthDate,
                PhoneNumber: message.PhoneNumber);

            var result = await _sender.Send(command);

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
