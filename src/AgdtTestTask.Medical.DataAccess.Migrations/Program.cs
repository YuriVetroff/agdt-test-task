using AgdtTestTask.Medical.DataAccess.EF.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMedicalDataAccess(
            hostContext.Configuration.GetConnectionString("DefaultConnection"),
            Assembly.GetExecutingAssembly().FullName);
    })
    .UseConsoleLifetime()
    .Build();

if (args.Length > 0 && args.Contains("Migrate"))
{
    using var scope = host.Services.CreateScope();
    var context = host.Services.GetRequiredService<DbContext>();
    await context.Database.MigrateAsync();
}
