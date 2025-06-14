using Bazario.AspNetCore.Shared.Results;

namespace Bazario.Users.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error NotFound =
            Error.NotFound(
                code: "User.NotFound",
                description: "User has not been found.");

        public static Error UpdateTimeLimit(string property) =>
            Error.Validation(
                code: $"User.UpdateTimeLimit",
                description: $"You can only update your {property} once per year.");

        public static readonly Error Banned =
            Error.Validation(
                code: "User.Banned",
                description: "User is banned.");

        public static readonly Error AlreadyBanned =
            Error.Validation(
                code: "User.AlreadyBanned",
                description: "User is already banned.");

        public static readonly Error NotAdmin =
            Error.Validation(
                code: "User.NotAdmin",
                description: "User is not an admin.");

        public static readonly Error NotUser =
            Error.Validation(
                code: "User.NotUser",
                description: "User is not a regular user.");
    }
}
