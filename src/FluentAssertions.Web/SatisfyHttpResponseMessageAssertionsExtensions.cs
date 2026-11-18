#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary>
/// Contains extension methods for custom assertions in unit tests related to an <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// In FluentAssertions versions before 8, these extensions provide <c>Satisfy</c> on the response assertion returned
/// by <c>response.Should()</c>. Each overload returns <see cref="HttpResponseMessageAssertions"/>, so further
/// assertions can be chained from <c>And</c>.
/// </remarks>
/// <seealso cref="HttpResponseMessageAssertions"/>
[DebuggerNonUserCode]
public static class SatisfyHttpResponseMessageAssertionsExtensions
{
    /// <summary>
    /// Asserts that an HTTP response satisfies an assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion about the HTTP response.
    /// </param>
    /// <remarks>
    /// Use this for custom assertions about the response itself; no deserialization occurs. Failures are collected by
    /// this HTTP assertion and reported with the complete request/response conversation, which is the main reason to
    /// prefer <c>Satisfy</c> over plain assertions outside the library. The assertion can be a single assertion or a
    /// collection of assertions if the assertion action is expressed as a statement lambda.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        Action<HttpResponseMessage> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(assertion, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response satisfies an asynchronous assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion about the HTTP response.
    /// </param>
    /// <remarks>
    /// Use this for custom assertions about the response itself; no deserialization occurs. Failures are collected by
    /// this HTTP assertion and reported with the complete request/response conversation, which is the main reason to
    /// prefer <c>Satisfy</c> over plain assertions outside the library. The assertion can be a single assertion or a
    /// collection of assertions if the assertion action is expressed as a statement lambda.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy(
#pragma warning disable 1573
            this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        Func<HttpResponseMessage, Task> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(assertion, because, becauseArgs);
}
#endif