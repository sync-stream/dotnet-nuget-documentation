using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Attribute;

// Define our namespace
namespace SyncStream.Documentation.Filters.Schema;

/// <summary>
///     This class maintains the schema filter structure of our ignored properties in the generated documentation
/// </summary>
public class DocumentationIgnoreSchemaFilter : ISchemaFilter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) => context.Type?.GetProperties()
        .Where(p => p.GetCustomAttribute<DocumentationIgnoreAttribute>(false) is not null).Select(p =>
            new List<string>(new[]
            {
                // Localize the property's name
                p.Name,

                // Localize the Microsoft json property name
                p.GetCustomAttribute<JsonPropertyNameAttribute>(false)?.Name,

                // Localize the Newtonsoft json property name
                p.GetCustomAttribute<Newtonsoft.Json.JsonPropertyAttribute>(false)?.PropertyName,

                // Localize the Microsoft xml array element name
                p.GetCustomAttribute<XmlArrayAttribute>(false)?.ElementName,

                // Localize the Microsoft xml array item element name
                p.GetCustomAttribute<XmlArrayItemAttribute>(false)?.ElementName,

                // Localize the Microsoft xml element name
                p.GetCustomAttribute<XmlElementAttribute>(false)?.ElementName
            }.Where(n => n is not null))).Where(n => n.Any(p => schema.Properties.ContainsKey(p))).ToList()
        .ForEach(n => n.ForEach(p => schema.Properties.Remove(p)));
}
