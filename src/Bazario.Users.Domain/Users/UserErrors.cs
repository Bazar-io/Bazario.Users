using Bazario.AspNetCore.Shared.Results;

namespace Bazario.Users.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error NotFound =
            Error.NotFound(
                code: "User.NotFound",
                description: "User has not been found.");
    }
}
