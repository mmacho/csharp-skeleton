namespace Aseme.HubSupplier.Shared.Infrastructure.Startup
{
    public interface IPubSubHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}