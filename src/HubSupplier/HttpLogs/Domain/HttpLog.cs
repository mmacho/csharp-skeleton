using Aseme.Shared.Domain.Support;

namespace Aseme.HubSupplier.HttpLogs.Domain
{
    public class HttpLog : LegacyEntity
    {
        public const string TableName = "HttpLog";

        public long Id { get; private set; }

        public DateTime ReceivedDateTime { get; set; }

        public string? IpAddress { get; set; }

        public string Scheme { get; set; }

        public string HttpMethod { get; set; }

        public string HttpPath { get; set; }

        public string? HttpQueryParams { get; set; }

        public string? HttpRequestHeaders { get; set; }

        public string? HttpRequestBody { get; set; }

        public string? HttpResponseHeaders { get; set; }

        public string? HttpResponseBody { get; set; }

        public int HttpStatusCode { get; set; }

        public int? ExecutionTime { get; set; }

        public long? EntityId { get; set; }
    }
}