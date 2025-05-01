using Bazario.Users.Domain.Users;
using Bazario.Users.Domain.Users.BirthDates;
using Bazario.Users.Domain.Users.Emails;
using Bazario.Users.Domain.Users.FirstNames;
using Bazario.Users.Domain.Users.LastNames;
using Bazario.Users.Domain.Users.PhoneNumbers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazario.Users.Infrastructure.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Id)
                .IsUnique();

            builder.HasIndex(user => user.Role);

            builder.Property(user => user.Id).HasConversion(
                 id => id.Value,
                 value => new UserId(value));

            builder.Property(user => user.FirstName)
                .HasMaxLength(FirstName.MaxLength)
                .HasConversion(
                    firstName => firstName.Value,
                    value => FirstName.Create(value).Value);

            builder.Property(user => user.LastName)
                .HasMaxLength(LastName.MaxLength)
                .HasConversion(
                    lastName => lastName.Value,
                    value => LastName.Create(value).Value);

            builder.Property(user => user.BirthDate)
                .HasConversion(
                    birthDate => birthDate.Value,
                    value => BirthDate.Create(value).Value);

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value);

            var phoneNumberMaxLength = 50;

            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(phoneNumberMaxLength)
                .HasConversion(
                    phoneNumber => phoneNumber.Value,
                    value => PhoneNumber.Create(value).Value);
        }
    }
}
