using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aseme.HubSupplier.Shared.Infrastructure.Startup
{
    public class PubSubHostedService : IHostedService, IPubSubHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public PubSubHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceProvider.GetRequiredService<IPubSubPublisher>();
            //TODO REVISAR ESTO
            //_serviceProvider.GetRequiredService<IHttpLogReceivedTopicService>();
            //_serviceProvider.GetRequiredService<IRestoreIcpWasCreatedTopicService>();
            //_serviceProvider.GetRequiredService<IRestoreIcpWasUpdatedTopicService>();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}