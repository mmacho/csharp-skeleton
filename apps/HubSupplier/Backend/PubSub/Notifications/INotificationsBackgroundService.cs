namespace Aseme.HubSupplier.Notifications.Infrastructure.BackgroundServices.Notifications
{
    public interface INotificationsBackgroundService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}