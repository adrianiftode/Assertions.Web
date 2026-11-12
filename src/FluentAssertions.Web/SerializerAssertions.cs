#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

public partial class HttpResponseMessageAssertions
{
    #region UsingSerializer

    /// <summary>
    /// Makes the assertions chained on this instance deserialize the response content with the given serializer,
    /// instead of <see cref="AssertionsWebConfig.Serializer"/>.
    /// </summary>
    /// <param name="serializer">The serializer to deserialize the response content with.</param>
    /// <returns>This assertions object.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serializer"/> is <c>null</c>.</exception>
    [CustomAssertion]
    public HttpResponseMessageAssertions UsingSerializer(ISerializer serializer)
    {
        Guard.ThrowIfArgumentIsNull(serializer, nameof(serializer), "Cannot use a <null> serializer.");

        _serializer = serializer;
        return this;
    }

    /// <summary>
    /// Makes the assertions chained on this instance deserialize the response content with System.Text.Json
    /// and the given options, instead of <see cref="AssertionsWebConfig.Serializer"/>.
    /// </summary>
    /// <param name="options">The options to deserialize the response content with.</param>
    /// <returns>This assertions object.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
    [CustomAssertion]
    public HttpResponseMessageAssertions UsingSerializer(JsonSerializerOptions options)
    {
        Guard.ThrowIfArgumentIsNull(options, nameof(options), "Cannot use <null> serializer options.");

        return UsingSerializer(new SystemTextJsonSerializer(options));
    }

    #endregion
}
