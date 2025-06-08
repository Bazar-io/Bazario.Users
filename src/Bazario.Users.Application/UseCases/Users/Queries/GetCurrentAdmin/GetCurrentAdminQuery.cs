using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetCurrentAdmin
{
    public sealed record GetCurrentAdminQuery()
        : IQuery<UserResponse>;
}
