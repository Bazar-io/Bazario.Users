using SharedJobs = Bazario.AspNetCore.Shared.Infrastructure.BackgroundJobs.DependencyInjection.BackgroundJobsExtensions;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Bazario.Users.Infrastructure.Persistence;
using Bazario.AspNetCore.Shared.Infrastructure.BackgroundJobs.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    public static class BackgroundJobsExtensions
    {
        public static IServiceCollection ConfigureAppBackgroundJobs(this IServiceCollection services)
        {
            List<Action<IServiceCollection, IServiceCollectionQuartzConfigurator>> jobConfigurations = [
                SharedJobs.ConfigureProcessOutboxMessagesJob<ApplicationDbContext>];

            services.AddBackgroundJobs([.. jobConfigurations]);

            return services;
        }
    }
}
