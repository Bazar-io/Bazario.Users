using Bazario.Users.WebAPI.Middleware;

namespace Bazario.Users.WebAPI.Extensions.DI
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();

            return builder;
        }
    }
}
