using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Commands.DeleteUser
{
    internal sealed class DeleteUserCommandHandler
        : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            DeleteUserCommand command,
            CancellationToken cancellationToken)
        {
            var userId = new UserId(command.UserId);
            var user = await _userRepository.GetByIdAsync(
                userId, cancellationToken);

            if (user is null)
            {
                return UserErrors.NotFound;
            }

            await _userRepository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
