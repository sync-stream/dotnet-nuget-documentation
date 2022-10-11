using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Attribute;

// Define our namespace
namespace SyncStream.Documentation.Filters.Schema;

/// <summary>
///     This class maintains the structure for our conditional requirement attribute
/// </summary>
public class RequiredIfSchemaFilter : ISchemaFilter
{
    /// <summary>
    ///     This method applies the filter to the <paramref name="schema" />
    /// </summary>
    /// <param name="schema">The schema to apply this filter to</param>
    /// <param name="context">The current OpenAPI SchemaFilterContext instance</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Iterate over the properties
        foreach (PropertyInfo property in context.Type.GetProperties())
        {
            // Localize the parameter
            string parameter = schema.Properties.Where(p => p.Key.ToLower().Equals(property.Name.ToLower()))
                .Select(p => p.Key).FirstOrDefault();

            // Make sure we have a parameter
            if (string.IsNullOrEmpty(parameter)) continue;

            // Localize the RequiredIf attributes
            List<RequiredIfAttribute> attributes =
                property.GetCustomAttributes<RequiredIfAttribute>(false).ToList();

            // Iterate over the attributes and update the required field
            foreach (RequiredIfAttribute attribute in attributes)
            {
                // Localize the target
                string target = schema.Properties.Where(p => p.Key.ToLower().Equals(attribute.PropertyName.ToLower()))
                    .Select(p => p.Key).FirstOrDefault();

                // Check for a reference to an object and update the object's description
                if (!string.IsNullOrEmpty(schema.Properties[parameter].Reference?.Id))
                    context.SchemaRepository.Schemas[schema.Properties[parameter].Reference.Id].Description =
                        $"{context.SchemaRepository.Schemas[schema.Properties[parameter].Reference.Id].Description}{attribute.GenerateDescription(parameter, target)}";

                // Otherwise update the schema description
                else
                    schema.Properties[parameter].Description =
                        $"{schema.Properties[parameter].Description}{attribute.GenerateDescription(parameter, target)}";
            }
        }
    }
}
