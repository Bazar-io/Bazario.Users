using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.Exceptions;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;
using Bazario.Users.Domain.Users.Bans;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanUser
{
    internal sealed class BanUserCommandHandler
        : IRequestHandler<BanUserCommand, Result>
    {
        private readonly ILogger<BanUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BanUserCommandHandler(
            ILogger<BanUserCommandHandler> logger,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            BanUserCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("Starting handling BanUserCommand.");

            var foundUser = await _userRepository.GetByIdAsync(
                userId: new UserId(request.UserId),
                cancellationToken: cancellationToken);

            var eligible = IsUserEligibleToBeBanned(
                userId: request.UserId,
                foundUser: foundUser);

            if (eligible.IsFailure)
            {
                return eligible.Error;
            }

            var banDetailsResult = CreateBanDetails(request);

            if (banDetailsResult.IsFailure)
            {
                return banDetailsResult.Error;
            }

            var banDetails = banDetailsResult.Value;

            var banResult = foundUser!.ApplyBan(banDetails);

            if (banResult.IsFailure)
            {
                return banResult.Error;
            }

            var affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);

            ValidateAffectedRows(affectedRows);

            _logger.LogDebug("Successfully banned user with ID {UserId}.", request.UserId);
            _logger.LogTrace("Completed handling BanUserCommand.");

            return Result.Success();
        }

        private Result<BanDetails> CreateBanDetails(BanUserCommand request)
        {
            var banResult = BanDetails.Create(
                reason: request.Reason,
                blockedAt: DateTime.UtcNow,
                expiresAt: request.ExpiresAtUtc);

            if (banResult.IsFailure)
            {
                _logger.LogWarning("BanDetails creation failed for UserId {UserId}.", request.UserId);
            }

            return banResult;
        }

        private void ValidateAffectedRows(int affectedRows)
        {
            if (affectedRows == 0)
            {
                throw new InternalSystemException("Failed to ban user. Affected rows equals 0.");
            }

            if (affectedRows > 1)
            {
                _logger.LogWarning(
                    "Affected a few rows in the storage while trying to affect a single one. Affected rows: {AffectedRows}", affectedRows);
            }
        }

        private Result IsUserEligibleToBeBanned(
            Guid userId,
            User? foundUser)
        {
            if (foundUser is null)
            {
                _logger.LogDebug("User with ID {UserId} not found.", userId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            if (foundUser.Role != Role.User)
            {
                _logger.LogDebug("User with ID {UserId} is not a regular User.", userId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            return Result.Success();
        }
    }
}
