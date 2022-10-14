using System.Reflection;
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
        .Where(p => p.GetCustomAttribute<DocumentationIgnoreAttribute>(false) is not null)
        .ToList().ForEach(p =>
        {
            // Localize the parameter
            string parameter = schema.Properties.Where(i => i.Key.ToLower().Equals(p.Name.ToLower())).Select(i => i.Key)
                .FirstOrDefault();

            // Make sure we have a parameter
            if (!string.IsNullOrEmpty(parameter)) schema.Properties.Remove(parameter);
        });
}
