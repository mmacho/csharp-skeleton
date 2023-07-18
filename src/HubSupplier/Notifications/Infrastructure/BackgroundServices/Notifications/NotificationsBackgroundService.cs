using Aseme.HubSupplier.Notifications.Application;
using Aseme.HubSupplier.Notifications.Application.Search;
using Aseme.HubSupplier.Notifications.Domain;
using Aseme.HubSupplier.Shared.Domain.Notification;
using Aseme.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aseme.HubSupplier.Notifications.Infrastructure.BackgroundServices.Notifications
{
    public class NotificationsBackgroundService : BackgroundService, INotificationsBackgroundService
    {
        private readonly ILogger<NotificationsBackgroundService> _logger;
        private readonly INotificationSettings _options;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<Type, INotificationSender> _notificationTypes = new();

        public NotificationsBackgroundService(ILogger<NotificationsBackgroundService> logger, IOptions<INotificationSettings> options, IServiceProvider serviceProvider)

        {
            _logger = logger;
            _options = options.Value;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting notifications background service...");
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started notifications background service");

            // Fire right away
            await ProcessNotificationsAsync();

            // Interval
            int intervalMinutes = _options.IntervalMinutes;
            PeriodicTimer periodicTimer = new(TimeSpan.FromMinutes(intervalMinutes));

            while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
            {
                await ProcessNotificationsAsync();
            }
        }

        private void SetNotificationTypes(IServiceScope scope)
        {
            IEnumerable<INotificationSender> notificationSenders = scope.ServiceProvider.GetRequiredService<IEnumerable<INotificationSender>>();

            foreach (INotificationSender notificationSender in notificationSenders)
            {
                Type senderType = notificationSender.GetSenderType();
                _notificationTypes.Add(senderType, notificationSender);
            }
        }

        private async Task ProcessNotificationsAsync()
        {
            using var scope = _serviceProvider.CreateScope();

            // Fill notification types from DI
            SetNotificationTypes(scope);

            ISearchNotificationService searchNotificationService = scope.ServiceProvider.GetRequiredService<ISearchNotificationService>();
            NotificationFilter notificationFilter = new()
            {
                SentState = 0
            };

            PageResult<BaseNotification> pageResult = await searchNotificationService.SearchAsync(notificationFilter);
            List<BaseNotification> notifications = pageResult.Data;

            _logger.LogInformation($"Pending notifications: {notifications.Count}");

            foreach (BaseNotification notification in notifications)
            {
                Type notificationType = notification.GetType();

                if (_notificationTypes.ContainsKey(notificationType))
                {
                    await _notificationTypes[notificationType].SendAsync(notification);
                }
            }

            _logger.LogInformation("All notifications sent");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping notifications background service...");
            await base.StopAsync(cancellationToken);
        }
    }
}