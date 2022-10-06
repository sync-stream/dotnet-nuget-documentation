using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Documentation.Model;

/// <summary>
///     This interface maintains the structure of an object with an example provider
/// </summary>
/// <typeparam name="TSource">The expected type of the example</typeparam>
public interface IExampleProvider<TSource> : IExamplesProvider<TSource>
{
    /// <summary>
    ///     This method generates and returns an example object of <typeparamref name="TSource" />
    /// </summary>
    /// <returns>The example <typeparamref name="TSource" /> object</returns>
    public TSource GetExample();

    /// <summary>
    ///     This method generates and returns an example object of <typeparamref name="TSource" />
    /// </summary>
    /// <returns>The example <typeparamref name="TSource" /> object</returns>
    public new TSource GetExamples() => GetExample();
}
