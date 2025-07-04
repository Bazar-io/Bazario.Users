using FluentValidation;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetPublicInfoById
{
    internal sealed class GetUserPublicInfoByIdQueryValidator
        : AbstractValidator<GetUserPublicInfoByIdQuery>
    {
        public GetUserPublicInfoByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
