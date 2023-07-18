namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application
{
    public static partial class CorsExtension
    {
        public static WebApplication ConfigureCors(this WebApplication app)
        {
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            return app;
        }
    }
}