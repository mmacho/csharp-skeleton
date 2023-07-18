using Aseme.Apps.HubSupplier.Backend.PubSub.HttpLogs;
using Aseme.Apps.HubSupplier.Backend.PubSub.RestoreIcps;
using Aseme.HubSupplier.Shared.Infrastructure.Startup;
using Aseme.Shared.Infrastructure.PubSub.Publisher;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static class SingletonsExtension
    {
        public static IServiceCollection ConfigureAppSingletons(this IServiceCollection services)
        {
            // PubSub
            services.AddSingleton<IPubSubPublisher, PubSubPublisher>();
            services.AddSingleton<IHttpLogReceivedTopicService, HttpLogWasReceivedTopicService>();
            services.AddSingleton<IRestoreIcpWasCreatedTopicService, RestoreIcpWasCreatedTopicService>();
            services.AddSingleton<IRestoreIcpWasUpdatedTopicService, RestoreIcpWasUpdatedTopicService>();

            return services;
        }
    }
}