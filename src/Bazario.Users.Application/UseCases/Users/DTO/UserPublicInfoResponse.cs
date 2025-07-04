namespace Bazario.Users.Application.UseCases.Users.DTO
{
    public sealed record UserPublicInfoResponse(
        string FirstName,
        string LastName,
        string PhoneNumber);
}
