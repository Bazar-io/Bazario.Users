using Bazario.AspNetCore.Shared.Results;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanAdmin
{
    public sealed record BanAdminCommand(
        Guid AdminId,
        string Reason,
        DateTime? ExpiresAtUtc) : IRequest<Result>;
}
