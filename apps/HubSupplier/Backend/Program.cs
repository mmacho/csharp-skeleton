using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application;
using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services;
using Aseme.Apps.HubSupplier.Backend.Extensions.DependencyInjection;
using System.Reflection;

var webApplicationBuilder = WebApplication.CreateBuilder(args);
var services = webApplicationBuilder.Services;
var configuration = webApplicationBuilder.Configuration;
var executingAssembly = Assembly.GetExecutingAssembly();

//TODO: mmacho debería convergir a
services.AddApplication();
services.AddInfrastructure(configuration, executingAssembly);

var app = webApplicationBuilder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.ConfigureHealthChecks();
app.ConfigureMiddlewares();
app.ConfigureEndpoints();
app.ConfigureCors();
app.ConfigureSwaggerForDevelopmentEnvironment();

// HttpLogging
//app.UseHttpLogging();

app.Run();

//TODO: Revisar
// await app.RunAsync();

public partial class Program
{ }