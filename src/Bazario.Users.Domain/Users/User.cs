using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users.Bans;

namespace Bazario.Users.Domain.Users
{
    public sealed class User : AggregateRoot<UserId>
    {
        private User()
            : base(new(Guid.Empty))
        { }

        private User(
            UserId userId,
            Role role,
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            BirthDate birthDate) : base(userId)
        {
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public Role Role { get; }

        public FirstName FirstName { get; private set; }

        public DateTime? FirstNameUpdatedAt { get; private set; }

        public LastName LastName { get; private set; }

        public DateTime? LastNameUpdatedAt { get; private set; }

        public Email Email { get; private set; }

        public DateTime? EmailUpdatedAt { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public DateTime? PhoneNumberUpdatedAt { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public DateTime? BirthDateUpdatedAt { get; private set; }

        public bool IsBanned => BanDetails is not null;

        public BanDetails? BanDetails { get; private set; }

        public Result ApplyBan(BanDetails banDetails)
        {
            if (IsBanned)
            {
                return Result.Failure(UserErrors.AlreadyBanned);
            }

            BanDetails = banDetails;

            return Result.Success();
        }

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

            return Result.Success();
        }

        public static Result<User> Create(
            UserId userId,
            Role role,
            FirstName firstName,
            LastName lastName,
            Email email,
            PhoneNumber phoneNumber,
            BirthDate birthDate)
        {
            return new User(
                userId,
                role,
                firstName,
                lastName,
                email,
                phoneNumber,
                birthDate);
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
