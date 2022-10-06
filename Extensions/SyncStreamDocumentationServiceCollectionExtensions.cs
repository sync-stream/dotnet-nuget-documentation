using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Documentation.Configuration;
using SyncStream.Documentation.Filters.Document;
using SyncStream.Documentation.Filters.Operation;
using SyncStream.Documentation.Model;

// Define our namespace
namespace SyncStream.Documentation.Extensions;

/// <summary>
///     This class maintains the structure of our IServiceCollection extensions for SyncStream.Documentation
/// </summary>
public static class SyncStreamDocumentationServiceCollectionExtensions
{
    /// <summary>
    ///     This method configures the <paramref name="instance" /> with a documentation example for <typeparamref name="TExample" />
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <typeparam name="TExample">The expected type of the example to use</typeparam>
    /// <returns><paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationExample<TExample>(this IServiceCollection instance)
        where TExample : IExampleProvider<TExample> => instance.AddSwaggerExamplesFromAssemblyOf<TExample>();

    /// <summary>
    ///     This method configures the <paramref name="instance" /> with Swagger and ReDoc
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="configuration">The documentation configuration values</param>
    /// <returns><paramref name="instance" /></returns>
    public static IServiceCollection UseSyncStreamDocumentation(this IServiceCollection instance,
        DocumentationConfiguration configuration)
    {
        // Add Swagger to the IServiceCollection instance
        instance.AddSwaggerGen(options =>
        {
            // Describe all parameters in camel case
            options.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;

            // Enable annotations
            options.EnableAnnotations(true, true);

            // Enable example filters
            options.ExampleFilters();

            // We want to include the XML documentation
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

            // Add our markdown documentation document filter
            options.DocumentFilter<MarkdownDocumentationDocumentFilter>();

            // Add our code example documentation operation filter
            options.OperationFilter<CodeExampleOperationFilter>();

            // Add our display name operation filter
            options.OperationFilter<DisplayNameOperationFilter>();

            // Add our markdown documentation operation filter

            options.OperationFilter<MarkdownDocumentationOperationFilter>();

            // Define our extensions
            Dictionary<string, IOpenApiExtension> extensions = new();

            // Check for a logo
            if (!string.IsNullOrEmpty(configuration.Logo) && !string.IsNullOrWhiteSpace(configuration.Logo))
            {
                // Define our extension object
                OpenApiObject logoExtension = new();

                // Add our logo URL to the configuration
                logoExtension.Add("url", new OpenApiString(configuration.Logo));

                // Add our application title to the alternate text
                logoExtension.Add("altText", new OpenApiString(configuration.Title));

                // Add the logo extension to the dictionary
                extensions.Add("x-logo", logoExtension);
            }

            // Define our open api information
            OpenApiInfo apiInfo = new();

            // Set the description into the open api information object
            apiInfo.Description = File.ReadAllText(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "readme.md"))
                .Replace("${SWAGGER_PATH}", configuration.GetFullPath(), StringComparison.CurrentCultureIgnoreCase)
                .Replace("${SWAGGER_URL}", configuration.GetFullPath(), StringComparison.CurrentCultureIgnoreCase);

            // Set the license into the open api information object
            apiInfo.License = new() { Url = configuration.License?.ToUrl() };

            // Set our extensions into the open api information object
            apiInfo.Extensions = extensions;

            // Set our application terms-of-service into the open api information object
            apiInfo.TermsOfService = configuration.TermsOfService;

            // Set our application title into the open api information object
            apiInfo.Title = configuration.Title;

            // Set our application version into the open api information object
            apiInfo.Version = configuration.Version;
        }).AddSwaggerExamples();

        // We're done, return the current IServiceCollection instance
        return instance.AddHttpContextAccessor();
    }
}