using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerSwashbuckleOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        private const string API_TITLE = "Supply HUB API";
        private const string API_DESCRIPTION = "An ASP.NET Core Web API for managing supplies";
        private const string API_VERSION = "v1";
        private const string API_CONTACT_NAME = "CIC Consulting Informatico";
        private const string API_CONTACT_EMAIL = "https://www.cic.es/";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerSwashbuckleOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerSwashbuckleOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = API_TITLE,
                Description = API_DESCRIPTION,
                Version = API_VERSION,
                Contact = new OpenApiContact
                {
                    Name = API_CONTACT_NAME,
                    Url = new Uri(API_CONTACT_EMAIL)
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " [This API version has been deprecated]";
            }

            return info;
        }
    }
}