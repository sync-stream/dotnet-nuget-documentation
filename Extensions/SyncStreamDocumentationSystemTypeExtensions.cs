// Define our namespace
namespace SyncStream.Documentation.Extensions;

/// <summary>
///     This class maintains the extension structure of our System.Type
/// </summary>
public static class SyncStreamDocumentationSystemTypeExtensions
{
    /// <summary>
    ///     This method determines whether or not a class inherits a generic class or not
    /// </summary>
    /// <param name="instance">The current type</param>
    /// <param name="target">The target type</param>
    /// <returns>A boolean denoting truth</returns>
    public static bool IsSubclassOfGeneric(this Type instance, Type target)
    {
        // Localize the source
        Type source = instance;

        // Iterate while we have a value
        while (source != null && source != typeof(object))
        {
            // Localize the current type
            Type current = source.IsGenericType ? source.GetGenericTypeDefinition() : source;

            // Check to see if the current type is the generic
            if (target == current) return true;

            // Reset the source
            source = source.BaseType;
        }

        // We're done, no match
        return false;
    }
}
