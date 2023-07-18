using Aseme.Shared.Domain;
using Newtonsoft.Json;

namespace Aseme.HubSupplier.Shared.Domain.Notification
{
    public class BaseNotification : BaseEntity
    {
        public const string TableName = "BaseNotification";

        public DateTime? SentDate { get; set; }

        public int SentState { get; set; }

        public EntityType EntityType { get; set; }

        public long EntityId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}