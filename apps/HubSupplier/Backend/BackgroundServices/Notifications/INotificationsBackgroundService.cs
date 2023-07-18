namespace Aseme.Apps.HubSupplier.Backend.BackgroundServices.Notifications
{
    public interface INotificationsBackgroundService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}