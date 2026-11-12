#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

/// <summary>
/// Contains a number of methods to assert that an <see cref="HttpResponseMessage"/> is in the expected state related to HTTP headers.
/// </summary>
public class LocationAssertions : HeadersAssertions
{
    /// <summary>
    /// Initialized a new instance of the <see cref="LocationAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
#if FAV8
    /// <param name="assertionChain">The assertion chain to build and manage assertions.</param>
    public LocationAssertions(HttpResponseMessage value, AssertionChain assertionChain) : base(value, "Location", assertionChain) { }
#else
    public LocationAssertions(HttpResponseMessage value) : base(value, "Location") { }
#endif

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