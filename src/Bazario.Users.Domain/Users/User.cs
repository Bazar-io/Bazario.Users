using Bazario.AspNetCore.Shared.Auth.Roles;
using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users.Bans;
using Bazario.Users.Domain.Users.BirthDates;
using Bazario.Users.Domain.Users.Emails;
using Bazario.Users.Domain.Users.FirstNames;
using Bazario.Users.Domain.Users.LastNames;
using Bazario.Users.Domain.Users.PhoneNumbers;

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

        public LastName LastName { get; private set; }  

        public Email Email { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public bool IsBanned => BanDetails is not null;

        public BanDetails? BanDetails { get; private set; }

        public void Ban(BanDetails banDetails)
        {
            BanDetails = banDetails;
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
