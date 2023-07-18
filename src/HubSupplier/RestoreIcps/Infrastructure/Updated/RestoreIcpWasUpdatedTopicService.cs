using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.RestoreIcps.Infrastructure.Created;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.HubSupplier.WebhookNotifications.Application.Create;
using Aseme.HubSupplier.WebhookNotifications.Domain;
using Aseme.Shared.Domain.PubSub.Base;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Aseme.Shared.Infrastructure.PubSub.Subscriber;
using Aseme.Shared.Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aseme.HubSupplier.RestoreIcps.Infrastructure.Updated
{
    public class RestoreIcpWasUpdatedTopicService : PubSubSubscriber, IPubSubSubscriber, IRestoreIcpWasUpdatedTopicService
    {
        private const string TOPIC_NAME = RestoreIcpWasUpdatedMessage.TOPIC_NAME;

        private readonly ILogger<RestoreIcpWasCreatedTopicService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RestoreIcpWasUpdatedTopicService(ILogger<RestoreIcpWasCreatedTopicService> logger, IPubSubPublisher pubSubPublisher, IServiceProvider serviceProvider) : base(logger, pubSubPublisher, TOPIC_NAME)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            Subscribe();
        }

        public override void Subscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(RestoreIcpWasUpdatedTopicService)}' listening to topic '{TOPIC_NAME}'");
        }

        public override void Unsubscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(RestoreIcpWasUpdatedTopicService)}' unlistening from topic '{TOPIC_NAME}'");
        }

        public override async void Receive(string data)
        {
            base.Receive(data);

            PubSubMessage? pubSubMessage = PubSubUtils.GetPubSubMessage(data, _logger);
            RestoreIcp? restoreIcp = pubSubMessage?.Payload.ToObject<RestoreIcp>();

            if (restoreIcp == null)
            {
                _logger.LogError($"Bad message payload from topic '{TOPIC_NAME}'");
                return;
            }

            using var scope = _serviceProvider.CreateScope();

            ICreateWebhookNotificationService createWebhookNotificationService = scope.ServiceProvider.GetRequiredService<ICreateWebhookNotificationService>();

            OperationStatusType operationStatus = restoreIcp.OperationStatus;
            bool notExecuted = OperationStatusType.EXE.Equals(operationStatus) == false;

            if (notExecuted)
            {
                return;
            }

            //string distributor = restoreIcp.Distributor;

            WebhookNotification webhookNotification = new()
            {
                Url = "http://example.com"
            };

            await createWebhookNotificationService.CreateAsync(webhookNotification);
        }
    }
}