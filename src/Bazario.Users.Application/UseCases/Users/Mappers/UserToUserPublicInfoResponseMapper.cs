using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Mappers
{
    internal sealed class UserToUserPublicInfoResponseMapper
        : Mapper<User, UserPublicInfoResponse>
    {
        public override UserPublicInfoResponse Map(User source)
        {
            return new UserPublicInfoResponse(
                source.FirstName.Value,
                source.LastName.Value,
                source.PhoneNumber.Value);
        }
    }
}
