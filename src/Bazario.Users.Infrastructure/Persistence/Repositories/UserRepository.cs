using Bazario.Users.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Bazario.Users.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public Task<int> DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAdminsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
