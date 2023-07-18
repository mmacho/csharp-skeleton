using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;
using Hsd.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class DbContextExtension
    {
        public const string HubSuppliersConnection = "HubSuppliersConnection";
        public const string HscConnection = "HscConnection";
        public const string HsdConnection = "HsdConnection";

        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HubSuppliersDbContext>(options =>
             {
                 options.UseSqlServer(configuration.GetConnectionString(HubSuppliersConnection), options => options.EnableRetryOnFailure());
                 options.EnableSensitiveDataLogging();
             }, ServiceLifetime.Scoped);

            services.AddDbContext<HscDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(HscConnection), options => options.EnableRetryOnFailure());
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Scoped);

            services.AddDbContext<HsdDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(HsdConnection), options => options.EnableRetryOnFailure());
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Scoped);

            return services;
        }
    }
}