using Aseme.Shared.Infrastructure.Services.PageUri;
using Microsoft.Extensions.Caching.Memory;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class SingletonsExtesion
    {
        public static IServiceCollection ConfigureSingletons(this IServiceCollection services)
        {
            // HttpContextAccessor
            services.AddHttpContextAccessor();

            // URI
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

                return new UriService(uri);
            });

            // Cache
            services.AddSingleton<IMemoryCache, MemoryCache>();

            return services;
        }
    }
}