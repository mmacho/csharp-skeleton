using Aseme.HubSupplier.EmailNotifications.Application;
using Aseme.HubSupplier.EmailNotifications.Application.Create;
using Aseme.HubSupplier.EmailNotifications.Domain;
using Aseme.HubSupplier.HttpLogs.Application.Create;
using Aseme.HubSupplier.HttpLogs.Application.Get;
using Aseme.HubSupplier.HttpLogs.Domain;
using Aseme.HubSupplier.Notifications.Application;
using Aseme.HubSupplier.Notifications.Application.Search;
using Aseme.HubSupplier.Notifications.Domain;
using Aseme.HubSupplier.RestoreIcps.Application.Create;
using Aseme.HubSupplier.RestoreIcps.Application.Delete;
using Aseme.HubSupplier.RestoreIcps.Application.Get;
using Aseme.HubSupplier.RestoreIcps.Application.Search;
using Aseme.HubSupplier.RestoreIcps.Application.Update;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.RestoreIcps.Infrastructure;
using Aseme.HubSupplier.Shared.Infrastructure.Providers.Claims;
using Aseme.HubSupplier.Shared.Infrastructure.Services.Logging;
using Aseme.HubSupplier.WebhookNotifications.Application;
using Aseme.HubSupplier.WebhookNotifications.Application.Create;
using Aseme.HubSupplier.WebhookNotifications.Domain;
using Hsc.Logins.Aplication.Search;
using Hsc.Logins.Domain;
using Hsd.Users.Aplication.Search;
using Hsd.Users.Domain;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static class ScopedsExtension
    {
        //TODO: los repositories de base de datos son infra.
        //Los service son appplication
        public static IServiceCollection ConfigureAppScopeds(this IServiceCollection services)
        {
            // HTTP Context
            // infra
            services.AddScoped<IClaimsProvider, ClaimsProvider>();

            // Logging
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<IHttpLogRepository, HttpLogRepository>();
            services.AddScoped<IGetHttpLogService, GetHttpLogService>();
            services.AddScoped<ICreateHttpLogService, CreateHttpLogService>();

            // Notifications
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ISearchNotificationService, SearchNotificationService>();

            // Emails
            services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();
            services.AddScoped<ICreateEmailNotificationService, CreateEmailNotificationService>();
            services.AddScoped<INotificationSender, EmailSenderService>();

            // Webhooks
            services.AddScoped<IWebhookNotificationRepository, WebhookNotificationRepository>();
            services.AddScoped<ICreateWebhookNotificationService, CreateWebhookNotificationService>();
            services.AddScoped<INotificationSender, WebhookSenderService>();

            // Client tables
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ISearchLoginService, SearchLoginService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISearchUserService, SearchUserService>();

            // Feature
            // Restore ICP
            services.AddScoped<IRestoreIcpRepository, MsSqlRestoreIcpRepository>();
            services.AddScoped<ISearchRestoreIcpService, SearchRestoreIcpService>();
            services.AddScoped<IGetRestoreIcpService, GetRestoreIcpService>();
            services.AddScoped<ICreateRestoreIcpService, CreateRestoreIcpService>();
            services.AddScoped<IUpdateRestoreIcpService, UpdateRestoreIcpService>();
            services.AddScoped<IDeleteRestoreIcpService, DeleteRestoreIcpService>();

            return services;
        }
    }
}