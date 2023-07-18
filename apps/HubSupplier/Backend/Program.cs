using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application;
using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services;
using Aseme.Apps.HubSupplier.Backend.Extensions.DependencyInjection;
using System.Reflection;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
IServiceCollection services = webApplicationBuilder.Services;
IConfiguration configuration = webApplicationBuilder.Configuration;
Assembly executingAssembly = Assembly.GetExecutingAssembly();

services.AddApplication();
services.AddInfrastructure(configuration, webApplicationBuilder, executingAssembly);

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