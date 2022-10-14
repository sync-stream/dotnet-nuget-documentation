// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains the attribute structure of a property that should be ignored in the documentation
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DocumentationIgnoreAttribute : System.Attribute { }
