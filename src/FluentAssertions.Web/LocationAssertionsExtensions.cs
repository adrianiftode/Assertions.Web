#if !FAV8

namespace FluentAssertions;


/// <summary>
/// Contains extension methods for custom assertions in unit tests related to <see cref="LocationAssertions"/>.
/// </summary>
/// <remarks>
/// <para>
/// In FluentAssertions versions before 8, this extension provides <c>HaveLocation</c> directly on the response
/// assertion returned by <c>response.Should()</c>. It returns a <see cref="HeadersAssertions"/> bound to the
/// <c>Location</c> header, so header-value assertions can be chained from <c>And</c>.
/// </para>
/// <para>
/// Some status assertions already return <see cref="LocationAssertions"/>, which offers the same
/// check without starting a new chain. <c>HaveLocation</c> is not available from a <see cref="BadRequestAssertions"/>
/// chain.
/// </para>
/// </remarks>
/// <seealso cref="HeadersAssertions"/>
/// <seealso cref="HttpResponseMessageAssertions"/>
[DebuggerNonUserCode]
public static class LocationAssertionsExtensions
{
    /// <summary>
    /// Asserts that an HTTP response has a Location header.
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>
    /// A <see cref="HeadersAssertions"/> bound to the <c>Location</c> header, so header-value assertions can follow
    /// from <c>And</c>. The general <see cref="HttpResponseMessageAssertions"/> assertions also remain available.
    /// </returns>
    /// <seealso cref="HeadersAssertions"/>
    [CustomAssertion]
    public static AndConstraint<HeadersAssertions> HaveLocation(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
    => new LocationAssertions(parent.Subject).HaveLocation(because, becauseArgs);
}
#endif