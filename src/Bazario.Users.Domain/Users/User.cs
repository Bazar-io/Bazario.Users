using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Emails;
using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames;
using Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users.Bans;
using Bazario.Users.Domain.Users.DomainEvents;

namespace Bazario.Users.Domain.Users
{
    public sealed partial class User : AggregateRoot<UserId>
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

            RaiseDomainEvent(new UserBannedDomainEvent(
                Id.Value,
                BanDetails.ExpiresAt));

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
    }
}
