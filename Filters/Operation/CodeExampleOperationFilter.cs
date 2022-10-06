using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Attribute;

// Define our namespace
namespace SyncStream.Documentation.Filters.Operation;

/// <summary>
///     This class maintains the operation filter structure for
///     adding code examples to the Swagger documentation
/// </summary>
public class CodeExampleOperationFilter : IOperationFilter
{
    /// <summary>
    ///     This method applies our filter
    /// </summary>
    /// <param name="operation">The current operation</param>
    /// <param name="context">The current context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Define our code examples array
        OpenApiArray codeExamples = new();

        // Iterate over the attributes on the method
        foreach (CodeExampleAttribute attribute in context.MethodInfo.GetCustomAttributes<CodeExampleAttribute>(false))
        {
            // Instantiate our example object
            OpenApiObject codeExample = new()
            {
                // Set the language into the object
                ["lang"] = new OpenApiString(attribute.Language),

                // Set the source into the object
                ["source"] = new OpenApiString(attribute.PathIsAbsolute
                    ? File.ReadAllText(attribute.Path)
                    : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? String.Empty,
                        attribute.Path))
            };

            // Add the code example to the list
            codeExamples.Add(codeExample);
        }

        // Set the code samples into the documentation
        operation.Extensions["x-codeSamples"] = codeExamples;
    }
}
