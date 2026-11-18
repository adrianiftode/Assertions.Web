#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

/// <summary>
/// Contains a number of methods to assert that an <see cref="HttpResponseMessage"/> is in the expected state related to the HTTP <c>Location</c> header.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="LocationAssertions"/> is a <see cref="HeadersAssertions"/> whose header is fixed to <c>Location</c>.
/// It is returned by some status assertions, such as <see cref="HttpResponseMessageAssertions.Be201Created"/>
/// and <see cref="HttpResponseMessageAssertions.Be3XXRedirection"/>. Returning this group does not assert that the
/// header is present; use <see cref="HaveLocation"/> or one of the inherited header assertions to check it.
/// </para>
/// <para>
/// <see cref="HaveLocation"/> checks that the header exists and returns <see cref="HeadersAssertions"/>, so the
/// header-value assertions can follow. The inherited header assertions are also available directly from <c>And</c>
/// because this group derives from <see cref="HeadersAssertions"/>. After that transition this group's own
/// <see cref="HaveLocation"/> is no longer available; start a new chain with a location-bearing status if it is
/// needed again.
/// </para>
/// <para>
/// A chain that is currently on <see cref="BadRequestAssertions"/> cannot reach this group. To use
/// <see cref="HaveLocation"/>, start a new <c>response.Should()</c> call with a status assertion that returns
/// <see cref="LocationAssertions"/>. In FluentAssertions versions before 8, the same check is also available as a
/// <c>HaveLocation()</c> extension method on the initial response assertion.
/// </para>
/// </remarks>
/// <seealso cref="HeadersAssertions"/>
/// <seealso cref="HttpResponseMessageAssertions"/>
public class LocationAssertions : HeadersAssertions
{
#if FAV8
    /// <summary>
    /// Initialized a new instance of the <see cref="LocationAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
    /// <param name="assertionChain">The assertion chain to build and manage assertions.</param>
    public LocationAssertions(HttpResponseMessage value, AssertionChain assertionChain) : base(value, "Location", assertionChain) { }
#else
    /// <summary>
    /// Initialized a new instance of the <see cref="LocationAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
    public LocationAssertions(HttpResponseMessage value) : base(value, "Location") { }
#endif

    /// <summary>
    /// Asserts that an HTTP response has a Location header.
    /// </summary>
    /// <remarks>The HTTP field name is matched case-insensitively, as required by HTTP.</remarks>
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
    /// <seealso cref="HttpResponseMessageAssertions.HaveHeader"/>
    [CustomAssertion]
    public AndConstraint<HeadersAssertions> HaveLocation(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(IsHeaderPresent(Header))
            .FailWith("Expected {context:response} to contain " +
                      "the Location HTTP header, but no such header was found in the actual response{reason}.{0}",
                Subject);
#if FAV8
        return new AndConstraint<HeadersAssertions>(new HeadersAssertions(Subject, Header, CurrentAssertionChain));
#else
        return new AndConstraint<HeadersAssertions>(new HeadersAssertions(Subject, Header));
#endif
    }

    /// <summary>
    /// Assertion identifier for the <see cref="LocationAssertions"/> class.
    /// </summary>
    protected override string Identifier => "Header.Location";
}