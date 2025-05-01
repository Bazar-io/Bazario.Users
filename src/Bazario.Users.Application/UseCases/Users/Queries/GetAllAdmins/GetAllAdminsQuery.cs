using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins
{
    public sealed record GetAllAdminsQuery
        : IRequest<Result<IEnumerable<UserResponse>>>;
}
