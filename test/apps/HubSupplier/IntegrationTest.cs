using Aseme.Shared.Domain.HttpLogs.Application.Get;
using Hsc.Functionalities.Domain;
using Hsc.Logins.Domain;
using Hsc.Roles.Domain;
using Hsc.RolesFunctionalities.Domain;
using Hsc.Shared.Infrastructure.Persistence.EntityFramework;
using Hsc.UsersRoles.Domain;
using HubSupplierTest.apps.Configuration.Services;
using HubSupplierTest.apps.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace HubSupplierTest.apps
{
    public class IntegrationTest
    {
        protected readonly ILogger Logger;
        protected IGetHttpLogService IGetHttpLogService { get; private set; }
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = ConfigureServices();
            var scope = appFactory.Services.CreateScope();

            // Services
            Logger = scope.ServiceProvider.GetRequiredService<ILogger<IntegrationTest>>();
            TestClient = appFactory.CreateClient();
            IGetHttpLogService = scope.ServiceProvider.GetRequiredService<IGetHttpLogService>();

            // Context
            ConfigureHscDbContext(scope);
        }

        protected void LogTestCase()
        {
            MethodBase? method = new StackFrame(4).GetMethod();
            string methodName = method?.Name ?? string.Empty;

            Logger.LogInformation(Environment.NewLine);
            Logger.LogInformation("## TEST CASE ##");
            Logger.LogInformation(methodName);
            Logger.LogInformation(Environment.NewLine);
        }

        private static WebApplicationFactory<Program> ConfigureServices()
        {
            return new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddLogging();
                    services.ConfigureDbContext();
                });
            });
        }

        private void ConfigureHscDbContext(IServiceScope scope)
        {
            var hscDbContext = scope.ServiceProvider.GetRequiredService<HscDbContext>();

            hscDbContext.Database.EnsureDeleted();
            hscDbContext.Database.EnsureCreated();

            CreateInitialData(hscDbContext);
        }

        private void CreateInitialData(HscDbContext hscDbContext)
        {
            CreateUsers(hscDbContext);
            CreateRoles(hscDbContext);
            CreateUserRoles(hscDbContext);
            CreateFunctionalities(hscDbContext);
            CreateRoleFunctionalities(hscDbContext);

            Logger.LogInformation("Initial data created");
        }

        private static void CreateUsers(HscDbContext hscDbContext)
        {
            Login portalUser1 = new()
            {
                UserName = AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1,
                Password = string.Empty,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                AuthorizedThirdPartyFilterMode = 1
            };

            hscDbContext.Users.Add(portalUser1);

            Login portalUser2 = new()
            {
                UserName = AuthorizationTestConstants.AUTHORIZED_USER_PORTAL2,
                Password = string.Empty,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                AuthorizedThirdPartyFilterMode = 1
            };

            hscDbContext.Users.Add(portalUser2);

            Login distributor1 = new()
            {
                UserName = AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1,
                Password = string.Empty,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                AuthorizedThirdPartyFilterMode = 1
            };

            hscDbContext.Users.Add(distributor1);

            Login distributor2 = new()
            {
                UserName = AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR2,
                Password = string.Empty,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                AuthorizedThirdPartyFilterMode = 1
            };

            hscDbContext.Users.Add(distributor2);

            Login unauthorizedUser = new()
            {
                UserName = AuthorizationTestConstants.UNAUTHORIZED_USERNAME,
                Password = string.Empty,
                IsDeleted = false,
                CreationDate = DateTime.Now,
                AuthorizedThirdPartyFilterMode = 1
            };

            hscDbContext.Users.Add(unauthorizedUser);

            hscDbContext.SaveChanges();
        }

        private static void CreateRoles(HscDbContext hscDbContext)
        {
            Role datadisRole = new()
            {
                Description = AuthorizationTestConstants.DATADAIS_ROLE,
                Functionalities = new Collection<RoleFunctionalities>()
            };

            hscDbContext.Roles.Add(datadisRole);

            Role networkUserPlatformRole = new()
            {
                Description = AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE,
                Functionalities = new Collection<RoleFunctionalities>()
            };

            hscDbContext.Roles.Add(networkUserPlatformRole);

            hscDbContext.SaveChanges();
        }

        private static void CreateUserRoles(HscDbContext hscDbContext)
        {
            UserRoles datadisUserRole = new()
            {
                User = hscDbContext.Users.Where(user => user.UserName == AuthorizationTestConstants.UNAUTHORIZED_USERNAME).First(),
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.DATADAIS_ROLE).First()
            };

            hscDbContext.UserRoles.Add(datadisUserRole);

            UserRoles networkUserPlatformUserRole1 = new()
            {
                User = hscDbContext.Users.Where(user => user.UserName == AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1).First(),
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE).First()
            };

            hscDbContext.UserRoles.Add(networkUserPlatformUserRole1);

            UserRoles networkUserPlatformUserRole2 = new()
            {
                User = hscDbContext.Users.Where(user => user.UserName == AuthorizationTestConstants.AUTHORIZED_USER_PORTAL2).First(),
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE).First()
            };

            hscDbContext.UserRoles.Add(networkUserPlatformUserRole2);

            UserRoles networkUserPlatformUserRole3 = new()
            {
                User = hscDbContext.Users.Where(user => user.UserName == AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1).First(),
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE).First()
            };

            hscDbContext.UserRoles.Add(networkUserPlatformUserRole3);

            UserRoles networkUserPlatformUserRole4 = new()
            {
                User = hscDbContext.Users.Where(user => user.UserName == AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR2).First(),
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE).First()
            };

            hscDbContext.UserRoles.Add(networkUserPlatformUserRole4);

            hscDbContext.SaveChanges();
        }

        private static void CreateFunctionalities(HscDbContext hscDbContext)
        {
            Functionality distributorDataFilterFunctionality = new()
            {
                Description = AuthorizationTestConstants.DISTRIBUTOR_DATA_FILTER_FUNCTIONALITY
            };

            hscDbContext.Functionalities.Add(distributorDataFilterFunctionality);

            Functionality apiContractDataFunctionality = new()
            {
                Description = AuthorizationTestConstants.CONTRACT_API_DATA_FUNCTIONALITY
            };

            hscDbContext.Functionalities.Add(apiContractDataFunctionality);

            Functionality hubSuppliersAccessFunctionality = new()
            {
                Description = AuthorizationTestConstants.HUB_SUPPLIERS_FUNCTIONALITY
            };

            hscDbContext.Functionalities.Add(hubSuppliersAccessFunctionality);

            hscDbContext.SaveChanges();
        }

        private static void CreateRoleFunctionalities(HscDbContext hscDbContext)
        {
            RoleFunctionalities distributorDataFilterRoleFunctionality = new()
            {
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.DATADAIS_ROLE).First(),
                Functionality = hscDbContext.Functionalities.Where(functionality => functionality.Description == AuthorizationTestConstants.DISTRIBUTOR_DATA_FILTER_FUNCTIONALITY).First(),
                Value = string.Empty
            };

            hscDbContext.RoleFunctionalities.Add(distributorDataFilterRoleFunctionality);

            RoleFunctionalities hubSuppliersRoleFunctionality = new()
            {
                Role = hscDbContext.Roles.Where(role => role.Description == AuthorizationTestConstants.NETWORK_USER_PLATFORM_ROLE).First(),
                Functionality = hscDbContext.Functionalities.Where(functionality => functionality.Description == AuthorizationTestConstants.HUB_SUPPLIERS_FUNCTIONALITY).First(),
                Value = string.Empty
            };

            hscDbContext.RoleFunctionalities.Add(hubSuppliersRoleFunctionality);

            hscDbContext.SaveChanges();
        }
    }
}