#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary>
/// Contains extension methods for custom assertions in unit tests related to the HTTP response content.
/// </summary>
/// <remarks>
/// <para>
/// In FluentAssertions versions before 8, these extensions provide the content assertions on the response assertion
/// returned by <c>response.Should()</c>. Each one returns <see cref="HttpResponseMessageAssertions"/>, so status,
/// header and further content assertions can be chained from <c>And</c>.
/// </para>
/// </remarks>
/// <seealso cref="HttpResponseMessageAssertions"/>
[DebuggerNonUserCode]
public static class HttpResponseContentAssertionsExtensions
{
    /// <summary>
    /// Asserts that HTTP response content is empty
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    /// <returns>The same <see cref="HttpResponseMessageAssertions"/> group.</returns>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> BeEmpty(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).BeEmpty(because, becauseArgs);

    /// <summary>
    /// Asserts that HTTP response content can be an equivalent representation of the expected model.
    /// </summary>
    /// <param name="expectedModel">
    /// The expected model.
    /// </param>
    /// <param name="options">
    /// A reference to the <see cref="EquivalencyAssertionOptions{TExpectation}"/> configuration object that can be used
    /// to influence the way the object graphs are compared. You can also provide an alternative instance of the
    /// <see cref="EquivalencyAssertionOptions{TExpectation}"/> class. The global defaults are determined by the
    /// <see cref="AssertionOptions"/> class.
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
    public static AndConstraint<HttpResponseMessageAssertions> BeAs<TModel>(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        TModel expectedModel, Func<EquivalencyAssertionOptions<TModel>, EquivalencyAssertionOptions<TModel>> options, string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).BeAs(expectedModel, options, because, becauseArgs);

    /// <summary>
    /// Asserts that HTTP response content can be an equivalent representation of the expected model.
    /// </summary>
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
    public static AndConstraint<HttpResponseMessageAssertions> BeAs<TModel>(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        TModel expectedModel, string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).BeAs(expectedModel, because, becauseArgs);

    /// <summary>
    /// Asserts that HTTP response has content that matches a wildcard pattern.
    /// </summary>
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
    public static AndConstraint<HttpResponseMessageAssertions> MatchInContent(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string expectedWildcardText, string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).MatchInContent(expectedWildcardText, because, becauseArgs);
}
#endif