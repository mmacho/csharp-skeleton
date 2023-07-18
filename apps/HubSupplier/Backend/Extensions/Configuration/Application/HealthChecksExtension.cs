using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application
{
    public static partial class HealthChecksExtension
    {
        public static WebApplication ConfigureHealthChecks(this WebApplication app)
        {
            app
                // Standard AspNet Healthcheck
                .UseHealthChecks(RequestConstants.HEALTH_PATH, new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}