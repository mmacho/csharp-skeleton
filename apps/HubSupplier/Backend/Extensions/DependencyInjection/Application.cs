using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.DependencyInjection
{
    public static class Application
    {
        internal static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //TODO: mmacho meter todo lo que corresponde a application
            services.ConfigureAppSingletons();
            services.ConfigureAppScopeds();
            return services;
        }
    }
}
