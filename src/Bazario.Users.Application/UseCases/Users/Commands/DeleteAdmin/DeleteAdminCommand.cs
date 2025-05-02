using Bazario.AspNetCore.Shared.Results;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin
{
    public sealed record DeleteAdminCommand(Guid AdminId)
        : IRequest<Result>;
}
