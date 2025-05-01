using Bazario.Users.Application;
using Bazario.Users.Infrastructure;
using Bazario.Users.Infrastructure.Extensions;
using Bazario.Users.WebAPI.Factories;

namespace Bazario.Users.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<ProblemDetailsFactory>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.ApplyMigrations();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
