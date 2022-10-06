using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Attribute;

// Define our namespace
namespace SyncStream.Documentation.Filters.Operation;

/// <summary>
/// This class maintains the structure of our markdown documentation filter
/// which checks operation descriptions for file notation and then loads the
/// markdown file into the operation's description
/// </summary>
public class MarkdownDocumentationOperationFilter : IOperationFilter
{
    /// <summary>
    ///     This method applies the operation
    /// </summary>
    /// <param name="operation">The current api-explorer operation</param>
    /// <param name="context">The current api-explorer filter context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Localize the attribute
        MarkdownDocumentationAttribute attribute =
            context.MethodInfo.GetCustomAttribute<MarkdownDocumentationAttribute>(false);

        // Check for an attribute and return
        if (attribute == null) return;

        // Reset the description of the operation
        operation.Description += "\n" + File.ReadAllText(attribute.PathIsAbsolute
            ? attribute.Path
            : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? String.Empty,
                attribute.Path));
    }
}
