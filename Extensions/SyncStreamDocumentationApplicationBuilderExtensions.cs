using Microsoft.AspNetCore.Builder;
using SyncStream.Documentation.Configuration;

// Define our namespace
namespace SyncStream.Documentation.Extensions;

/// <summary>
///     This class maintains our IApplicationBuilder extensions for SyncStream.Documentation
/// </summary>
public static class SyncStreamDocumentationApplicationBuilderExtensions
{
    /// <summary>
    ///     This method sets up Swagger/ReDoc for an application
    /// </summary>
    /// <param name="instance">The current instance of IApplicationBuilder</param>
    /// <param name="configuration">The configuration values for the documentation</param>
    /// <returns><paramref name="instance" /></returns>
    public static IApplicationBuilder UseSyncStreamDocumentation(this IApplicationBuilder instance,
        DocumentationConfiguration configuration) => instance
        .UseSwagger(swaggerOptions => swaggerOptions.SerializeAsV2 = false).UseReDoc(redocOptions =>
        {
            // Set the document title
            redocOptions.DocumentTitle = configuration.GetTitle();

            // Set the route prefix
            redocOptions.RoutePrefix = configuration.RoutePrefix;

            // Define our ReDoc index
            redocOptions.IndexStream = configuration.GetReDocIndex;

            // Sanitize user input
            redocOptions.EnableUntrustedSpec();

            // Expand successful responses
            redocOptions.ExpandResponses("200,201");

            // Do NOT inject authentication setting automatically
            redocOptions.NoAutoAuth();

            // Show the path and verb in the middle panel
            redocOptions.PathInMiddlePanel();

            // List required properties first
            redocOptions.RequiredPropsFirst();

            // Set the vertical scroll offset
            redocOptions.ScrollYOffset(10);

            // Sort properties alphabetically
            redocOptions.SortPropsAlphabetically();

            // Define our specification URL
            redocOptions.SpecUrl(configuration.GetFullPath());
        });
}
