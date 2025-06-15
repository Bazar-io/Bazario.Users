using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox;
using Bazario.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bazario.Users.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext, IHasOutboxMessages
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<OutboxMessage> OutboxMessages { get; init; }

        public DbSet<User> Users { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<DomainEvent>();

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}
