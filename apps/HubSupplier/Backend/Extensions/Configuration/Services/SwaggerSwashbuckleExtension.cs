using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Aseme.Apps.HubSupplier.Backend.Extensions.Configuration.Services
{
    /// <summary>
    /// Configure the Swagger generator.
    /// </summary>
    public static class SwaggerSwashbuckleExtension
    {
        // Add a Swagger generator and Automatic Request and Response annotations:

        /// <summary>
        /// Configure the Swagger generator with XML comments, bearer authentication, etc.
        /// Additional configuration files:
        /// <list type="bullet">
        ///     <item>ConfigureSwaggerSwashbuckleOptions.cs</item>
        /// </list>
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerSwashbuckleOptions>();

            // Configures ApiExplorer (needed from ASP.NET Core 6.0).
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            // Register the Swagger generator, defining one or more Swagger documents.
            // Read more here: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            services.AddSwaggerGen(options =>
            {
                // If we would like to provide request and response examples (Part 1/2)
                // Enable the Automatic (or Manual) annotation of the [SwaggerRequestExample] and [SwaggerResponseExample].
                // Read more here: https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters
                options.ExampleFilters();
                // If we would like to include documentation comments in the OpenAPI definition file and SwaggerUI.
                // Set the comments path for the XmlComments file.
                // Read more here: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio#xml-comments
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);
                options.EnableAnnotations();

                // If we would like to provide security information about the authorization scheme that we are using (e.g. Bearer).
                // Add Security information to each operation for bearer tokens and define the scheme.
                options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            },
                            Array.Empty<string>()
                        }
                    }
                );

                // If we use the [Authorize] attribute to specify which endpoints require Authorization, then we can
                // Show an "(Auth)" info to the summary so that we can easily see which endpoints require Authorization.
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                options.CustomOperationIds(e => $"{e.HttpMethod}_{e.RelativePath}");

                options.OrderActionsBy((apiDesc) =>
                {
                    var method = (apiDesc.HttpMethod ?? string.Empty).ToUpper();

                    if (method == "GET")
                    {
                        return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}1{apiDesc.RelativePath}";
                    }
                    else if (method == "POST")
                    {
                        return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}2{apiDesc.RelativePath}";
                    }
                    else if (method == "PUT")
                    {
                        return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}3{apiDesc.RelativePath}";
                    }
                    else if (method == "DELETE")
                    {
                        return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}4{apiDesc.RelativePath}";
                    }

                    // Set a higher value for other methods if needed
                    return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}5{apiDesc.RelativePath}";
                });
            });

            // If we would like to provide request and response examples (Part 2/2)
            // Register examples with the ServiceProvider based on the location assembly or example type.
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            // If we are using FluentValidation, then we can register the following service to add the  fluent validation rules to swagger.
            // Adds FluentValidationRules staff to Swagger. (Minimal configuration)
            services.AddFluentValidationRulesToSwagger();
        }
    }
}