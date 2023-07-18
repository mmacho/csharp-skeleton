using Aseme.Apps.HubSupplier.Backend.Settings;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class OptionsExtension
    {
        public const string HTTP_LOGGER_SETTINGS_SECTION_KEY = "HttpLoggerSettings";
        public const string JWT_SETTINGS_SECTION_KEY = "JwtSettings";
        public const string NOTIFICATION_SETTINGS_SECTION_KEY = "NotificationSettings";

        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<HttpLoggerSettings>().Bind(configuration.GetSection(HTTP_LOGGER_SETTINGS_SECTION_KEY)).ValidateDataAnnotations();
            services.AddOptions<JwtSettings>().Bind(configuration.GetSection(JWT_SETTINGS_SECTION_KEY)).ValidateDataAnnotations();
            services.AddOptions<NotificationSettings>().Bind(configuration.GetSection(NOTIFICATION_SETTINGS_SECTION_KEY)).ValidateDataAnnotations();

            return services;
        }
    }
}