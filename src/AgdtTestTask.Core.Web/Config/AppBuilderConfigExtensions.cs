using AgdtTestTask.Core.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgdtTestTask.Core.Web.Config
{
    public static class AppBuilderConfigExtensions
    {
        public static WebApplication CreateDefaultApp(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMiddlewares();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
