using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using AgdtTestTask.Core.Mapping.Config;

namespace AgdtTestTask.Medical.BusinessLogic.Config
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddMedicalBusinessLogic(
            this IServiceCollection services)
        {
            services.RegisterMappingProfiles(
                typeof(ConfigExtensions));

            services.AddScoped<
                IPatientSearchingService,
                PatientSearchingService>();

            services.AddScoped<
                IPatientModifyingService,
                PatientModifyingService>();

            return services;
        }
    }
}
