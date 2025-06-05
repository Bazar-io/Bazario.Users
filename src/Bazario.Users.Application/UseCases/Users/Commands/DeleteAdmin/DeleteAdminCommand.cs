using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Results;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin
{
    public sealed record DeleteAdminCommand(Guid AdminId)
        : ICommand;
}
