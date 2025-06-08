using Bazario.AspNetCore.Shared.Abstractions;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetCurrentAdmin
{
    internal sealed class GetCurrentAdminQueryHandler
        : IQueryHandler<GetCurrentAdminQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly Mapper<User, UserResponse> _mapper;

        public GetCurrentAdminQueryHandler(
            IUserRepository userRepository,
            IUserContextService userContextService,
            Mapper<User, UserResponse> mapper)
        {
            _userRepository = userRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(
            GetCurrentAdminQuery query,
            CancellationToken cancellationToken)
        {
            var userId = new UserId(
                _userContextService.GetAuthenticatedUserId());

            var user = await _userRepository.GetByIdAsync(
                userId,
                cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            if (user.Role != Role.Admin)
            {
                return Result.Failure<UserResponse>(UserErrors.NotAdmin);
            }

            var mappedUser = _mapper.Map(user);

            return Result.Success(mappedUser);
        }
    }
}
