using AgdtTestTask.Core.DataAccess.EF.Config;
using AgdtTestTask.Medical.DataAccess.EF.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AgdtTestTask.Medical.DataAccess.EF.Config
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddMedicalDataAccess(
            this IServiceCollection services,
            string connectionString,
            string migrationsAssembly = null)
        {
            services.AddEFDataAccess<AgdtMedicalDbContext>(
                connectionString, migrationsAssembly);

            return services;
        }
    }
}
