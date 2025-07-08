using Bazario.AspNetCore.Shared.Abstractions.Mappers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.Commands.UpdateUser;
using Bazario.Users.Application.UseCases.Users.DTO;

namespace Bazario.Users.Application.UseCases.Users.Mappers
{
    internal sealed class UpdateUserCommandMapper
        : Mapper<UpdateUserCommand, Result<UpdateUserRequestModel>>
    {
        public override Result<UpdateUserRequestModel> Map(UpdateUserCommand source)
        {
            // Create first name

            var firstNameResult = FirstName.Create(source.FirstName);

            if (firstNameResult.IsFailure)
            {
                return Result.Failure<UpdateUserRequestModel>(firstNameResult.Error);
            }

            var firstName = firstNameResult.Value;

            // Create last name

            var lastNameResult = LastName.Create(source.LastName);

            if (lastNameResult.IsFailure)
            {
                return Result.Failure<UpdateUserRequestModel>(lastNameResult.Error);
            }

            var lastName = lastNameResult.Value;

            // Create email

            var emailResult = Email.Create(source.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<UpdateUserRequestModel>(emailResult.Error);
            }

            var email = emailResult.Value;

            // Create phone number

            var phoneNumberResult = PhoneNumber.Create(source.PhoneNumber);

            if (phoneNumberResult.IsFailure)
            {
                return Result.Failure<UpdateUserRequestModel>(phoneNumberResult.Error);
            }

            var phoneNumber = phoneNumberResult.Value;

            // Create birth date

            var birthDateResult = BirthDate.Create(source.BirthDate);

            if (birthDateResult.IsFailure)
            {
                return Result.Failure<UpdateUserRequestModel>(birthDateResult.Error);
            }

            var birthDate = birthDateResult.Value;

            // Return the mapped result

            return new UpdateUserRequestModel(
                firstName,
                lastName,
                email,
                phoneNumber,
                birthDate);
        }
    }
}
