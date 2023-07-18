using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application.Builder;
using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services;
using System.Reflection;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.DependencyInjection
{
    public static class Infrastructure
    {
        internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder webApplicationBuilder, Assembly executingAssembly)
        {
            //TODO: mmacho meter todo lo que corresponde a application

            webApplicationBuilder.ConfigureNLog();

            services.ConfigureHealthChecks(configuration);
            services.ConfigureAuthentication(configuration);
            services.ConfigureAuthorization();
            services.ConfigureDbContext(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureSingletons();
            services.ConfigureHostedServices();
            services.ConfigureRouteOptions();
            services.ConfigureApiVersioning();
            services.ConfigureControllers();
            services.ConfigureSwagger();

            services.AddAutoMapper(executingAssembly);

            return services;
        }
    }
}
