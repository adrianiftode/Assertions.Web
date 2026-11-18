#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

/// <summary>
/// Contains a number of methods to assert that an <see cref="HttpResponseMessage"/> is in the expected state related to the HTTP content.
/// </summary>
public partial class HttpResponseMessageAssertions
{
    /// <summary>
    /// Asserts that the HTTP content is empty.
    /// </summary>
    /// <remarks>
    /// This method targets the response body. When the current chain is a header group, <c>BeEmpty</c> instead
    /// checks the values of the selected header. Start a new <c>response.Should()</c> chain to reach this
    /// response-content assertion after header checks.
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
    public AndConstraint<HttpResponseMessageAssertions> BeEmpty(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

        var content = GetContent();

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(string.IsNullOrEmpty(content))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:response} to have no content. {0}", Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that HTTP response content can be an equivalent representation of the expected model.
    /// </summary>
    /// <remarks>
    /// Use this assertion when the complete expected body is known. The response is deserialized into the runtime type
    /// of <paramref name="expectedModel"/> and the full object graph is compared with the assertion framework's
    /// equivalency engine. Use <c>Satisfy&lt;T&gt;()</c> for custom assertions over a deserialized model. A body that
    /// cannot be deserialized is reported as a JSON parsing failure; differences found after successful deserialization
    /// are reported as equivalency differences. Equivalency options are passed to the installed assertion framework,
    /// so its configuration affects the comparison. JSON property-name matching follows the configured serializer,
    /// while string values inside the model use the framework's equivalency defaults and are case-sensitive unless
    /// the options say otherwise.
    /// </remarks>
    /// <param name="expectedModel">
    /// The expected model.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> BeAs<TModel>(TModel expectedModel, string because = "", params object[] becauseArgs)
        => BeAs(expectedModel, options => options, because, becauseArgs);

    /// <summary>
    /// Asserts that HTTP response content can be an equivalent representation of the expected model.
    /// </summary>
    /// <remarks>
    /// Use this assertion when the complete expected body is known. The response is deserialized into the runtime type
    /// of <paramref name="expectedModel"/> and the full object graph is compared with the assertion framework's
    /// equivalency engine. Use <c>Satisfy&lt;T&gt;()</c> for custom assertions over a deserialized model. A body that
    /// cannot be deserialized is reported as a JSON parsing failure; differences found after successful deserialization
    /// are reported as equivalency differences. Equivalency options are passed to the installed assertion framework,
    /// so its configuration affects the comparison. JSON property-name matching follows the configured serializer,
    /// while string values inside the model use the framework's equivalency defaults and are case-sensitive unless
    /// the options say otherwise.
    /// </remarks>
    /// <param name="expectedModel">
    /// The expected model.
    /// </param>
    /// <param name="options">
    /// A reference to the equivalency-options configuration object that can be used to influence the way the object
    /// graphs are compared. You can also provide an alternative instance of that class. The global defaults are
    /// determined by the assertion framework configuration.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
#if FAV8
    public AndConstraint<HttpResponseMessageAssertions> BeAs<TModel>(TModel expectedModel, Func<EquivalencyOptions<TModel>, EquivalencyOptions<TModel>> options, string because = "", params object[] becauseArgs)
#else
    public AndConstraint<HttpResponseMessageAssertions> BeAs<TModel>(TModel expectedModel, Func<EquivalencyAssertionOptions<TModel>, EquivalencyAssertionOptions<TModel>> options, string because = "", params object[] becauseArgs)
#endif
    {
        Guard.ThrowIfArgumentIsNull(options, nameof(options));

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

        if (expectedModel == null)
        {
            throw new ArgumentNullException(nameof(expectedModel), "Cannot verify having a content equivalent to a model against a <null> model.");
        }

        var expectedModelType = expectedModel.GetType();

        var (success, errorMessage) = TryGetSubjectModel(out var subjectModel, expectedModelType);

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(success)
            .FailWith("Expected {context:response} to have a content equivalent to a model of type {0}, but the JSON representation could not be parsed, as the operation failed with the following message: {2}{reason}. {1}",
                expectedModelType.ToString() ?? "unknown type", Subject, errorMessage);

        string[] failures;

        using (var scope = new AssertionScope())
        {
            subjectModel.Should().BeEquivalentTo(expectedModel, options);

            failures = scope.Discard();
        }

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
                   .BecauseOf(because, becauseArgs)
                   .ForCondition(failures.Length == 0)
                   .FailWith("Expected {context:response} to have a content equivalent to a model, but it has differences:{0}{reason}. {1}",
                       new AssertionsFailures(failures),
                       Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that HTTP response has content that matches a wildcard pattern.
    /// </summary>
    /// <remarks>
    /// The pattern is an anchored, case-sensitive text match. Casing is not normalized by this library; use the
    /// assertion framework's pattern options or a different assertion when case-insensitive matching is intended.
    /// </remarks>
    /// <param name="expectedWildcardText">
    /// The wildcard pattern with which the subject is matched, where * and ? have special meanings.
    /// <remarks>
    ///     <para>* - Matches any number of characters. You can use the asterisk (*) anywhere in a character string. Example: wh* finds what, white, and why, but not awhile or watch.</para>
    ///     <para>? - Matches a single alphabet in a specific position. Example: b?ll finds ball, bell, and bill.</para>
    /// </remarks>
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> MatchInContent(string expectedWildcardText, string because = "", params object[] becauseArgs)
    {
        Guard.ThrowIfArgumentIsNull(expectedWildcardText, nameof(expectedWildcardText), "Cannot verify an HTTP response content match a <null> wildcard pattern.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

        var content = GetContent();

        if (string.IsNullOrEmpty(content))
        {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:response} to match the wildcard pattern {0} in its content, but content was <null>{reason}. {1}",
                    expectedWildcardText,
                    Subject);
        }

        string[] failures;

        using (var scope = new AssertionScope())
        {
            content.Should().Match(expectedWildcardText);

            failures = scope.Discard();
        }

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
                   .BecauseOf(because, becauseArgs)
                   .ForCondition(failures.Length == 0)
                   .FailWith("Expected {context:response} to match a wildcard pattern in its content, but does not since:{0}{reason}. {1}",
                       new AssertionsFailures(failures),
                       Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
}