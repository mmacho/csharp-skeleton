using Aseme.HubSupplier.Notifications.Infrastructure.BackgroundServices.Notifications;
using Aseme.HubSupplier.Shared.Infrastructure.Startup;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class HostedServicesExtension
    {
        public static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<PubSubHostedService>();
            services.AddHostedService<NotificationsBackgroundService>();

            return services;
        }
    }
}