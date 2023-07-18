using Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Conventions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    public static partial class ControllersExtension
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            // Add services to the container.
            services
                .AddControllers(options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                    options.Conventions.Add(new ControllerNameAttributeConvention());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Program>();

            //  .AddXmlSerializerFormatters()
            //  .AddXmlDataContractSerializerFormatters();

            return services;
        }
    }
}