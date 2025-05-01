using Bazario.AspNetCore.Shared.Domain;

namespace Bazario.Users.Domain.Users
{
    public sealed class User : AggregateRoot<UserId>
    {
        private User()
            : base(new(Guid.Empty))
        { }
    }
}
