using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.Exceptions;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;
using Bazario.Users.Domain.Users.Bans;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Commands.BanAdmin
{
    internal sealed class BanAdminCommandHandler
        : ICommandHandler<BanAdminCommand>
    {
        private readonly ILogger<BanAdminCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BanAdminCommandHandler(
            ILogger<BanAdminCommandHandler> logger,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            BanAdminCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("Starting handling BanAdminCommand.");

            var foundUser = await _userRepository.GetByIdAsync(
                userId: new UserId(request.AdminId),
                cancellationToken: cancellationToken);

            var eligible = IsUserEligibleToBeBanned(
                adminId: request.AdminId,
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

            _logger.LogDebug("Successfully banned admin with ID {AdminId}.", request.AdminId);
            _logger.LogTrace("Completed handling BanAdminCommand.");

            return Result.Success();
        }

        private Result<BanDetails> CreateBanDetails(BanAdminCommand request)
        {
            var banResult = BanDetails.Create(
                reason: request.Reason,
                blockedAt: DateTime.UtcNow,
                expiresAt: request.ExpiresAtUtc);

            if (banResult.IsFailure)
            {
                _logger.LogWarning("BanDetails creation failed for AdminId {AdminId}.", request.AdminId);
            }

            return banResult;
        }

        private void ValidateAffectedRows(int affectedRows)
        {
            if (affectedRows == 0)
            {
                throw new InternalSystemException("Failed to ban admin. Affected rows equals 0.");
            }

            if (affectedRows > 1)
            {
                _logger.LogWarning(
                    "Affected a few rows in the storage while trying to affect a single one. Affected rows: {AffectedRows}", affectedRows);
            }
        }

        private Result IsUserEligibleToBeBanned(
            Guid adminId,
            User? foundUser)
        {
            if (foundUser is null)
            {
                _logger.LogDebug("User with ID {AdminId} not found.", adminId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            if (foundUser.Role != Role.Admin)
            {
                _logger.LogDebug("User with ID {AdminId} is not an Admin.", adminId);

                return Result.Failure<UserResponse>(UserErrors.NotFound);
            }

            return Result.Success();
        }
    }
}
