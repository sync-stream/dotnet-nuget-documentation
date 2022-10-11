// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains our acceptable request payloads and types attribute
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.ReturnValue)]
public class HttpAcceptAttribute : System.Attribute
{
    /// <summary>
    ///     This property contains the content types that are accepted
    /// </summary>
    public readonly List<string> ContentTypes = new();

    /// <summary>
    ///     This property contains the request body type
    /// </summary>
    public readonly Type Type;

    /// <summary>
    ///     This method instantiates the attribute with an acceptable <paramref name="type" /> as well as valid <paramref name="contentTypes" />
    /// </summary>
    /// <param name="type">The type that is accepted by the endpoint</param>
    /// <param name="contentTypes">The content-type values the <paramref name="type" /> is valid for</param>
    public HttpAcceptAttribute(Type type, IEnumerable<string> contentTypes)
    {
        // Iterate over the content types and add them to the instance
        foreach (string contentType in contentTypes) ContentTypes.Add(contentType);

        // Set the type into the instance
        Type = type;
    }

    /// <summary>
    ///     This method instantiates the attribute with an acceptable <paramref name="type" /> as well as valid <paramref name="contentTypes" />
    /// </summary>
    /// <param name="type">The type that is accepted by the endpoint</param>
    /// <param name="contentTypes">The content-type values the <paramref name="type" /> is valid for</param>
    public HttpAcceptAttribute(Type type, params string[] contentTypes)
    {
        // Iterate over the content types and add them to the instance
        foreach (string contentType in contentTypes) ContentTypes.Add(contentType);

        // Set the type into the instance
        Type = type;
    }
}
