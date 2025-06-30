using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users.DomainEvents;

namespace Bazario.Users.Domain.Users
{
    public sealed partial class User
    {
        public Result Update(
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            BirthDate birthDate)
        {
            if (IsBanned)
            {
                return UserErrors.Banned;
            }

            if (Role != Role.User)
            {
                return UserErrors.NotUser;
            }

            var firstNameIsSame = FirstName == firstName;
            var lastNameIsSame = LastName == lastName;
            var emailIsSame = Email == email;
            var phoneNumberIsSame = PhoneNumber == phoneNumber;
            var birthDateIsSame = BirthDate == birthDate;

            if (firstNameIsSame && lastNameIsSame && emailIsSame && phoneNumberIsSame && birthDateIsSame)
            {
                return UserErrors.UserPropertiesIdentical;
            }

            var validationResult = ValidateUpdateTimeLimits(
                firstNameIsSame,
                lastNameIsSame,
                emailIsSame,
                phoneNumberIsSame,
                birthDateIsSame);

            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            UpdateFirstName(firstName, firstNameIsSame);
            UpdateLastName(lastName, lastNameIsSame);
            UpdateEmail(email, emailIsSame);
            UpdatePhoneNumber(phoneNumber, phoneNumberIsSame);
            UpdateBirthDate(birthDate, birthDateIsSame);

            RaiseDomainEvent(new UserUpdatedDomainEvent(
                Id.Value,
                firstName.Value,
                lastName.Value,
                email.Value,
                phoneNumber.Value,
                birthDate.Value));

            return Result.Success();
        }

        private void UpdateFirstName(FirstName value, bool isSame)
        {
            if (isSame)
                return;

            FirstName = value;
            FirstNameUpdatedAt = DateTime.UtcNow;
        }

        private void UpdateLastName(LastName value, bool isSame)
        {
            if (isSame)
                return;

            LastName = value;
            LastNameUpdatedAt = DateTime.UtcNow;
        }

        private void UpdateEmail(Email value, bool isSame)
        {
            if (isSame)
                return;

            Email = value;
            EmailUpdatedAt = DateTime.UtcNow;
        }

        private void UpdatePhoneNumber(PhoneNumber value, bool isSame)
        {
            if (isSame)
                return;

            PhoneNumber = value;
            PhoneNumberUpdatedAt = DateTime.UtcNow;
        }

        private void UpdateBirthDate(BirthDate value, bool isSame)
        {
            if (isSame)
                return;

            BirthDate = value;
            BirthDateUpdatedAt = DateTime.UtcNow;
        }

        private Result ValidateUpdateTimeLimits(
            bool firstNameIsSame,
            bool lastNameIsSame,
            bool emailIsSame,
            bool phoneNumberIsSame,
            bool birthDateIsSame)
        {
            var results = new List<Result>
            {
                ValidateUpdateTimeLimit(firstNameIsSame, FirstNameUpdatedAt, nameof(FirstName)),
                ValidateUpdateTimeLimit(lastNameIsSame, LastNameUpdatedAt, nameof(LastName)),
                ValidateUpdateTimeLimit(emailIsSame, EmailUpdatedAt, nameof(Email)),
                ValidateUpdateTimeLimit(phoneNumberIsSame, PhoneNumberUpdatedAt, nameof(PhoneNumber)),
                ValidateUpdateTimeLimit(birthDateIsSame, BirthDateUpdatedAt, nameof(BirthDate))
            };

            return results.FirstOrDefault(r => r.IsFailure) ?? Result.Success();
        }

        private static Result ValidateUpdateTimeLimit(
            bool propertyIsSameAsValue,
            DateTime? updatedAt,
            string propertyName)
        {
            if (!propertyIsSameAsValue &&
                updatedAt is not null &&
                updatedAt.Value.AddYears(1) > DateTime.UtcNow)
            {
                return UserErrors.UpdateTimeLimit(propertyName);
            }

            return Result.Success();
        }
    }
}
