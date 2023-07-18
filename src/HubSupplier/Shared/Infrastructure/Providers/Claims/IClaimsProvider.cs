namespace Aseme.HubSupplier.Shared.Infrastructure.Providers.Claims
{
    public interface IClaimsProvider
    {
        public string? OwnerId { get; }
        public string? DistributorId { get; }
    }
}