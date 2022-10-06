// Define our namespace
namespace SyncStream.Documentation.Model;

/// <summary>
///     This class maintains the structure of an object with an example provider
/// </summary>
/// <typeparam name="TSource">The expected type of the example</typeparam>
public class ExampleProvider<TSource> : IExampleProvider<TSource> where TSource: class, new()
{
    /// <summary>
    ///     This method generates and returns an example object of <typeparamref name="TSource" />
    /// </summary>
    /// <returns>The example <typeparamref name="TSource" /> object</returns>
    public TSource GetExample() => new();

    /// <summary>
    ///     This method generates and returns an example object of <typeparamref name="TSource" />
    /// </summary>
    /// <returns>The example <typeparamref name="TSource" /> object</returns>
    public TSource GetExamples() => GetExample();
}
