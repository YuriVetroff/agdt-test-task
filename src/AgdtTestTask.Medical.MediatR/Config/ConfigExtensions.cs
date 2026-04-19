using AgdtTestTask.Core.MediatR.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AgdtTestTask.Medical.MediatR.Config
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddMedicalMediatR(
            this IServiceCollection services)
        {
            services.RegisterMediatR(
                typeof(ConfigExtensions));

            return services;
        }
    }
}
