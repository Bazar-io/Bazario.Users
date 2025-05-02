using Bazario.AspNetCore.Shared.Results;

namespace Bazario.Users.Domain.Users.Bans
{
    public static class BanDetailsErrors
    {
        public static readonly Error Empty =
            Error.Validation(
                code: "BanDetails.Reason.Empty",
                description: "Reason cannot be empty or whitespace.");

        public static readonly Error MinOrMaxBlockedAtValue =
            Error.Validation(
                code: "BanDetails.BlockedAt.MinOrMaxValue",
                description: "BlockedAt cannot be DateTime.MinValue or DateTime.MaxValue.");

        public static readonly Error BlockedAtLaterThanExpiresAt =
            Error.Validation(
                code: "BanDetails.BlockedAt.LaterThanExpiresAt",
                description: "ExpiresAt must be later than BlockedAt.");

        public static Error ReasonLengthTooLong(int maxLength) =>
            Error.Validation(
                code: "BanDetails.Reason.TooLong",
                description: $"Reason length cannot be greater than {maxLength} characters.");

        public static Error ReasonLengthTooShort(int minLength) =>
            Error.Validation(
                code: "Email.TooShort",
                description: $"Reason length cannot be less than {minLength} characters.");
    }
}
