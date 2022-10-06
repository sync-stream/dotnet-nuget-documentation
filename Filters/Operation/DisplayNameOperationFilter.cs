using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// Define our namespace
namespace SyncStream.Documentation.Filters.Operation;

/// <summary>
///     This class maintains the Swagger endpoint documentation filter
///     for generating relative documentation to the request payload
/// </summary>
public class DisplayNameOperationFilter : IOperationFilter
{

    /// <summary>
    ///     This method applies the filter to method with an Accepts attribute
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check for an existing summary and set the description to it
        if (!string.IsNullOrEmpty(operation.Summary) && !string.IsNullOrWhiteSpace(operation.Summary))
            operation.Description = operation.Summary;

        // Reset the summary of the operation
        operation.Summary = $"/{context.ApiDescription.RelativePath}";
    }
}
