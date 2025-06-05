using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins
{
    public sealed record GetAllAdminsQuery
        : IQuery<IEnumerable<UserResponse>>;
}
