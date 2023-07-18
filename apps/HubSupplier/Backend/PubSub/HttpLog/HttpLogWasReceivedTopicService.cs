using Aseme.Shared.Domain.HttpLogs.Application.Create;
using Aseme.Shared.Domain.HttpLogs.Domain;
using Aseme.Shared.Domain.PubSub.Base;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Aseme.Shared.Infrastructure.PubSub.Subscriber;
using Aseme.Shared.Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aseme.Shared.Domain.HttpLogs.Infrastructure.Received
{
    //TODO: mmacho me gusta mas consumer
    public class HttpLogWasReceivedTopicService : PubSubSubscriber, IPubSubSubscriber, IHttpLogReceivedTopicService
    {
        private const string TOPIC_NAME = HttpLogWasReceivedMessage.TOPIC_NAME;

        private readonly ILogger<HttpLogWasReceivedTopicService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public HttpLogWasReceivedTopicService(ILogger<HttpLogWasReceivedTopicService> logger, IPubSubPublisher pubSubPublisher, IServiceProvider serviceProvider) : base(logger, pubSubPublisher, TOPIC_NAME)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            Subscribe();
        }

        public override void Subscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(HttpLogWasReceivedTopicService)}' listening to topic '{TOPIC_NAME}'");
        }

        public override void Unsubscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(HttpLogWasReceivedTopicService)}' unlistening from topic '{TOPIC_NAME}'");
        }

        public override async void Receive(string data)
        {
            base.Receive(data);

            PubSubMessage? pubSubMessage = PubSubUtils.GetPubSubMessage(data, _logger);
            HttpLog? httpLog = pubSubMessage?.Payload.ToObject<HttpLog>();

            if (httpLog == null)
            {
                _logger.LogError($"Bad message payload from topic '{TOPIC_NAME}'");
                return;
            }

            using var scope = _serviceProvider.CreateScope();

            ICreateHttpLogService httpLogService = scope.ServiceProvider.GetRequiredService<ICreateHttpLogService>();
            await httpLogService.CreateAsync(httpLog);
        }
    }
}