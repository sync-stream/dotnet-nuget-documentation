using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using SyncStream.Documentation.Enum;
using SyncStream.Documentation.Extensions;

// Define our namespace
namespace SyncStream.Documentation.Configuration;

/// <summary>
///     This class maintains the configuration values for the SyncStream.Documentation library
/// </summary>
[XmlRoot("documentationConfiguration")]
public class DocumentationConfiguration
{
    /// <summary>
    ///     This property contains the default host and protocol
    /// </summary>
    private static readonly string Host = "http://localhost/";

    /// <summary>
    ///     This property contains the path Swagger/ReDoc will be accessible at
    /// </summary>
    private static readonly string Path = "/swagger";

    /// <summary>
    ///     This property contains the default ReDoc index.html content
    /// </summary>
    private static readonly string ReDocIndexHtml =
        "<!DOCTYPE html>" +
        "<html lang=\"en\">" +
        "<head>" +
        "<base target=\"_blank\">" +
        "<meta charset=\"utf-8\" />" +
        "<link rel=\"shortcut icon\" href=\"/asset/brand/acx/logo/icon\" />" +
        "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />" +
        "<meta name=\"theme-color\" content=\"#000000\" />" +
        "<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Montserrat:300,400,700|Roboto:300,400,700\" />" +
        "<link rel=\"stylesheet\" href=\"https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap\" />" +
        "<style>body { margin: 0; padding: 0; }</style>" +
        "<title>%(DocumentTitle)</title>%(HeadContent)" +
        "</head><body>" +
        "<noscript>You need to enable JavaScript to run this app.</noscript>" +
        "<div id=\"documentation-container\"></div>" +
        "<script src=\"https://cdn.jsdelivr.net/npm/redoc@next/bundles/redoc.standalone.js\"></script>" +
        "<script type=\"text/javascript\">" +
        "(function() { Redoc.init('%(SpecUrl)', { enumSkipQuotes: true, expandDefaultServerVariables: true, menuToggle: false, hideDownloadButton: true, untrustedSpec: false, requiredPropsFirst: true, showExtensions: false, sortPropsAlphabetically: true }, document.getElementById('documentation-container')); })();" +
        "</script>" +
        "</body></html>";

    /// <summary>
    ///     This property contains the file path or HTML for the ReDoc index
    /// </summary>
    [ConfigurationKeyName("documentationIndex")]
    [JsonPropertyName("documentationIndex")]
    [XmlText]
    public string DocumentationIndex { get; set; }

    /// <summary>
    ///     This property contains the host url where Swagger and ReDoc will live
    /// </summary>
    [ConfigurationKeyName("hostUrl")]
    [JsonPropertyName("hostUrl")]
    [XmlAttribute("hostUrl")]
    public string HostUrl { get; set; } = Host;

    /// <summary>
    ///     This property denotes whether to include the application's XML comments or not
    /// </summary>
    [ConfigurationKeyName("includeXmlComments")]
    [JsonPropertyName("includeXmlComments")]
    [XmlAttribute("includeXmlComments")]
    public bool IncludeXmlComments { get; set; }

    /// <summary>
    ///     This property contains the software license
    /// </summary>
    [ConfigurationKeyName("license")]
    [JsonPropertyName("license")]
    [XmlAttribute("license")]
    public DocumentationLicenseEnum License { get; set; } = DocumentationLicenseEnum.Proprietary;

    /// <summary>
    ///     This property contains the URL or file path to the URL
    /// </summary>
    [ConfigurationKeyName("logo")]
    [JsonPropertyName("logo")]
    [XmlAttribute("logo")]
    public string Logo { get; set; }

    /// <summary>
    ///     This property contains the route prefix for the ReDoc documentation route
    /// </summary>
    [ConfigurationKeyName("routePrefix")]
    [JsonPropertyName("routePrefix")]
    [XmlAttribute("routePrefix")]
    public string RoutePrefix { get; set; } = string.Empty;

    /// <summary>
    ///     This property contains the URL to the terms of service for the application
    /// </summary>
    [ConfigurationKeyName("termsOfService")]
    [JsonPropertyName("termsOfService")]
    [XmlAttribute("termsOfService")]
    public string TermsOfService { get; set; }

    /// <summary>
    ///     This property contains the application title to be displayed for the documentation
    /// </summary>
    [ConfigurationKeyName("title")]
    [JsonPropertyName("title")]
    [XmlAttribute("title")]
    public string Title { get; set; }

    /// <summary>
    ///     This property contains the version of the API
    /// </summary>
    [ConfigurationKeyName("version")]
    [JsonPropertyName("version")]
    [XmlAttribute("version")]
    public string Version { get; set; }

    /// <summary>
    ///     This method returns the full path to the Swagger YAML file
    /// </summary>
    /// <returns>The full path to the Swagger YAML file</returns>
    public string GetFullPath() => Regex.Replace(GetPath($"/{GetPath()}/{GetVersion()}/swagger.json"), @"\/+", "/",
        RegexOptions.Compiled);

    /// <summary>
    ///     This method returns the full URL to the Swagger YAML file
    /// </summary>
    /// <param name="hostUrl">Optional, host URL override</param>
    /// <returns>The full URL to the Swagger YAML file</returns>
    public string GetFullUrl(Uri hostUrl = null) => GetUrl(hostUrl?.ToString() ?? HostUrl ?? Host, GetFullPath());

    /// <summary>
    ///     This method returns the full license url of the application
    /// </summary>
    /// <returns>The full URL to the application's license</returns>
    public Uri GetLicenseUrl() => License is DocumentationLicenseEnum.Proprietary ? null : License.ToUrl();

    /// <summary>
    ///     This method returns the full license url of the application as an OpenAPI schema
    /// </summary>
    /// <returns>The OpenAPI schema containing the full license url of the application</returns>
    public OpenApiLicense GetLicenseUrlOpenApi() => new() {Url = GetLicenseUrl()};

    /// <summary>
    ///     This method sanitizes a <paramref name="path" /> for URL generation
    /// </summary>
    /// <param name="path">The path to sanitize</param>
    /// <returns>The sanitized <paramref name="path" /></returns>
    public string GetPath(string path) =>
        $"/{Regex.Replace((path.StartsWith("/") ? path[1..] : path).Trim(), @"\/+", "/", RegexOptions.Compiled)}";

    /// <summary>
    ///     This method returns the path to the Swagger UI
    /// </summary>
    /// <returns>The path to the Swagger UI</returns>
    public string GetPath() => GetPath(Path);

    /// <summary>
    ///     This method returns the ReDoc UI index HTML for the application
    /// </summary>
    /// <returns>The ReDoc UI index HTML for the application</returns>
    public MemoryStream GetReDocIndex() =>
        !string.IsNullOrEmpty(DocumentationIndex) && !string.IsNullOrWhiteSpace(DocumentationIndex)
            ? new(File.Exists(DocumentationIndex)
                ? File.ReadAllBytes(DocumentationIndex)
                : Encoding.UTF8.GetBytes(DocumentationIndex))
            : new(Encoding.UTF8.GetBytes(ReDocIndexHtml));

    /// <summary>
    ///     This method returns a title for the application
    /// </summary>
    /// <returns>The application's title</returns>
    public string GetTitle() => Title ?? Assembly.GetExecutingAssembly().GetName().Name;

    /// <summary>
    ///     This method sanitizes the <paramref name="hostPart" /> and <paramref name="pathPart" /> then generates an absolute URL
    /// </summary>
    /// <param name="hostPart">The host and protocol to sanitize</param>
    /// <param name="pathPart">The path and query string to sanitize</param>
    /// <returns>The sanitized absolute URL: <code><paramref name="hostPart" />/<paramref name="pathPart" /></code></returns>
    public string GetUrl(string hostPart, string pathPart)
    {
        // Check the host part and sanitize it
        if (hostPart?.EndsWith("/") is true && hostPart.Contains("://") is not true) hostPart = hostPart[..^1];

        // Check the path part and sanitize it
        if (pathPart?.EndsWith("/") is true) pathPart = pathPart[..^1];

        // Check the path part again and sanitize it
        if (pathPart?.StartsWith("/") is true) pathPart = pathPart[1..];

        // Clean the path part
        if (pathPart is not null) pathPart = Regex.Replace(pathPart.Trim(), @"\/+", "/", RegexOptions.Compiled);

        // We're done, return the URL
        return hostPart is not null
            ? $"{hostPart.Trim()}/{pathPart?.Trim()}"
            : null;
    }

    /// <summary>
    ///     This method returns the full URL to the Swagger UI
    /// </summary>
    /// <param name="hostUrl">Optional, host URL override</param>
    /// <returns>The full URL to the Swagger UI</returns>
    public string GetUrl(Uri hostUrl = null) =>
        GetUrl(hostUrl?.ToString().Trim() ?? HostUrl?.Trim() ?? Host, GetPath());

    /// <summary>
    ///     This method returns a version for the application
    /// </summary>
    /// <returns>The version of the application</returns>
    public string GetVersion() => Version ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";
}
