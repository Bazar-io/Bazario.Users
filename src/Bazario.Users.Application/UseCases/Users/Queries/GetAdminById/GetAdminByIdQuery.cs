using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAdminById
{
    public sealed record GetAdminByIdQuery(Guid AdminId) 
        : IRequest<Result<UserResponse>>;
}
