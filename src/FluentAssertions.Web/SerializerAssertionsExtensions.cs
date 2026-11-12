#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary>
/// Contains extension methods for choosing the serializer of the assertions on an Http Response Message.
/// </summary>
[DebuggerNonUserCode]
public static class SerializerAssertionsExtensions
{
    /// <summary>
    /// Makes the assertions chained on the result deserialize the response content with the given serializer,
    /// instead of <see cref="AssertionsWebConfig.Serializer"/>.
    /// </summary>
    /// <param name="serializer">The serializer to deserialize the response content with.</param>
    /// <returns>The assertions object to chain the assertions on.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serializer"/> is <c>null</c>.</exception>
    [CustomAssertion]
    public static HttpResponseMessageAssertions UsingSerializer(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        ISerializer serializer)
        => new HttpResponseMessageAssertions(parent.Subject).UsingSerializer(serializer);

    /// <summary>
    /// Makes the assertions chained on the result deserialize the response content with System.Text.Json
    /// and the given options, instead of <see cref="AssertionsWebConfig.Serializer"/>.
    /// </summary>
    /// <param name="options">The options to deserialize the response content with.</param>
    /// <returns>The assertions object to chain the assertions on.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
    [CustomAssertion]
    public static HttpResponseMessageAssertions UsingSerializer(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        JsonSerializerOptions options)
        => new HttpResponseMessageAssertions(parent.Subject).UsingSerializer(options);
}
#endif
