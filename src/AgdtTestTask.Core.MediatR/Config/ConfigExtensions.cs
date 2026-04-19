using Microsoft.Extensions.DependencyInjection;

namespace AgdtTestTask.Core.MediatR.Config
{
    public static class ConfigExtensions
    {
        public static IServiceCollection RegisterMediatR(
            this IServiceCollection services,
            Type markerType)
        {
            services.AddMediatR(x =>
                x.RegisterServicesFromAssembly(
                    markerType.Assembly));

            return services;
        }
    }
}
