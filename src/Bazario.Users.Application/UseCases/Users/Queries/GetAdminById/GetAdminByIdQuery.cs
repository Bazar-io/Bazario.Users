using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAdminById
{
    public sealed record GetAdminByIdQuery(Guid AdminId)
        : IQuery<UserResponse>;
}
