using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Results;

namespace Bazario.Users.Domain.Users.Bans
{
    public sealed class BanDetails : ValueObject
    {
        public const int MinReasonLength = 10;
        public const int MaxReasonLength = 255;

        private BanDetails(
            string reason,
            DateTime blockedAt,
            DateTime? expiresAt)
        {
            Reason = reason;
            BlockedAt = blockedAt;
            ExpiresAt = expiresAt;
        }

        public string Reason { get; }

        public DateTime BlockedAt { get; }

        public DateTime? ExpiresAt { get; }

        public static Result<BanDetails> Create(
            string reason,
            DateTime blockedAt,
            DateTime? expiresAt)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                return Result.Failure<BanDetails>(BanDetailsErrors.Empty);
            }

            if (reason.Length > MaxReasonLength)
            {
                return Result.Failure<BanDetails>(
                    BanDetailsErrors.ReasonLengthTooLong(MaxReasonLength));
            }

            if (reason.Length < MinReasonLength)
            {
                return Result.Failure<BanDetails>(
                    BanDetailsErrors.ReasonLengthTooShort(MinReasonLength));
            }

            if (blockedAt == DateTime.MinValue || blockedAt == DateTime.MaxValue)
            {
                return Result.Failure<BanDetails>(
                    BanDetailsErrors.MinOrMaxBlockedAtValue);
            }

            if (expiresAt.HasValue && expiresAt.Value <= blockedAt)
            {
                return Result.Failure<BanDetails>(
                    BanDetailsErrors.BlockedAtLaterThanExpiresAt);
            }

            return new BanDetails(reason, blockedAt, expiresAt);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Reason;
            yield return BlockedAt;
            yield return ExpiresAt!;
        }
    }
}
