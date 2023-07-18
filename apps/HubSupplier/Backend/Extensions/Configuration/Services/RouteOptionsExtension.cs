namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class RouteOptionsExtension
    {
        // generate lowercase URLs
        public const bool LOWERCASE_URLS = true;

        public static IServiceCollection ConfigureRouteOptions(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = LOWERCASE_URLS;
            });

            return services;
        }
    }
}