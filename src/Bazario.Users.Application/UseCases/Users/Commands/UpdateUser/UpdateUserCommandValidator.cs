using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandValidator
        : AbstractValidator<UpdateUserCommand>
    {
        public const string NamePattern = @"^[\p{L}'\-]+$";

        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 30)
                .Matches(NamePattern);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 30)
                .Matches(NamePattern);

            RuleFor(x => x.Email)
                .NotEmpty()
                .Length(5, 50)
                .EmailAddress();

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Length(10, 15)
                .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$");
        }
    }
}
