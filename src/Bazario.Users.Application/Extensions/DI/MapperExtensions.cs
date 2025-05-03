using Bazario.AspNetCore.Shared.Application.Mappers.DependencyInjection;
using Bazario.Users.Application.UseCases.Users.DTO;
using Bazario.Users.Application.UseCases.Users.Mappers;
using Bazario.Users.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Application.Extensions.DI
{
    internal static class MapperExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddMapper<User, UserResponse, UserToUserResponseMapper>();

            return services;
        }
    }
}
