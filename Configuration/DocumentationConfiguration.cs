using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

// Define our namespace
namespace SyncStream.Documentation.Configuration;

/// <summary>
///     This class maintains the configuration values for the SyncStream.Documentation library
/// </summary>
[XmlRoot("documentationConfiguration")]
public class DocumentationConfiguration
{
    /// <summary>
    ///     This property contains the software license
    /// </summary>
    [ConfigurationKeyName("license")]
    [JsonPropertyName("license")]
    [XmlAttribute("license")]
    public DocumentationLicense? License { get; set; }

    /// <summary>
    ///     This property contains the URL or file path to the URL
    /// </summary>
    [ConfigurationKeyName("logo")]
    [JsonPropertyName("logo")]
    [XmlAttribute("logo")]
    public string Logo { get; set; }

    /// <summary>
    ///     This property contains the path Swagger/ReDoc will be accessible at
    /// </summary>
    [ConfigurationKeyName("path")]
    [JsonPropertyName("path")]
    [XmlText]
    public string Path { get; set; } = "/swagger/${SWAGGER_VERSION}/swagger.json";

    /// <summary>
    ///     This property contains the URL to the terms of service for the application
    /// </summary>
    [ConfigurationKeyName("termsOfService")]
    [JsonPropertyName("termsOfService")]
    [XmlElement("termsOfService")]
    public Uri TermsOfService { get; set; }

    /// <summary>
    ///     This property contains the application title to be displayed for the documentation
    /// </summary>
    [ConfigurationKeyName("title")]
    [JsonPropertyName("title")]
    [XmlAttribute("title")]
    public string Title { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

    /// <summary>
    ///     This property contains the version of the API
    /// </summary>
    [ConfigurationKeyName("version")]
    [JsonPropertyName("version")]
    [XmlAttribute("version")]
    public string Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    /// <summary>
    ///     This method returns the full path Swagger/ReDoc will be accessible at
    /// </summary>
    /// <returns>The full path Swagger/ReDoc will be accessible at</returns>
    public string GetFullPath() => Path.Replace("${SWAGGER_VERSION}", Version);
}
