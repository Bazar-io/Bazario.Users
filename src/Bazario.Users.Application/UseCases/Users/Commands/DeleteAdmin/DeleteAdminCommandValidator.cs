using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin
{
    internal sealed class DeleteAdminCommandValidator 
        : AbstractValidator<DeleteAdminCommand>
    {
        public DeleteAdminCommandValidator()
        {
            RuleFor(x => x.AdminId)
                .NotEmpty();
        }
    }
}
