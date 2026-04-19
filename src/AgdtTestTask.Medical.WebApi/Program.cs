using AgdtTestTask.Core.Web.Config;
using AgdtTestTask.Medical.WebApi.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMedicalWebApi(builder.Configuration);
builder.CreateDefaultApp().Run();
