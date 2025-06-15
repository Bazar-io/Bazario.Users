using Bazario.AspNetCore.Shared.Domain;

namespace Bazario.Users.Domain.Users.DomainEvents
{
    public sealed record UserUpdatedDomainEvent(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        DateOnly BirthDate) : DomainEvent;
}
