using Bazario.AspNetCore.Shared.Auth.Roles;
using Bazario.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bazario.Users.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(
            ILogger<UserRepository> logger, 
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("Fetching user by ID: {UserId}", userId);

            var user = await _dbContext.Users.FindAsync(
                keyValues: [userId],
                cancellationToken: cancellationToken);

            if (user is null)
            {
                _logger.LogDebug("User with ID {UserId} was not found", userId);
            }
            else
            {
                _logger.LogDebug("User with ID {UserId} was retrieved", userId);
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAdminsAsync(
            CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("Fetching all users with role: Admin");

            var stopwatch = Stopwatch.StartNew();

            var admins = await _dbContext.Users
                .Where(user => user.Role == Role.Admin)
                .ToListAsync(cancellationToken);

            stopwatch.Stop();

            _logger.LogDebug(
                "Retrieved {Count} admin users in {ElapsedMilliseconds} ms",
                admins.Count, stopwatch.ElapsedMilliseconds);

            return admins;
        }

        public async Task InsertAsync(
            User user, 
            CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("Inserting new user with ID: {UserId}", user.Id);

            await _dbContext.Users.AddAsync(user, cancellationToken);

            _logger.LogDebug("User with ID {UserId} was added to the context", user.Id);
        }

        public Task UpdateAsync(User user)
        {
            _logger.LogTrace("Marking user to update with ID: {UserId}", user.Id);

            _dbContext.Users.Update(user);

            _logger.LogDebug("User with ID {UserId} was marked as modified", user.Id);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user)
        {
            _logger.LogTrace("Marking user to delete with ID: {UserId}", user.Id);

            _dbContext.Users.Remove(user);

            _logger.LogDebug("User with ID {UserId} was marked for deletion", user.Id);

            return Task.CompletedTask;
        }
    }
}
