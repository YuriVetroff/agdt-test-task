using AgdtTestTask.Core.Mapping.Config;
using AgdtTestTask.Medical.BusinessLogic.Config;
using AgdtTestTask.Medical.DataAccess.EF.Config;
using AgdtTestTask.Medical.MediatR.Config;

namespace AgdtTestTask.Medical.WebApi.Config
{
    internal static class ConfigExtensions
    {
        public static IServiceCollection AddMedicalWebApi(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.RegisterMappingProfiles(
                typeof(ConfigExtensions));

            services.AddMedicalDataAccess(
                config.GetConnectionString("DefaultConnection"));

            services.AddMedicalBusinessLogic();

            services.AddMedicalMediatR();

            return services;
        }
    }
}
