using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAdminById
{
    internal sealed class GetAdminByIdQueryValidator
        : AbstractValidator<GetAdminByIdQuery>
    {
        public GetAdminByIdQueryValidator()
        {
            RuleFor(x => x.AdminId)
                .NotEmpty();
        }
    }
}
