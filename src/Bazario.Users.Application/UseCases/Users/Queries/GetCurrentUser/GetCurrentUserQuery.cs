using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetCurrentUser
{
    public sealed record GetCurrentUserQuery()
        : IQuery<UserResponse>;
}
