using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Shouldly.Web.Internal;

namespace Shouldly;

/// <summary>
/// Shouldly assertions asserting that an HTTP response, or the model its content deserializes into,
/// satisfies one or more inner assertions.
/// </summary>
/// <remarks>
/// <para>
/// These are flat Shouldly assertions: every method returns <c>void</c> and stands on its own. There is no
/// <c>.And</c> and no assertion group to continue from; use a separate call for each expectation.
/// </para>
/// <para>
/// The overloads taking an array or sequence of assertions run each entry as its own condition and report all
/// failures. A single assertion lambda containing several statements stops at its first failure instead.
/// </para>
/// <para>
/// These assertions do not check the status code. Assert the status separately when it matters.
/// </para>
/// <para>
/// The model overloads deserialize the body into <c>TModel</c> before running the supplied conditions; use
/// <c>ShouldBeAs&lt;T&gt;()</c> for whole-object equivalency instead. A deserialization failure uses the
/// <c>should be as</c> layout, while failed model conditions use <c>should satisfy all the conditions specified</c>.
/// Unexpected exceptions thrown by a condition are captured with their exception text. The optional
/// <c>givenModelStructure</c> argument only infers the model type; its values are ignored. These assertions exist so
/// custom conditions about the response or its model stay inside a Shouldly.Web assertion: failures include the
/// complete request/response conversation, unlike plain Shouldly assertions after manual deserialization.
/// </para>
/// </remarks>
/// <seealso cref="HttpStatusCodeShouldlyExtensions"/>
/// <seealso cref="HttpResponseContentShouldlyExtensions"/>
[ShouldlyMethods]
[DebuggerStepThrough]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class SatisfyShouldlyExtensions
{
    /// <summary>
    /// Asserts that an HTTP response satisfies an assertion.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertion">An assertion about the HTTP response.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy(this HttpResponseMessage? actual, Action<HttpResponseMessage> assertion,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        ShouldlyWebFailure.Rethrow(response,
            () => NativeShouldSatisfy(response, new[] { assertion }, customMessage, actualExpression));
    }

    /// <summary>
    /// Asserts that an HTTP response satisfies every one of the given assertions.
    /// </summary>
    /// <remarks>
    /// Each entry runs as its own condition, so all failing assertions are reported. By contrast, a single
    /// assertion whose lambda body holds several statements stops at its first failure.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertions">Assertions about the HTTP response; each one is a separate condition.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy(this HttpResponseMessage? actual, Action<HttpResponseMessage>[] assertions,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertions, nameof(assertions),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        ShouldlyWebFailure.Rethrow(response,
            () => NativeShouldSatisfy(response, assertions, customMessage, actualExpression));
    }

    /// <summary>
    /// Asserts that an HTTP response satisfies every one of the given assertions.
    /// </summary>
    /// <remarks>
    /// Each entry runs as its own condition, so all failing assertions are reported. By contrast, a single
    /// assertion whose lambda body holds several statements stops at its first failure.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertions">Assertions about the HTTP response; each one is a separate condition.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy(this HttpResponseMessage? actual, IEnumerable<Action<HttpResponseMessage>> assertions,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertions, nameof(assertions),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        ShouldlyWebFailure.Rethrow(response,
            () => NativeShouldSatisfy(response, assertions.ToArray(), customMessage, actualExpression));
    }

    /// <summary>
    /// Asserts that an HTTP response satisfies an asynchronous assertion.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertion">An assertion about the HTTP response.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy(this HttpResponseMessage? actual, Func<HttpResponseMessage, Task> assertion,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        ShouldlyWebFailure.Rethrow(response,
            () => NativeShouldSatisfy(response, new[]
            {
                (Action<HttpResponseMessage>)(r => new Func<Task>(() => assertion(r))
                    .ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult())
            }, customMessage, actualExpression));
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an assertion.
    /// </summary>
    /// <remarks>
    /// Use this for custom assertions over the deserialized <typeparamref name="TModel"/>; use
    /// <c>ShouldBeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures use the
    /// <c>should be as</c> layout and are reported before failed model conditions. Failures include the complete
    /// request/response conversation; this is the main reason to prefer <c>ShouldSatisfy</c> over manual deserialization
    /// followed by plain Shouldly assertions.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertion">An assertion regarding the given model.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, Action<TModel> assertion,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, new[] { assertion }, customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an assertion starting from an
    /// inferred model structure.
    /// </summary>
    /// <remarks>
    /// Use this for custom assertions over the deserialized <typeparamref name="TModel"/>; use
    /// <c>ShouldBeAs&lt;T&gt;()</c> when the complete expected body is known. The given model structure only infers
    /// the model type; its values are ignored. Failures include the complete request/response conversation, so custom
    /// checks stay inside a Shouldly.Web assertion.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="givenModelStructure">
    /// A proposed model structure that will help to compose the assertions. This is used to define the type
    /// of the asserted model and it doesn't have to contain other values than the default one.
    /// </param>
    /// <param name="assertion">An assertion regarding the given model.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, TModel givenModelStructure,
        Action<TModel> assertion, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        _ = givenModelStructure; // Only used to infer TModel, as in the master library.
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, new[] { assertion }, customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an asynchronous assertion.
    /// </summary>
    /// <remarks>
    /// Use this for custom asynchronous assertions over the deserialized <typeparamref name="TModel"/>; use
    /// <c>ShouldBeAs&lt;T&gt;()</c> when the complete expected body is known. Deserialization failures use the
    /// <c>should be as</c> layout and are reported before failed model conditions. Failures include the complete
    /// request/response conversation; this is the main reason to prefer <c>ShouldSatisfy</c> over manual deserialization
    /// followed by plain Shouldly assertions.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertion">An assertion regarding the given model.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, Func<TModel, Task> assertion,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, new[]
        {
            (Action<TModel>)(model => new Func<Task>(() => assertion(model))
                .ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult())
        }, customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies an asynchronous assertion starting
    /// from an inferred model structure.
    /// </summary>
    /// <remarks>
    /// Use this for custom asynchronous assertions over the deserialized <typeparamref name="TModel"/>; use
    /// <c>ShouldBeAs&lt;T&gt;()</c> when the complete expected body is known. The given model structure only infers
    /// the model type; its values are ignored. Failures include the complete request/response conversation, so custom
    /// checks stay inside a Shouldly.Web assertion.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="givenModelStructure">
    /// A proposed model structure that will help to compose the assertions. This is used to define the type
    /// of the asserted model and it doesn't have to contain other values than the default one.
    /// </param>
    /// <param name="assertion">An assertion regarding the given model.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, TModel givenModelStructure,
        Func<TModel, Task> assertion, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        _ = givenModelStructure; // Only used to infer TModel, as in the master library.
        Guard.ThrowIfArgumentIsNull(assertion, nameof(assertion),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, new[]
        {
            (Action<TModel>)(model => new Func<Task>(() => assertion(model))
                .ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult())
        }, customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies every one of the given assertions.
    /// </summary>
    /// <remarks>
    /// Each entry runs as its own condition, so all failing assertions are reported. By contrast, a single
    /// assertion whose lambda body holds several statements stops at its first failure. Use these model conditions
    /// for custom assertions over the deserialized <c>TModel</c>; use <c>ShouldBeAs&lt;T&gt;()</c>
    /// for whole-object equivalency. In the inferred overload, the given model structure only supplies the model
    /// type; its values are ignored.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="assertions">Assertions regarding the model; each one is a separate condition.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, IEnumerable<Action<TModel>> assertions,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(assertions, nameof(assertions),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, assertions.ToArray(), customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Asserts that an HTTP response content can be a model that satisfies every one of the given assertions,
    /// starting from an inferred model structure.
    /// </summary>
    /// <remarks>
    /// Each entry runs as its own condition, so all failing assertions are reported. By contrast, a single
    /// assertion whose lambda body holds several statements stops at its first failure. Use these model conditions
    /// for custom assertions over the deserialized <c>TModel</c>; use <c>ShouldBeAs&lt;T&gt;()</c>
    /// for whole-object equivalency. In the inferred overload, the given model structure only supplies the model
    /// type; its values are ignored.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="givenModelStructure">
    /// A proposed model structure that will help to compose the assertions. This is used to define the type
    /// of the asserted model and it doesn't have to contain other values than the default one.
    /// </param>
    /// <param name="assertions">Assertions regarding the model; each one is a separate condition.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldSatisfy<TModel>(this HttpResponseMessage? actual, TModel givenModelStructure,
        IEnumerable<Action<TModel>> assertions, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        _ = givenModelStructure; // Only used to infer TModel, as in the master library.
        Guard.ThrowIfArgumentIsNull(assertions, nameof(assertions),
            "Cannot verify the subject satisfies a `null` assertion.");
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);

        SatisfyModel(response, assertions.ToArray(), customMessage, $"{actualExpression}.Content");
    }

    /// <summary>
    /// Deserializes the response into a model and runs the inner assertions against it, appending the
    /// HTTP response dump to any failure.
    /// </summary>
    private static void SatisfyModel<TModel>(HttpResponseMessage response, Action<TModel>[] assertions,
        string? customMessage, string actualExpression)
    {
        var (success, errorMessage, model) = response.TryReadModel(typeof(TModel));
        if (!success)
        {
            // Shouldly's ExpectedActualShouldlyMessage drops the actual value ("but was" section) when the
            // method name is ShouldSatisfy, which would hide the deserialization error. Report the failure
            // with the ShouldBeAs layout, shared with Task 07.
            throw ShouldlyWebFailure.Deserialization(response, typeof(TModel), errorMessage, customMessage,
                "ShouldBeAs", actualExpression);
        }

        var subjectModel = model is null ? default : (TModel)model;
        // The null-forgiving operator makes the native generic infer TModel (not TModel?), which matches
        // Action<TModel>[] exactly; a null subject model is still passed through, as in the master library.
        ShouldlyWebFailure.Rethrow(response,
            () => NativeShouldSatisfy(subjectModel!, assertions, customMessage, actualExpression));
    }

    /// <summary>
    /// Calls Shouldly's native multi-conditions assertion.
    /// The free <typeparamref name="T"/> receiver matters: at this call site only the native overload is
    /// applicable, because every extension of this class requires an <see cref="HttpResponseMessage"/>
    /// receiver. Without it the call would bind to this class' own overloads, which win overload resolution
    /// against the native one for identical arguments, and recurse.
    /// </summary>
    private static void NativeShouldSatisfy<T>(T actual, Action<T>[] conditions, string? customMessage,
        string? actualExpression)
        => actual.ShouldSatisfy(conditions, customMessage, actualExpression);
}