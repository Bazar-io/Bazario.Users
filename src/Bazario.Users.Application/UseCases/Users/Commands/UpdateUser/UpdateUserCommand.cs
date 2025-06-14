using Bazario.AspNetCore.Shared.Abstractions.Messaging;

namespace Bazario.Users.Application.UseCases.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        DateOnly BirthDate,
        string PhoneNumber) : ICommand;
}
