using System.Net.Mime;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// Define our namespace
namespace SyncStream.Documentation.Filters.Operation;

/// <summary>
///     This class maintains the Swagger endpoint documentation filter
///     for generating relative documentation to the response payload
/// </summary>
public class ReturnsOperationFilter : FilterWithExampleGenerator, IOperationFilter
{
    /// <summary>
    ///     This method processes a content item into the responses of the endpoint
    /// </summary>
    /// <param name="contentType">The available content-type for the response</param>
    /// <param name="type">The object type of the response</param>
    /// <param name="context">The current filter context</param>
    /// <returns>The OpenAPI media type definition</returns>
    private OpenApiMediaType ProcessResponse(string contentType, Type type, OperationFilterContext context)
    {
        // Define our response
        OpenApiMediaType response = new OpenApiMediaType();

        // Set the serialized example into the the response object
        response.Example = contentType.ToLower().Contains("xml") ? XmlExample(type) : JsonExample(type);

        // Try to lookup our schema and set it into the the response object
        if (context.SchemaRepository.TryLookupByType(type, out OpenApiSchema schema)) response.Schema = schema;

        // We're done, send the response
        return response;
    }

    /// <summary>
    ///     This method processes the responses from the Return attribute classes
    /// </summary>
    /// <param name="attribute">The attribute explicitly defining the responses</param>
    /// <param name="responses">The current OpenAPI response objects</param>
    /// <param name="context">The current OpenAPI filter context</param>
    private void ProcessResponse(Attribute.ReturnsAttribute attribute, OpenApiResponses responses,
        OperationFilterContext context)
    {
        // Define our OpenAPI Response object
        OpenApiResponse response = new OpenApiResponse();

        // Iterate over attribute content types
        attribute.ContentTypes.ForEach(c => response.Content.Add(c, ProcessResponse(c, attribute.Type, context)));

        // Add the response to the response list
        responses.Add(attribute.StatusCode.ToString(), response);
    }

    /// <summary>
    ///     This method applies our filter in the <paramref name="context" /> to the <paramref name="operation" />
    /// </summary>
    /// <param name="operation">The OpenAPI operation to apply the filter to</param>
    /// <param name="context">The current OpenAPI filter context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Localize the list of attributes
        List<Attribute.ReturnsAttribute> returnAttributes =
            context.MethodInfo.GetCustomAttributes<Attribute.ReturnsAttribute>(false).ToList();

        // Define our list of content types
        List<string> contentTypes = new List<string>();

        // Check for any return attributes
        if (returnAttributes.Any())
        {
            // Clear the list of responses
            operation.Responses.Clear();

            // Iterate over the response types and process them into the Swagger engine
            returnAttributes.ForEach(a =>
            {
                // Add the content types to the list
                contentTypes.AddRange(a.ContentTypes);

                // Process the response
                ProcessResponse(a, operation.Responses, context);
            });
        }

        // Check for content types
        if (!contentTypes.Any())
        {
            // Add our JSON content type
            contentTypes.Add(MediaTypeNames.Application.Json);

            // Add our XML content type
            contentTypes.Add(MediaTypeNames.Application.Xml);
        }
    }
}
