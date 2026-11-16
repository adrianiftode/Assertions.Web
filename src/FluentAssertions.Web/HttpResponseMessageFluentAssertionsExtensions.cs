// ReSharper disable once CheckNamespace
#if AAV
namespace AwesomeAssertions;
#elif FAV8 
namespace FluentAssertions;
#else
#endif

#if FAV8
/// <summary>
/// Contains extension methods for custom assertions in unit tests.
/// </summary>
[DebuggerNonUserCode]
public static class HttpResponseMessageFluentAssertionsExtensions
{
    /// <summary>
    /// Returns an <see cref="HttpResponseMessageAssertions"/> object that can be used to assert the
    /// current <see cref="HttpResponseMessage"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the entry point of the fluent assertions. Fluent assertion methods return an
    /// <c>AndConstraint&lt;TAssertions&gt;</c>; continue the chain from its <c>And</c> property.
    /// </para>
    /// <para>
    /// Start a new <c>response.Should()</c> call when the current chain cannot reach the assertion you need,
    /// for example when <c>HaveLocation()</c> is required after a <c>BadRequestAssertions</c> assertion.
    /// </para>
    /// </remarks>
    /// <returns>
    /// An <see cref="HttpResponseMessageAssertions"/> for the response. Status, content, header and
    /// <c>Satisfy</c> assertions can be composed from it.
    /// </returns>
    /// <seealso cref="HttpResponseMessageAssertions"/>
    /// <seealso cref="BadRequestAssertions"/>
    /// <seealso cref="HeadersAssertions"/>
    /// <seealso cref="LocationAssertions"/>
    public static HttpResponseMessageAssertions Should(this HttpResponseMessage? actual)
        => new HttpResponseMessageAssertions(actual, AssertionChain.GetOrCreate());
}
#endif