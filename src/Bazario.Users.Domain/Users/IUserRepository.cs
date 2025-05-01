namespace Bazario.Users.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(
            UserId userId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetAllAdminsAsync(
            CancellationToken cancellationToken = default);

        Task InsertAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);
    }
}
