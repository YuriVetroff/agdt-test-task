using Microsoft.Extensions.DependencyInjection;

namespace AgdtTestTask.Core.Mapping.Config
{
    public static class AutoMapperConfigExtensions
    {
        public static IServiceCollection RegisterMappingProfiles(
            this IServiceCollection services,
            Type markerType)
        {
            services.AddAutoMapper(x => x.AddMaps(markerType));

            return services;
        }
    }
}
