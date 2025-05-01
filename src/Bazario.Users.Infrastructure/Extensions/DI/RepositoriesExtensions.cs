using Bazario.Users.Domain.Users;
using Bazario.Users.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    internal static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
