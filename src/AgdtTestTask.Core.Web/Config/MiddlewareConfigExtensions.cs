using AgdtTestTask.Core.Web.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace AgdtTestTask.Core.Web.Config
{
    internal static class MiddlewareConfigExtensions
    {
        public static IServiceCollection AddMiddlewares(
            this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            return services;
        }
    }
}
