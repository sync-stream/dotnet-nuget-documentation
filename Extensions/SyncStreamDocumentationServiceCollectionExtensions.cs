using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Documentation.Configuration;
using SyncStream.Documentation.Filters.Document;
using SyncStream.Documentation.Filters.Operation;
using SyncStream.Documentation.Filters.Schema;
using SwaggerGenOptionsExtensions = Swashbuckle.AspNetCore.Filters.SwaggerGenOptionsExtensions;

// Define our namespace
namespace SyncStream.Documentation.Extensions;

/// <summary>
///     This class maintains the structure of our IServiceCollection extensions for SyncStream.Documentation
/// </summary>
public static class SyncStreamDocumentationServiceCollectionExtensions
{
    /// <summary>
    ///     This property contains any custom IDocumentFilter filters for the documentation generation
    /// </summary>
    private static readonly List<Tuple<Type, object[]>> DocumentFilters = new();

    /// <summary>
    ///     This property contains any custom IOperationFilter filters for the documentation generation
    /// </summary>
    private static readonly List<Tuple<Type, object[]>> OperationFilters = new();

    /// <summary>
    ///     This property contains any custom IParameterFilter filters for the documentation generation
    /// </summary>
    private static readonly List<Tuple<Type, object[]>> ParameterFilters = new();

    /// <summary>
    ///     This property contains any custom IRequestBodyFilter filters for the documentation generation
    /// </summary>
    private static readonly List<Tuple<Type, object[]>> RequestBodyFilters = new();

    /// <summary>
    ///     This property contains any custom ISchemaFilter filters for the documentation generation
    /// </summary>
    private static readonly List<Tuple<Type, object[]>> SchemaFilters = new();

    /// <summary>
    ///     This method calls a generic filter method for adding a custom filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current instance of the SwaggerGenOptions</param>
    /// <param name="method">The filter method to call</param>
    /// <param name="type">The type of the filter to add</param>
    /// <param name="arguments">The arguments to pass to the filter's constructor</param>
    private static void
        CallFilterMethod(this SwaggerGenOptions instance, string method, Type type, object[] arguments) =>
        typeof(SwaggerGenOptionsExtensions).GetMethod(method, BindingFlags.Public | BindingFlags.Static)
            ?.MakeGenericMethod(type).Invoke(instance, arguments);

    /// <summary>
    ///     This method adds a custom IDocumentFilter filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="arguments">Arguments to send to the filter's constructor</param>
    /// <typeparam name="TDocumentFilter">The type of the IDocumentFilter filter to add</typeparam>
    /// <returns>The current IServiceCollection <paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationDocumentFilter<TDocumentFilter>(
        this IServiceCollection instance, params object[] arguments) where TDocumentFilter : IDocumentFilter
    {
        // Add the filter to the instance
        DocumentFilters.Add(new(typeof(TDocumentFilter), arguments));

        // We're done, return the current IServiceCollection instance
        return instance;
    }

    /// <summary>
    ///     This method configures the <paramref name="instance" /> with a documentation example for <typeparamref name="TExample" />
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <typeparam name="TExample">The expected type of the example to use</typeparam>
    /// <returns><paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationExample<TExample>(this IServiceCollection instance)
        where TExample : IExamplesProvider<TExample> => instance.AddSwaggerExamplesFromAssemblyOf<TExample>();

    /// <summary>
    ///     This method adds a custom IOperationFilter filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="arguments">Arguments to send to the filter's constructor</param>
    /// <typeparam name="TOperationFilter">The type of the IOperationFilter filter to add</typeparam>
    /// <returns>The current IServiceCollection <paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationOperationFilter<TOperationFilter>(
        this IServiceCollection instance, params object[] arguments) where TOperationFilter : IOperationFilter
    {
        // Add the filter to the instance
        OperationFilters.Add(new(typeof(TOperationFilter), arguments));

        // We're done, return the current IServiceCollection instance
        return instance;
    }

    /// <summary>
    ///     This method adds a custom IParameterFilter filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="arguments">Arguments to send to the filter's constructor</param>
    /// <typeparam name="TParameterFilter">The type of the IParameterFilter filter to add</typeparam>
    /// <returns>The current IServiceCollection <paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationParameterFilter<TParameterFilter>(
        this IServiceCollection instance, params object[] arguments) where TParameterFilter : IParameterFilter
    {
        // Add the filter to the instance
        ParameterFilters.Add(new(typeof(TParameterFilter), arguments));

        // We're done, return the current IServiceCollection instance
        return instance;
    }

    /// <summary>
    ///     This method adds a custom IRequestBodyFilter filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="arguments">Arguments to send to the filter's constructor</param>
    /// <typeparam name="TRequestBodyFilter">The type of the IRequestBodyFilter filter to add</typeparam>
    /// <returns>The current IServiceCollection <paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationRequestBodyFilter<TRequestBodyFilter>(
        this IServiceCollection instance, params object[] arguments) where TRequestBodyFilter : IRequestBodyFilter
    {
        // Add the filter to the instance
        RequestBodyFilters.Add(new(typeof(TRequestBodyFilter), arguments));

        // We're done, return the current IServiceCollection instance
        return instance;
    }

    /// <summary>
    ///     This method adds a custom ISchemaFilter filter to the documentation generation
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="arguments">Arguments to send to the filter's constructor</param>
    /// <typeparam name="TSchemaFilter">The type of the ISchemaFilter filter to add</typeparam>
    /// <returns>The current IServiceCollection <paramref name="instance" /></returns>
    public static IServiceCollection AddSyncStreamDocumentationSchemaFilter<TSchemaFilter>(
        this IServiceCollection instance, params object[] arguments) where TSchemaFilter : ISchemaFilter
    {
        // Add the filter to the instance
        SchemaFilters.Add(new(typeof(TSchemaFilter), arguments));

        // We're done, return the current IServiceCollection instance
        return instance;
    }

    /// <summary>
    ///     This method configures the <paramref name="instance" /> with Swagger and ReDoc
    /// </summary>
    /// <param name="instance">The current IServiceCollection instance</param>
    /// <param name="configuration">The documentation configuration values</param>
    /// <returns><paramref name="instance" /></returns>
    public static IServiceCollection UseSyncStreamDocumentation(this IServiceCollection instance,
        DocumentationConfiguration configuration)
    {
        // Add Swagger to the IServiceCollection instance
        instance.AddSwaggerGen(options =>
        {
            // Describe all parameters in camel case
            options.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;

            // Enable annotations
            options.EnableAnnotations(true, true);

            // Enable example filters
            options.ExampleFilters();

            // We may want to include the XML documentation
            if (configuration.IncludeXmlComments)
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

            // Add our markdown documentation document filter
            options.DocumentFilter<MarkdownDocumentationDocumentFilter>();

            // Add our code example documentation operation filter
            options.OperationFilter<CodeExampleOperationFilter>();

            // Add our display name operation filter
            options.OperationFilter<DisplayNameOperationFilter>();

            // Add our markdown documentation operation filter
            options.OperationFilter<MarkdownDocumentationOperationFilter>();

            // Add our explicit response definition operation filter
            options.OperationFilter<ReturnsOperationFilter>();

            // Add our documentation-ignore schema filter
            options.SchemaFilter<DocumentationIgnoreSchemaFilter>();

            // Add our required-if schema filter
            options.SchemaFilter<RequiredIfSchemaFilter>();

            // Add our required-if-in schema filter
            options.SchemaFilter<RequiredIfInSchemaFilter>();

            // Add our required-if-NOT-NULL-or-empty schema filter
            options.SchemaFilter<RequiredIfNotNullOrEmptySchemaFilter>();

            // Add our required-if-NULL schema filter
            options.SchemaFilter<RequiredIfNullOrEmptySchemaFilter>();

            // Iterate over any custom document filters
            DocumentFilters.ForEach(f => CallFilterMethod(options, "DocumentFilter", f.Item1, f.Item2));

            // Iterate over any custom operation filters
            OperationFilters.ForEach(f => CallFilterMethod(options, "OperationFilter", f.Item1, f.Item2));

            // Iterate over any custom parameter filters
            ParameterFilters.ForEach(f => CallFilterMethod(options, "ParameterFilter", f.Item1, f.Item2));

            // Iterate over any custom request-body filters
            RequestBodyFilters.ForEach(f => CallFilterMethod(options, "RequestBodyFilter", f.Item1, f.Item2));

            // Iterate over any custom schema filters
            SchemaFilters.ForEach(f => CallFilterMethod(options, "SchemaFilter", f.Item1, f.Item2));

            // Define our extensions
            Dictionary<string, IOpenApiExtension> extensions = new();

            // Check for a logo
            if (!string.IsNullOrEmpty(configuration.Logo) && !string.IsNullOrWhiteSpace(configuration.Logo))
            {
                // Define our extension object
                OpenApiObject logoExtension = new()
                {
                    // Add our logo URL to the configuration
                    { "url", new OpenApiString(configuration.Logo) },

                    // Add our application title to the alternate text
                    { "altText", new OpenApiString(configuration.Title) }
                };

                // Add the logo extension to the dictionary
                extensions.Add("x-logo", logoExtension);
            }

            // Define our open api information
            OpenApiInfo apiInfo = new();

            // Define our readme file path
            string readmeFile =
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                    "readme.md");

            // Check to see if a readme.md file exists then set the description into the open api information object
            if (File.Exists(readmeFile))
                apiInfo.Description = File
                    .ReadAllText(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "readme.md"))
                    .Replace("${SWAGGER_PATH}", configuration.GetFullPath(), StringComparison.CurrentCultureIgnoreCase)
                    .Replace("${SWAGGER_URL}", configuration.GetFullUrl(), StringComparison.CurrentCultureIgnoreCase);

            // Set the license into the open api information object
            apiInfo.License = configuration.GetLicenseUrlOpenApi();

            // Set our extensions into the open api information object
            apiInfo.Extensions = extensions;

            // Set our application terms-of-service into the open api information object
            apiInfo.TermsOfService = configuration.TermsOfService;

            // Set our application title into the open api information object
            apiInfo.Title = configuration.GetTitle();

            // Set our application version into the open api information object
            apiInfo.Version = configuration.GetVersion();

            // Define our swagger specification document
            options.SwaggerDoc(configuration.GetVersion(), apiInfo);
        }).AddSwaggerExamples();

        // We're done, return the current IServiceCollection instance
        return instance;
    }
}
