#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

public partial class HttpResponseMessageAssertions
{
    /// <summary>
    /// Asserts that an HTTP response satisfies an assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion about the HTTP response.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. It runs against the response itself; no deserialization occurs.
    /// Failures are reported with the complete request/response conversation. Assertions.Web preserves that context
    /// across the library, but it is especially valuable here because arbitrary test code stays inside the HTTP
    /// assertion. The assertion can be a single assertion or a collection of assertions if it is expressed as a
    /// statement lambda.
    /// </remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Satisfy(Action<HttpResponseMessage> assertion,
        string because = "", params object[] becauseArgs)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion), "Cannot verify the subject satisfies a `null` assertion.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

        var failuresFromAssertions = CollectFailuresFromAssertion(assertion!, Subject);

        if (failuresFromAssertions.Any())
        {
    #if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
                .BecauseOf(because, becauseArgs)
                .FailWith(
                    "Expected {context:response} to satisfy one or more assertions, but it wasn't{reason}: {0}{1}",
                    new AssertionsFailures(failuresFromAssertions), Subject);
        }

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response satisfies an asynchronous assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion about the HTTP response.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. It runs against the response itself; no deserialization occurs.
    /// Failures are reported with the complete request/response conversation. Assertions.Web preserves that context
    /// across the library, but it is especially valuable here because arbitrary test code stays inside the HTTP
    /// assertion. The assertion can be a single assertion or a collection of assertions if it is expressed as a
    /// statement lambda.
    /// </remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Satisfy(Func<HttpResponseMessage, Task> assertion,
        string because = "", params object[] becauseArgs)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion), "Cannot verify the subject satisfies a `null` assertion.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

        var failuresFromAssertions = CollectFailuresFromAssertion(asserted =>
        {
            Func<Task> assertionExecutor = () => assertion(asserted!);
            assertionExecutor.ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult();
        }, Subject);

        if (failuresFromAssertions.Any())
        {
    #if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
                .BecauseOf(because, becauseArgs)
                .FailWith(
                    "Expected {context:response} to satisfy one or more assertions, but it wasn't{reason}: {0}{1}",
                    new AssertionsFailures(failuresFromAssertions), Subject);
        }

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
}
