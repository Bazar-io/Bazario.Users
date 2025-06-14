using Bazario.AspNetCore.Shared.Abstractions;
using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler
        : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContextService;
        private readonly Mapper<UpdateUserCommand, Result<UpdateUserRequestModel>> _commandMapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUserContextService userContextService,
            Mapper<UpdateUserCommand, Result<UpdateUserRequestModel>> commandMapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userContextService = userContextService;
            _commandMapper = commandMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            var userId = new UserId(_userContextService.GetAuthenticatedUserId());

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                return UserErrors.NotFound;
            }

            var commandResult = _commandMapper.Map(command);

            if (commandResult.IsFailure)
            {
                return commandResult.Error;
            }

            var updateRequest = commandResult.Value;

            var updateResult = user.Update(
                updateRequest.FirstName,
                updateRequest.LastName,
                updateRequest.Email,
                updateRequest.PhoneNumber,
                updateRequest.BirthDate);

            if (updateResult.IsFailure)
            {
                return updateResult.Error;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
