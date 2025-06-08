using Bazario.AspNetCore.Shared.Abstractions;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetCurrentUser
{
    internal sealed class GetCurrentUserQueryHandler
        : IQueryHandler<GetCurrentUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly Mapper<User, UserResponse> _mapper;

        public GetCurrentUserQueryHandler(
            IUserRepository userRepository,
            IUserContextService userContextService,
            Mapper<User, UserResponse> mapper)
        {
            _userRepository = userRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(
            GetCurrentUserQuery query,
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

            if (user.Role != Role.User)
            {
                return Result.Failure<UserResponse>(UserErrors.NotUser);
            }

            var mappedUser = _mapper.Map(user);

            return Result.Success(mappedUser);
        }
    }
}
