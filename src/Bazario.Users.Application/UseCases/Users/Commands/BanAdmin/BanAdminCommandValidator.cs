using Bazario.Users.Domain.Users.Bans;
using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanAdmin
{
    internal sealed class BanAdminCommandValidator
        : AbstractValidator<BanAdminCommand>
    {
        public BanAdminCommandValidator()
        {
            RuleFor(x => x.AdminId)
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
