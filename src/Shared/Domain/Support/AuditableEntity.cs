namespace Aseme.Shared.Domain.Support
{
    // IOwnedBy
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string OwnerId { get; set; }
    }
}