using Bazario.Users.Application;
using Bazario.Users.Infrastructure;
using Bazario.Users.Infrastructure.Extensions;

namespace Bazario.Users.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.ApplyMigrations();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
