using Bazario.AspNetCore.Shared.Results;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Commands.InsertUser
{
    public sealed record InsertUserCommand(
        Guid UserId,
        string Email,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string PhoneNumber) : IRequest<Result>;
}
