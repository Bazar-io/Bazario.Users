using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.Exceptions;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteAdmin
{
    internal sealed class DeleteAdminCommandHandler
        : ICommandHandler<DeleteAdminCommand>
    {
        private readonly ILogger<DeleteAdminCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAdminCommandHandler(
            ILogger<DeleteAdminCommandHandler> logger,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteAdminCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogTrace("Starting handling DeleteAdminCommand.");

            var foundUser = await _userRepository.GetByIdAsync(
                userId: new UserId(request.AdminId),
                cancellationToken: cancellationToken);

            var eligible = IsUserEligibleToBeDeleted(
                request.AdminId, foundUser);

            if (eligible.IsFailure)
            {
                return eligible.Error;
            }

            await _userRepository.DeleteAsync(foundUser!);

            var affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);

            ValidateAffectedRows(affectedRows);

            _logger.LogDebug("Successfully deleted admin with ID {AdminId}.", request.AdminId);
            _logger.LogTrace("Completed handling DeleteAdminCommand.");

            return Result.Success();
        }

        private void ValidateAffectedRows(int affectedRows)
        {
            if (affectedRows == 0)
            {
                throw new InternalSystemException("Failed to delete admin even though the user is found in the storage. Affected rows equals 0.");
            }

            if (affectedRows > 1)
            {
                _logger.LogWarning(
                    "Deleted a few rows from the storage while trying to delete a single one. Affected rows: {AffectedRows}", affectedRows);
            }
        }

        private Result IsUserEligibleToBeDeleted(
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
