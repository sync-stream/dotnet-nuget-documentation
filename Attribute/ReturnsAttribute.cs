// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains the structure of our Returns attribute
///     which allows the explicit defining of response types
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.ReturnValue)]
public class ReturnsAttribute : System.Attribute
{
    /// <summary>
    ///     This property contains the list of content types that are returned
    /// </summary>
    public readonly List<string> ContentTypes = new();

    /// <summary>
    ///     This property contains the HTTP status code the response is triggered on
    /// </summary>
    public readonly int StatusCode;

    /// <summary>
    ///     This property contains the type of the response body
    /// </summary>
    public readonly Type Type;

    /// <summary>
    ///     This method instantiates the attribute with a response body <paramref name="type" />,
    ///     <paramref name="httpStatusCode"/> as well as <paramref name="contentTypes" />
    /// </summary>
    /// <param name="type">The expected type of the response body</param>
    /// <param name="httpStatusCode">The expected HTTP status code</param>
    /// <param name="contentTypes">The expected content types</param>
    public ReturnsAttribute(Type type, int httpStatusCode, IEnumerable<string> contentTypes)
    {
        // Iterate over the content types and add them to the instance
        foreach (string contentType in contentTypes) ContentTypes.Add(contentType);

        // Set the HTTP status code into the response
        StatusCode = httpStatusCode;

        // Set the response body type into the instance
        Type = type;
    }

    /// <summary>
    ///     This method instantiates the attribute with a response body <paramref name="type" />,
    ///     <paramref name="httpStatusCode" /> as well as <paramref name="contentTypes" />
    /// </summary>
    /// <param name="type">The expected type of the response body</param>
    /// <param name="httpStatusCode">The expected HTTP status code</param>
    /// <param name="contentTypes">The expected content types</param>
    public ReturnsAttribute(Type type, int httpStatusCode, params string[] contentTypes)
    {
        // Iterate over the content types and add them to the instance
        foreach (string contentType in contentTypes) ContentTypes.Add(contentType);

        // Set the HTTP status code into the response
        StatusCode = httpStatusCode;

        // Set the response body type into the instance
        Type = type;
    }
}
