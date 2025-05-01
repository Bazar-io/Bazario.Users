namespace Bazario.Users.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(
            UserId id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetAllAdminsAsync(
            CancellationToken cancellationToken = default);

        Task<int> InsertAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task<int> UpdateAsync(
            User user,
            CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(
            User user,
            CancellationToken cancellationToken = default);
    }
}
