using Bazario.AspNetCore.Shared.Abstractions.Mappers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.Commands.InsertUser;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Mappers
{
    internal sealed class InsertUserCommandMapper
        : Mapper<InsertUserCommand, Result<User>>
    {
        public override Result<User> Map(InsertUserCommand source)
        {
            var userId = new UserId(source.UserId);

            var emailResult = Email.Create(source.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<User>(emailResult.Error);
            }

            var email = emailResult.Value;

            var firstNameResult = FirstName.Create(source.FirstName);

            if (firstNameResult.IsFailure)
            {
                return Result.Failure<User>(firstNameResult.Error);
            }

            var firstName = firstNameResult.Value;

            var lastNameResult = LastName.Create(source.LastName);

            if (lastNameResult.IsFailure)
            {
                return Result.Failure<User>(lastNameResult.Error);
            }

            var lastName = lastNameResult.Value;

            var phoneNumberResult = PhoneNumber.Create(source.PhoneNumber);

            if (phoneNumberResult.IsFailure)
            {
                return Result.Failure<User>(phoneNumberResult.Error);
            }

            var phoneNumber = phoneNumberResult.Value;

            var birthDateResult = BirthDate.Create(source.BirthDate);

            if (birthDateResult.IsFailure)
            {
                return Result.Failure<User>(birthDateResult.Error);
            }

            var birthDate = birthDateResult.Value;

            return User.Create(
                userId,
                source.Role,
                firstName,
                lastName,
                email,
                phoneNumber,
                birthDate);
        }
    }
}
