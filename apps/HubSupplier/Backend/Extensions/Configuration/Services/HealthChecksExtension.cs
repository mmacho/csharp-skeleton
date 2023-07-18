using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class HealthChecksExtension
    {
        public const string SQL_SERVER_CONNECTION_STRING_KEY = "HubSuppliersConnection";
        public const string SQL_SERVER_NAME = "SQL Server";
        public const string SQL_SERVER_HEALTH_QUERY = "SELECT 1;";

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddSqlServer(
                    configuration.GetConnectionString(SQL_SERVER_CONNECTION_STRING_KEY) ?? string.Empty,
                    name: SQL_SERVER_NAME,
                    healthQuery: SQL_SERVER_HEALTH_QUERY,
                    failureStatus: HealthStatus.Degraded,
                    tags: new string[] { "searchengine", "sql", "sqlserver" }
                );
            return services;
        }
    }
}