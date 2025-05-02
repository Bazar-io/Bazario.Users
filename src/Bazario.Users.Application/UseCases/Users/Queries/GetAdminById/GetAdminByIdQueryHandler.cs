using Bazario.AspNetCore.Shared.Auth.Roles;
using Bazario.AspNetCore.Shared.Infrastructure.Mappers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAdminById
{
    internal sealed class GetAdminByIdQueryHandler
        : IRequestHandler<GetAdminByIdQuery, Result<UserResponse>>
    {
        private readonly ILogger<GetAdminByIdQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly Mapper<User, UserResponse> _mapper;

        public GetAdminByIdQueryHandler(
            ILogger<GetAdminByIdQueryHandler> logger,
            IUserRepository userRepository,
            Mapper<User, UserResponse> mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(
            GetAdminByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("Starting handling GetAdminByIdQuery.");

            var foundUser = await _userRepository.GetByIdAsync(
                userId: new UserId(request.AdminId),
                cancellationToken);

            if (foundUser is null)
            {
                _logger.LogDebug("User with ID {AdminId} not found.", request.AdminId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            if (foundUser.Role != Role.Admin)
            {
                _logger.LogDebug("User with ID {AdminId} is not an Admin.", request.AdminId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            var mappedUser = _mapper.Map(foundUser);

            _logger.LogTrace("Completed handling GetAdminByIdQuery.");

            return Result.Success(mappedUser);
        }
    }
}
