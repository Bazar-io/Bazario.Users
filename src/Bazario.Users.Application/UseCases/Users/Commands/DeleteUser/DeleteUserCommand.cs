using Bazario.AspNetCore.Shared.Abstractions.Messaging;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(
        Guid UserId) : ICommand;
}
