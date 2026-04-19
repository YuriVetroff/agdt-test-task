using AgdtTestTask.Core.DataAccess.Abstracts;
using AgdtTestTask.Core.DataAccess.EF.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AgdtTestTask.Core.DataAccess.EF.Config
{
    public static class EFConfigExtensions
    {
        public static IServiceCollection AddEFDataAccess<TContext>(
            this IServiceCollection services,
            string connectionString,
            string migrationsAssembly)
            where TContext : DbContext
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(
                    nameof(connectionString));
            }

            services.AddEFDataAccess<TContext>(
                options => options.UseSqlServer(
                    connectionString,
                    sql =>
                    {
                        sql.MigrationsAssembly(migrationsAssembly);
                        sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
                    })

            );

            return services;
        }
        private static IServiceCollection AddEFDataAccess<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
            where TContext : DbContext
        {
            services
                .AddDbContext<TContext>(options)
                .AddScoped<DbContext>(
                    x => x.GetRequiredService<TContext>())
                .AddScoped<IDatabaseFacade, EFDatabaseFacade>();

            services.TryAddScoped(
                typeof(IRepository<>),
                typeof(EFRepository<>));

            return services;
        }
    }
}
