using Bazario.AspNetCore.Shared.Abstractions.Messaging;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanUser
{
    public sealed record BanUserCommand(
        Guid UserId,
        string Reason,
        DateTime? ExpiresAtUtc) : ICommand;
}
