namespace Aseme.Shared.Domain
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        DateTime LastModifiedDate { get; set; }

        string OwnerId { get; set; }
    }
}