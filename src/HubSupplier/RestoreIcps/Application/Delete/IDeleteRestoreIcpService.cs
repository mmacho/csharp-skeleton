namespace Aseme.HubSupplier.RestoreIcps.Application.Delete
{
    public interface IDeleteRestoreIcpService
    {
        Task DeleteAsync(long id);
    }
}