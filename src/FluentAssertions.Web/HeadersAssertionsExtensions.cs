#if !FAV8

namespace FluentAssertions;


/// <summary>
/// Contains extension methods for custom assertions in unit tests related to <see cref="HeadersAssertions"/>.
/// </summary>
/// <remarks>
/// <para>
/// In FluentAssertions versions before 8, these extensions provide the header assertions on the response assertion
/// returned by <c>response.Should()</c>. <c>HaveHeader</c> returns a <see cref="HeadersAssertions"/> bound to the
/// requested header; the chain continues from <c>And</c> with assertions on that header or on the response.
/// </para>
/// <para>
/// Call <c>HaveHeader</c> again to switch to another header. <c>NotHaveHeader</c> and <c>NotHaveLocation</c> return
/// the general <see cref="HttpResponseMessageAssertions"/> group instead.
/// </para>
/// </remarks>
/// <seealso cref="LocationAssertions"/>
/// <seealso cref="HttpResponseMessageAssertions"/>
[DebuggerNonUserCode]
public static class HeadersAssertionsExtensions
{
    /// <summary>
    /// Asserts that an HTTP response has a named header.
    /// </summary>
    /// <remarks>The header name is matched case-insensitively, as required by HTTP.</remarks>
    /// <param name="expectedHeader">
    /// The expected header with which the HTTP headers list is matched.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>
    /// A <see cref="HeadersAssertions"/> bound to <paramref name="expectedHeader"/>. Its header assertions and the
    /// general <see cref="HttpResponseMessageAssertions"/> assertions can be chained from <c>And</c>.
    /// </returns>
    /// <seealso cref="HeadersAssertions"/>
    [CustomAssertion]
    public static AndConstraint<HeadersAssertions> HaveHeader(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string expectedHeader,
        string because = "", params object[] becauseArgs)
    => new HttpResponseMessageAssertions(parent.Subject).HaveHeader(expectedHeader, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response does not have a named header.
    /// </summary>
    /// <remarks>The header name is matched case-insensitively, as required by HTTP.</remarks>
    /// <param name="expectedHeader">
    /// The expected header with which the HTTP headers list is matched.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>
    /// The general <see cref="HttpResponseMessageAssertions"/> group, because no header is selected to continue
    /// asserting.
    /// </returns>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> NotHaveHeader(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string expectedHeader,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).NotHaveHeader(expectedHeader, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response does not have a Location header.
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>
    /// The general <see cref="HttpResponseMessageAssertions"/> group, because no header is selected to continue
    /// asserting.
    /// </returns>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> NotHaveLocation(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).NotHaveLocation(because, becauseArgs);
}
#endif