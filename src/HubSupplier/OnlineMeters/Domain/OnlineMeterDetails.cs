namespace Aseme.HubSupplier.OnlineMeters.Domain
{
    public class OnlineMeterDetails
    {
        public const string TableName = "OnlineMeterDetails";

        public long OnlineMeterId { get; set; }

        public string SerialNumber { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int InstallationYear { get; set; }

        public DateTime ReadingDate { get; set; }

        public int Period { get; set; }

        public virtual OnlineMeter OnlineMeter { get; set; }
    }
}