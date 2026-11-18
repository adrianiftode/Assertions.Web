#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary>
/// Contains extension methods for custom assertions on the model an HTTP response content deserializes into.
/// </summary>
/// <remarks>
/// In FluentAssertions versions before 8, these extensions provide <c>Satisfy</c> for models on the response
/// assertion returned by <c>response.Should()</c>. Each overload returns
/// <see cref="HttpResponseMessageAssertions"/>, so further assertions can be chained from <c>And</c>.
/// </remarks>
/// <seealso cref="HttpResponseMessageAssertions"/>
[DebuggerNonUserCode]
public static class SatisfyModelAssertionsExtensions
{
    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion regarding the given model.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. The response is deserialized into <typeparamref name="TModel"/> before
    /// the callback runs; use <c>BeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures
    /// are reported as JSON parsing failures, and unexpected exceptions are captured with their text. Every failure
    /// includes the complete request/response conversation. Assertions.Web preserves that context across the library,
    /// but it is especially valuable here because arbitrary test code stays inside the HTTP assertion. The assertion can
    /// be a single assertion or a collection of assertions if it is expressed as a statement lambda. In the inferred
    /// overloads, the given model structure only supplies the model type; its values are ignored.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy<TModel>(
#pragma warning disable 1573
            this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        Action<TModel> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(assertion, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an assertion starting from an inferred model structure.
    /// </summary>
    /// <param name="givenModelStructure">
    /// A proposed model structure that will help to compose the assertions. This is used to defined the type of the asserted model and it doesn't have to contain other values than the default one.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. The response is deserialized into <typeparamref name="TModel"/> before
    /// the callback runs; use <c>BeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures
    /// are reported as JSON parsing failures, and unexpected exceptions are captured with their text. Every failure
    /// includes the complete request/response conversation. Assertions.Web preserves that context across the library,
    /// but it is especially valuable here because arbitrary test code stays inside the HTTP assertion. The assertion can
    /// be a single assertion or a collection of assertions if it is expressed as a statement lambda. In the inferred
    /// overloads, the given model structure only supplies the model type; its values are ignored.
    /// </remarks>
    /// <param name="assertion">
    /// An assertion or a collection of assertions regarding the given model.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy<TModel>(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        TModel givenModelStructure,
        Action<TModel> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(givenModelStructure, assertion, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an asynchronous assertion.
    /// </summary>
    /// <param name="assertion">
    /// An assertion regarding the given model.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. The response is deserialized into <typeparamref name="TModel"/> before
    /// the callback runs; use <c>BeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures
    /// are reported as JSON parsing failures, and unexpected exceptions are captured with their text. Every failure
    /// includes the complete request/response conversation. Assertions.Web preserves that context across the library,
    /// but it is especially valuable here because arbitrary test code stays inside the HTTP assertion. The assertion can
    /// be a single assertion or a collection of assertions if it is expressed as a statement lambda. In the inferred
    /// overloads, the given model structure only supplies the model type; its values are ignored.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy<TModel>(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        Func<TModel, Task> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(assertion, because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an asynchronous assertion starting from an inferred model structure.
    /// </summary>
    /// <param name="givenModelStructure">
    /// A proposed model structure that will help to compose the assertions. This is used to defined the type of the asserted model and it doesn't have to contain other values than the default one.
    /// </param>
    /// <remarks>
    /// The callback is a general-purpose C# extension point, not a DSL limited to assertion-framework methods: it can
    /// inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion,
    /// and use <c>await</c> in async overloads. The response is deserialized into <typeparamref name="TModel"/> before
    /// the callback runs; use <c>BeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures
    /// are reported as JSON parsing failures, and unexpected exceptions are captured with their text. Every failure
    /// includes the complete request/response conversation. Assertions.Web preserves that context across the library,
    /// but it is especially valuable here because arbitrary test code stays inside the HTTP assertion. The assertion can
    /// be a single assertion or a collection of assertions if it is expressed as a statement lambda. In the inferred
    /// overloads, the given model structure only supplies the model type; its values are ignored.
    /// </remarks>
    /// <param name="assertion">
    /// An assertion or a collection of assertions regarding the given model.
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
    public static AndConstraint<HttpResponseMessageAssertions> Satisfy<TModel>(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        TModel givenModelStructure,
        Func<TModel, Task> assertion,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Satisfy(givenModelStructure, assertion, because, becauseArgs);
}
#endif