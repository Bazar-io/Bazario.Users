using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetPublicInfoById
{
    public sealed record GetUserPublicInfoByIdQuery(
        Guid UserId): IQuery<UserPublicInfoResponse>;
}
