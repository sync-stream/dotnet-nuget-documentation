using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Attribute;

// Define our namespace
namespace SyncStream.Documentation.Filters.Document;

/// <summary>
///     This class maintains the structure of our document filter
///     which provides Markdown loading for the describing endpoint groups/sections/tags
/// </summary>
public class MarkdownDocumentationDocumentFilter : IDocumentFilter
{
    /// <summary>
    ///     This method looks for documentation files for API sections
    /// </summary>
    /// <param name="document">The current api explorer document</param>
    /// <param name="context">The current api explorer context</param>
    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        // Define our list of tags that we've updated
        List<string> updatedTags = new();

        // Iterate over the API descriptions
        foreach (ApiDescription apiDescription in context.ApiDescriptions)
        {
            // Try to localize the method information
            if (apiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                // Localize the attribute on the method information
                MarkdownDocumentationAttribute attribute =
                    methodInfo.DeclaringType?.GetCustomAttribute<MarkdownDocumentationAttribute>(false);

                // Check for an attribute
                if (attribute != null)
                {
                    // Make sure we have a tag
                    if (string.IsNullOrEmpty(attribute.Tag) || string.IsNullOrWhiteSpace(attribute.Tag)) continue;

                    // Check for an existing tag
                    if (document.Tags.Any(t => t.Name.ToLower() == attribute.Tag.ToLower()))
                    {
                        // Iterate over the tags
                        foreach (OpenApiTag tag in document.Tags)
                        {
                            // Make sure we're on the right tag
                            if (tag.Name.ToLower() != attribute.Tag.ToLower()) continue;

                            // Reset the description on the tag
                            tag.Description = File.ReadAllText(attribute.PathIsAbsolute
                                ? attribute.Path
                                : Path.Combine(
                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                                    string.Empty,
                                    attribute.Path));
                        }
                    }

                    // Generate our new tag and add it to the document
                    else
                        document.Tags.Add(new()
                        {
                            // Read the description into the tag
                            Description = File.ReadAllText(attribute.PathIsAbsolute
                                ? attribute.Path
                                : Path.Combine(
                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                                    attribute.Path)),

                            // Set the name into the tag
                            Name = attribute.Tag
                        });
                }
            }
        }
    }
}
