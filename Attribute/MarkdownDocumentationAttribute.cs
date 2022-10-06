// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains the structure of our Documentation Markdown attribute
///     which provides a way to tag a class or section to a tag name with documentation
///     contained in a Markdown file.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MarkdownDocumentationAttribute : System.Attribute
{
    /// <summary>
    ///     This property contains the path to the markdown file
    /// </summary>
    public readonly string Path;

    /// <summary>
    ///     This property denotes whether the path to the markdown file is absolute or not
    /// </summary>
    public bool PathIsAbsolute => Path[0].Equals(System.IO.Path.DirectorySeparatorChar)
                                  || Path.Contains("http://")
                                  || Path.Contains("https://");

    /// <summary>
    ///     This property contains the name of the tag to associate the description with
    /// </summary>
    public readonly string Tag;

    /// <summary>
    ///     This method instantiates our attribute with a tag name and path to the markdown file
    /// </summary>
    /// <param name="filePath">The file path to the markdown documentation</param>
    /// <param name="tagName">The tag name the description belongs to</param>
    public MarkdownDocumentationAttribute(string filePath, string tagName = null)
    {
        // Set the path into the instance
        Path = filePath;

        // Set the tag name into the instance
        Tag = tagName;
    }
}
