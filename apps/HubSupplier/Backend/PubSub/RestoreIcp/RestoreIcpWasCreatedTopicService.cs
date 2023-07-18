using Aseme.HubSupplier.EmailNotifications.Application.Create;
using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.Shared.Domain;
using Aseme.Shared.Domain.PubSub.Base;
using Aseme.Shared.Infrastructure.PubSub.Publisher;
using Aseme.Shared.Infrastructure.PubSub.Subscriber;
using Aseme.Shared.Infrastructure.Utils;
using Hsd.Users.Aplication.Search;
using Hsd.Users.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aseme.HubSupplier.RestoreIcps.Infrastructure.Created
{
    public class RestoreIcpWasCreatedTopicService : PubSubSubscriber, IPubSubSubscriber, IRestoreIcpWasCreatedTopicService
    {
        private const string TOPIC_NAME = RestoreIcpWasCreatedMessage.TOPIC_NAME;

        private readonly ILogger<RestoreIcpWasCreatedTopicService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RestoreIcpWasCreatedTopicService(ILogger<RestoreIcpWasCreatedTopicService> logger, IPubSubPublisher pubSubPublisher, IServiceProvider serviceProvider) : base(logger, pubSubPublisher, TOPIC_NAME)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            Subscribe();
        }

        public override void Subscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(RestoreIcpWasCreatedTopicService)}' listening to topic '{TOPIC_NAME}'");
        }

        public override void Unsubscribe()
        {
            base.Subscribe();

            _logger.LogInformation($"Subscriber '{nameof(RestoreIcpWasCreatedTopicService)}' unlistening from topic '{TOPIC_NAME}'");
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

            string distributor = restoreIcp.Distributor;

            // TODO
            // basenotification -> column entity type

            // subscriber wasupdated
            // check exe
            // create callbacknotification

            using var scope = _serviceProvider.CreateScope();

            ISearchUserService searchUserService = scope.ServiceProvider.GetRequiredService<ISearchUserService>();

            UserFilter userFilter = new();
            userFilter.Distributor = distributor;

            PageResult<User> pageResult = await searchUserService.SearchAsync(userFilter);

            if (pageResult.TotalCount == 0)
            {
                _logger.LogError($"No user found with distributor '{distributor}'");
                return;
            }

            User user = pageResult.Data[0];

            EmailNotification emailNotification = new()
            {
                EmailAddress = user.Email,
                EntityType = EntityType.RESTORE_ICP,
                EntityId = restoreIcp.Id,
            };

            ICreateEmailNotificationService createEmailNotificationService = scope.ServiceProvider.GetRequiredService<ICreateEmailNotificationService>();
            await createEmailNotificationService.CreateAsync(emailNotification);
        }
    }
}