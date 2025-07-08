using Bazario.AspNetCore.Shared.Abstractions.Mappers;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetPublicInfoById
{
    internal sealed class GetUserPublicInfoByIdQueryHandler
        : IQueryHandler<GetUserPublicInfoByIdQuery, UserPublicInfoResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly Mapper<User, UserPublicInfoResponse> _userMapper;

        public GetUserPublicInfoByIdQueryHandler(
            IUserRepository userRepository,
            Mapper<User, UserPublicInfoResponse> userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public async Task<Result<UserPublicInfoResponse>> Handle(
            GetUserPublicInfoByIdQuery query,
            CancellationToken cancellationToken)
        {
            var userId = new UserId(query.UserId);

            var user = await _userRepository.GetByIdAsync(
                userId,
                cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserPublicInfoResponse>(UserErrors.NotFound);
            }

            if (user.Role != Role.User)
            {
                return Result.Failure<UserPublicInfoResponse>(UserErrors.NotFound);
            }

            var userPublicInfo = _userMapper.Map(user);

            return userPublicInfo;
        }
    }
}
