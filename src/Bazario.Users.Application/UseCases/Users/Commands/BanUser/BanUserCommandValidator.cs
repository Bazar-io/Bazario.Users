using Bazario.Users.Domain.Users.Bans;
using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanUser
{
    internal sealed class BanUserCommandValidator
        : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(BanDetails.MaxReasonLength)
                .MinimumLength(BanDetails.MinReasonLength);

            RuleFor(x => x.ExpiresAtUtc)
                .GreaterThan(DateTime.UtcNow).WithMessage("ExpiresAtUtc must be later than DateTime.UtcNow.")
                .LessThan(DateTime.MaxValue).WithMessage("ExpiresAtUtc cannot be DateTime.MaxValue.")
                .When(x => x.ExpiresAtUtc is not null);
        }
    }
}
