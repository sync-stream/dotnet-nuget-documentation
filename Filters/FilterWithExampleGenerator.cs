using Microsoft.OpenApi.Any;
using SyncStream.Documentation.Extensions;
using SyncStream.Documentation.Model;
using SyncStream.Serializer;

// Define our namespace
namespace SyncStream.Documentation.Filters;

/// <summary>
///     This class provides a base for filters that generate examples
/// </summary>
public abstract class FilterWithExampleGenerator
{
    /// <summary>
    ///     This method dynamically instantiates an example <paramref name="type" /> object
    /// </summary>
    /// <param name="type">The type to instantiate</param>
    /// <returns>The example object of <paramref name="type" /></returns>
    protected dynamic InstantiateObject(Type type)
    {
        // Instantiate our object
        dynamic objectInstance = Activator.CreateInstance(type);

        // Check for an example object provider
        if (type.IsSubclassOfGeneric(typeof(ExampleProvider<>).GetGenericTypeDefinition()) ||
            type.IsSubclassOfGeneric(typeof(ExampleProvider<>).GetGenericTypeDefinition()))
            return (objectInstance as IExampleProvider<dynamic>)?.GetExample();

        // We're done, return the object instance
        return objectInstance;
    }

    /// <summary>
    ///     This method generates a JSON example for a content item in the response
    /// </summary>
    /// <param name="type">The type to instantiate and generate an example of</param>
    /// <param name="instance">Existing instance of <paramref name="type" /> object</param>
    /// <returns>The JSON representation of <paramref name="type" /></returns>
    protected OpenApiString JsonExample(Type type, object instance = null) => new OpenApiString(
        SerializerService.SerializePretty(type, instance ?? InstantiateObject(type), SerializerFormat.Json));

    /// <summary>
    ///     This method generates an XML example for a content item in the response
    /// </summary>
    /// <param name="type">The type to instantiate and generate an example of</param>
    /// <param name="instance">Existing instance of <paramref name="type" /> object</param>
    /// <returns>The XML representation of <paramref name="type" /></returns>
    protected OpenApiString XmlExample(Type type, object instance = null) => new OpenApiString(
        SerializerService.SerializePretty(type, instance ?? InstantiateObject(type), SerializerFormat.Xml));
}
