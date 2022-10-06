// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains the structure of our code example documentation attribute
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public abstract class CodeExampleAttribute : System.Attribute
{
    /// <summary>
    ///     This property contains the tag name to use
    /// </summary>
    public readonly string Language;

    /// <summary>
    ///     This property contains the file path to the code example
    /// </summary>
    public readonly string Path;

    /// <summary>
    ///     This property denotes whether or not the path is an absolute path
    /// </summary>
    public bool PathIsAbsolute => Path.Length > 0 && Path[0].Equals(System.IO.Path.DirectorySeparatorChar);

    /// <summary>
    ///     This method instantiates our attribute with a language and file path
    /// </summary>
    /// <param name="language">The language the example is written in</param>
    /// <param name="filePath">The path to the file</param>
    protected CodeExampleAttribute(string language, string filePath)
    {
        // Set the language into the instance
        Language = language;

        // Set the file path into the instance
        Path = filePath;
    }
}
