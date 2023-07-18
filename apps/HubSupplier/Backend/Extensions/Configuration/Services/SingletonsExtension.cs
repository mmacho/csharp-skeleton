using Aseme.HubSupplier.RestoreIcps.Infrastructure.Created;
using Aseme.HubSupplier.RestoreIcps.Infrastructure.Updated;
using Aseme.Shared.Domain.HttpLogs.Infrastructure.Received;
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