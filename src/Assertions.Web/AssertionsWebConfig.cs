using HttpMessageFormatter;
using System.Threading;

namespace Assertions.Web;

/// <summary>
/// Holder of the global serializer and response formatter settings used by every flavour:
/// FluentAssertions.Web, FluentAssertions.Web.v8, AwesomeAssertions.Web and Shouldly.Web.
/// </summary>
public static class AssertionsWebConfig
{
    private static ISerializer? _serializer;
    private static readonly AsyncLocal<ISerializer?> ScopedSerializer = new();
    private static HttpResponseFormatterOptions? _responseFormatterOptions;

    static AssertionsWebConfig()
    {
        Serializer = new SystemTextJsonSerializer();
        ResponseFormatterOptions = new HttpResponseFormatterOptions();
    }

    /// <summary>
    /// The serializer instance used to deserialize the responses into a model of a specified typed.
    /// Reading it inside a <see cref="UseSerializer"/> scope returns the scoped serializer; setting it always changes the global one.
    /// </summary>
    public static ISerializer Serializer
    {
        get => ScopedSerializer.Value ?? _serializer ?? throw new InvalidOperationException("Serializer cannot be null");

        set => _serializer = value ?? throw new ArgumentNullException(nameof(value), "Serializer cannot be null.");
    }

    /// <summary>
    /// Makes <paramref name="serializer"/> the serializer of the current async flow until the returned scope is disposed,
    /// without changing the global <see cref="Serializer"/>. Scopes nest: disposing restores the previously scoped serializer.
    /// </summary>
    /// <param name="serializer">The serializer to use inside the scope.</param>
    /// <returns>A scope that restores the previous serializer when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serializer"/> is <c>null</c>.</exception>
    public static IDisposable UseSerializer(ISerializer serializer)
    {
        if (serializer is null)
        {
            throw new ArgumentNullException(nameof(serializer), "Cannot use a <null> serializer.");
        }

        var previous = ScopedSerializer.Value;
        ScopedSerializer.Value = serializer;
        return new SerializerScope(previous);
    }

    private sealed class SerializerScope : IDisposable
    {
        private readonly ISerializer? _previous;

        public SerializerScope(ISerializer? previous) => _previous = previous;

        public void Dispose() => ScopedSerializer.Value = _previous;
    }

    /// <summary>
    /// The options used when formatting an HTTP response message into a readable string,
    /// for example in the assertion failure messages.
    /// They control, for instance, how many characters of the content are printed before the content
    /// is considered too large. These options do not change the assertions themselves, only the way
    /// the HTTP messages are formatted.
    /// </summary>
    public static HttpResponseFormatterOptions ResponseFormatterOptions
    {
        get => _responseFormatterOptions ?? throw new InvalidOperationException("ResponseFormatterOptions cannot be null");

        set => _responseFormatterOptions = value ?? throw new ArgumentNullException(nameof(value), "ResponseFormatterOptions cannot be null.");
    }
}
