using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
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
        DocumentationConfiguration configuration) => instance.UseSwagger(options => options.SerializeAsV2 = false)
        .UseReDoc(options =>
        {
            // Set the document title
            options.DocumentTitle = configuration.GetTitle();

            // Set the route prefix
            options.RoutePrefix = configuration.RoutePrefix;

            // Define our ReDoc index
            options.IndexStream = configuration.GetReDocIndex;

            // Sanitize user input
            options.EnableUntrustedSpec();

            // Expand successful responses
            options.ExpandResponses("200,201");

            // Do NOT inject authentication setting automatically
            options.NoAutoAuth();

            // Show the path and verb in the middle panel
            options.PathInMiddlePanel();

            // List required properties first
            options.RequiredPropsFirst();

            // Set the vertical scroll offset
            options.ScrollYOffset(10);

            // Sort properties alphabetically
            options.SortPropsAlphabetically();

            // Localize our application's feature collection
            FeatureCollection features = instance.Properties["server.features"] as FeatureCollection;

            // Localize the addresses of the application
            IServerAddressesFeature addresses = features?.Get<IServerAddressesFeature>();

            // Localize the host URL
            Uri hostUrl = addresses?.Addresses.Any() is true ? new(addresses.Addresses.First()) : null;

            // Define our specification URL
            options.SpecUrl(configuration.GetFullUrl(hostUrl));
        });
}
