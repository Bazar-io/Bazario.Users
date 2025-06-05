using Bazario.AspNetCore.Shared.Abstractions.Messaging;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanAdmin
{
    public sealed record BanAdminCommand(
        Guid AdminId,
        string Reason,
        DateTime? ExpiresAtUtc) : ICommand;
}
