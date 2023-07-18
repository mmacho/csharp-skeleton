using NLog.Web;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application.Builder
{
    public static partial class NLogExtension
    {
        public static WebApplicationBuilder ConfigureNLog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            return builder;
        }
    }
}