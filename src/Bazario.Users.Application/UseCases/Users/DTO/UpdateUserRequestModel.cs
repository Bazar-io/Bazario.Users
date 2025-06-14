using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;

namespace Bazario.Users.Application.UseCases.Users.DTO
{
    public sealed record UpdateUserRequestModel(
        FirstName FirstName,
        LastName LastName,
        Email Email,
        PhoneNumber PhoneNumber,
        BirthDate BirthDate);
}
