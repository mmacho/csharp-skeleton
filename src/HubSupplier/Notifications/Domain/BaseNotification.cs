using Aseme.Shared.Domain.Support;
using Newtonsoft.Json;

namespace Aseme.HubSupplier.Notifications.Domain
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