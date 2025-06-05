using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Application.Mappers;
using Bazario.AspNetCore.Shared.Results;
using Bazario.Users.Domain.Users;

namespace Bazario.Users.Application.UseCases.Users.Commands.InsertUser
{
    internal sealed class InsertUserCommandHandler
        : ICommandHandler<InsertUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly Mapper<InsertUserCommand, Result<User>> _commandMapper;
        private readonly IUnitOfWork _unitOfWork;

        public InsertUserCommandHandler(
            IUserRepository userRepository,
            Mapper<InsertUserCommand, Result<User>> commandMapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _commandMapper = commandMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            InsertUserCommand request,
            CancellationToken cancellationToken)
        {
            // Map the command to a User entity

            var mappingResult = _commandMapper.Map(request);

            if (mappingResult.IsFailure)
            {
                return Result.Failure(mappingResult.Error);
            }

            var user = mappingResult.Value;

            // Add the user to the repository

            await _userRepository.InsertAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return success

            return Result.Success();
        }
    }
}
