namespace Aseme.HubSupplier.RestoreIcps.Domain
{
    [Serializable]
    public class RestoreIcpDetails
    {
        public const string TableName = "ResctoreIcpDetails";

        public long RestoreIcpId { get; set; }

        public RestoreIcpStatusType RestoreIcpStatus { get; set; }

        public DateTime ExecutionDate { get; set; }

        public string Description { get; set; }
    }
}