using Bazario.AspNetCore.Shared.Domain;
using Bazario.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bazario.Users.Infrastructure.Persistence
{
    internal sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

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
