using Bazario.AspNetCore.Shared.Results;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanUser
{
    public sealed record BanUserCommand(
        Guid UserId,
        string Reason,
        DateTime? ExpiresAtUtc) : IRequest<Result>;
}
