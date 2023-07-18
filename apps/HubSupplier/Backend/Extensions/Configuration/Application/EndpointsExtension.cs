namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application
{
    public static partial class EndpointsExtension
    {
        public static WebApplication ConfigureEndpoints(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

            return app;
        }
    }
}