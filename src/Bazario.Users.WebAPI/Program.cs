using Bazario.AspNetCore.Shared.Api.Factories.DependencyInjection;
using Bazario.AspNetCore.Shared.Api.Middleware.DependencyInjection;
using Bazario.AspNetCore.Shared.Authentication.DependencyInjection;
using Bazario.Users.Application;
using Bazario.Users.Infrastructure;
using Bazario.Users.Infrastructure.Extensions;
using Bazario.Users.WebAPI.Extensions;
using Bazario.Users.WebAPI.Filters;

namespace Bazario.Users.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<RequestLoggingFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.ConfigureAuthentication();

            builder.Services.AddProblemDetailsFactory();

            var app = builder.Build();

            app.Services.ValidateAppOptions();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandlingMiddleware();

            app.ApplyMigrations();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
