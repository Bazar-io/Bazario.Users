using Bazario.AspNetCore.Shared.Infrastructure.Mappers;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Mappers
{
    internal sealed class UserToUserResponseMapper : Mapper<User, UserResponse>
    {
        public override UserResponse Map(User source)
        {
            return new UserResponse(
                Id: source.Id.Value,
                Role: source.Role.ToString(),
                FirstName: source.FirstName.Value,
                LastName: source.LastName.Value,
                BirthDate: source.BirthDate.Value,
                Email: source.Email.Value,
                PhoneNumber: source.PhoneNumber.Value);
        }
    }
}
