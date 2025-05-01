namespace Bazario.Users.Application.UseCases.Users.DTO
{
    public sealed record UserResponse(
        Guid Id,
        string Role,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string Email,
        string PhoneNumber);
}
