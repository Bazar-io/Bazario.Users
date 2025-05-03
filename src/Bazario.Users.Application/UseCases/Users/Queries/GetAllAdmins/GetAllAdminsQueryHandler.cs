using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Queries.GetAllAdmins
{
    internal sealed class GetAllAdminsQueryHandler
        : IRequestHandler<GetAllAdminsQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly ILogger<GetAllAdminsQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly Mapper<User, UserResponse> _mapper;

        public GetAllAdminsQueryHandler(
            ILogger<GetAllAdminsQueryHandler> logger,
            IUserRepository userRepository,
            Mapper<User, UserResponse> mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(
            GetAllAdminsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("Starting handling GetAllAdminsQuery.");

            var admins = await _userRepository
                .GetAllAdminsAsync(cancellationToken);

            if (!admins.Any())
            {
                _logger.LogWarning("No admins retrieved from the repository.");
            }

            _logger.LogTrace("Mapping retrieved admins to result list.");

            var mappedResult = _mapper.Map(admins);

            _logger.LogTrace("Completed handling GetAllAdminsQuery.");

            return Result.Success(mappedResult);
        }
    }
}
