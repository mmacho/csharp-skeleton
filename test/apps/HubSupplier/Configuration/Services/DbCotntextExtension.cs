using Aseme.HubSupplier.Shared.Infrastructure.Persistence.EntityFramework;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;
using Hsd.Shared.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HubSupplierTest.apps.Configuration.Services
{
    public static partial class DbContextExtension
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            RemoveDescriptors(services);

            services.AddDbContext<HscDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            }, ServiceLifetime.Scoped);

            services.AddDbContext<HsdDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            }, ServiceLifetime.Scoped);

            services.AddDbContext<HubSuppliersDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            }, ServiceLifetime.Scoped);

            return services;
        }

        private static void RemoveDescriptors(IServiceCollection services)
        {
            var descriptor1 = services.SingleOrDefault(
                                     d => d.ServiceType ==
                                         typeof(DbContextOptions<HubSuppliersDbContext>));

            if (descriptor1 != null)
            {
                services.Remove(descriptor1);
            }

            var descriptor2 = services.SingleOrDefault(
                                     d => d.ServiceType ==
                                         typeof(DbContextOptions<HscDbContext>));

            if (descriptor2 != null)
            {
                services.Remove(descriptor2);
            }

            var descriptor3 = services.SingleOrDefault(
                 d => d.ServiceType ==
                     typeof(DbContextOptions<HsdDbContext>));

            if (descriptor3 != null)
            {
                services.Remove(descriptor3);
            }
        }
    }
}