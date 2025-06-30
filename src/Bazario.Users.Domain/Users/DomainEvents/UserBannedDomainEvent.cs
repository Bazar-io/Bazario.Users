using Bazario.AspNetCore.Shared.Domain;

namespace Bazario.Users.Domain.Users.DomainEvents
{
    public sealed record UserBannedDomainEvent(
        Guid UserId,
        DateTime? ExpiresAt) : DomainEvent;
}
