using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Application
{
    public static partial class SwwaggerConfiguration
    {
        private const string SWAGGER_JSON_PATH = "/swagger.json";

        public static WebApplication ConfigureSwaggerForDevelopmentEnvironment(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable middleware to serve the generated OpenAPI definition as JSON files.
                app.UseSwagger();

                // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.) by specifying the Swagger JSON endpoint(s).
                var descriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwaggerUI(options =>
                {
                    options.EnableTryItOutByDefault();

                    // Build a swagger endpoint for each discovered API version
                    foreach (var description in descriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{description.GroupName}{SWAGGER_JSON_PATH}", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            return app;
        }
    }
}