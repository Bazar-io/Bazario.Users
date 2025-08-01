﻿using Bazario.AspNetCore.Shared.Abstractions.Data;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.DependencyInjection;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Interceptors;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options;
using Bazario.AspNetCore.Shared.Options;
using Bazario.Users.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.Users.Infrastructure.Extensions.DI
{
    internal static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            services.RegisterInterceptors();

            services.AddAppDbContext();

            services.AddUnitOfWork();

            return services;
        }

        private static IServiceCollection RegisterInterceptors(
            this IServiceCollection services)
        {
            return services.RegisterInterceptor<ConvertDomainEventsToOutboxMessagesInterceptor>();
        }

        private static void AddAppDbContext(
            this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, options) =>
                {
                    options
                        .UseNpgsqlWithDbSettings(serviceProvider)
                        .AddAppInterceptors(serviceProvider);
                });
        }

        private static DbContextOptionsBuilder UseNpgsqlWithDbSettings(
            this DbContextOptionsBuilder options,
            IServiceProvider serviceProvider)
        {
            var dbSettings = serviceProvider.GetOptions<DbSettings>();

            return options.UseNpgsql(dbSettings.ConnectionString);
        }

        private static DbContextOptionsBuilder AddAppInterceptors(
            this DbContextOptionsBuilder options,
            IServiceProvider serviceProvider)
        {
            var publishDomainEventsInterceptor = serviceProvider
                .GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            return options.AddInterceptors(publishDomainEventsInterceptor);
        }

        private static IServiceCollection AddUnitOfWork(
            this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
