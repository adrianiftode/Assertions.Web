using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Shouldly.Web.Internal;

namespace Shouldly;

/// <summary>
/// Shouldly assertions on the status code of an <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// <para>
/// These are flat Shouldly assertions: every method returns <c>void</c> and stands on its own. There is no
/// <c>.And</c> and no assertion object whose type changes after a status assertion. Write each expectation as a
/// separate call on the same response, for example:
/// <c>response.ShouldBe400BadRequest(); response.ShouldHaveError("Email", "*required*");</c>.
/// </para>
/// <para>
/// A status assertion checks only the status code; it makes no assertion about the response body or headers.
/// After <c>ShouldBe400BadRequest()</c>, use the validation-error assertions; headers and content have their own
/// assertions. None of them depend on a previous call, so they can be reordered or repeated freely.
/// </para>
/// </remarks>
/// <seealso cref="BadRequestShouldlyExtensions"/>
/// <seealso cref="HeadersShouldlyExtensions"/>
/// <seealso cref="LocationShouldlyExtensions"/>
/// <seealso cref="HttpResponseContentShouldlyExtensions"/>
[ShouldlyMethods]
[DebuggerStepThrough]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class HttpStatusCodeShouldlyExtensions
{
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing an informational response.
    /// </summary>
    /// <remarks>The HTTP response was an informational one if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 100-199.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe1XXInformational(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Informational(), customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has a successful HTTP status code.
    /// </summary>
    /// <remarks>The HTTP response was successful if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 200-299.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe2XXSuccessful(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Successful(), customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a redirection response.
    /// </summary>
    /// <remarks>The HTTP response was a redirection if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 301-399. 300 Multiple Choices is deliberately not a redirection, mirroring the FluentAssertions assertions.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe3XXRedirection(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Redirection(), customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a client error.
    /// </summary>
    /// <remarks>The HTTP response was a client error if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 400-499.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe4XXClientError(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.ClientError(), customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a server error.
    /// </summary>
    /// <remarks>The HTTP response was a server error if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was 500 or above.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe5XXServerError(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.ServerError(), customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has an HTTP status with the specified code.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expected">The code of the expected HTTP Status.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveHttpStatusCode(this HttpResponseMessage? actual, HttpStatusCode expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        ShouldlyWebFailure.Rethrow(response,
            () => response.StatusCode.ShouldBe(expected, customMessage, $"{actualExpression}.StatusCode"));
    }

    /// <summary>
    /// Asserts that an HTTP response does not have an HTTP status with the specified code.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="unexpected">The code of the unexpected HTTP Status.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldNotHaveHttpStatusCode(this HttpResponseMessage? actual, HttpStatusCode unexpected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        ShouldlyWebFailure.Rethrow(response,
            () => response.StatusCode.ShouldNotBe(unexpected, customMessage, $"{actualExpression}.StatusCode"));
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 100 Continue
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe100Continue(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(100, "HttpStatusCode.Continue"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 101 Switching Protocols
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe101SwitchingProtocols(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(101, "HttpStatusCode.SwitchingProtocols"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 200 Ok
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe200Ok(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(200, "HttpStatusCode.OK"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 201 Created
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe201Created(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(201, "HttpStatusCode.Created"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 202 Accepted
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe202Accepted(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(202, "HttpStatusCode.Accepted"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 203 Non Authoritative Information
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe203NonAuthoritativeInformation(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(203, "HttpStatusCode.NonAuthoritativeInformation"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 204 No Content
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe204NoContent(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(204, "HttpStatusCode.NoContent"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 205 Reset Content
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe205ResetContent(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(205, "HttpStatusCode.ResetContent"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 206 Partial Content
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe206PartialContent(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(206, "HttpStatusCode.PartialContent"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 300 Multiple Choices
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe300MultipleChoices(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(300, "HttpStatusCode.MultipleChoices"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 300 Ambiguous
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe300Ambiguous(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(300, "HttpStatusCode.Ambiguous"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 301 Moved Permanently
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe301MovedPermanently(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(301, "HttpStatusCode.MovedPermanently"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 301 Moved
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe301Moved(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(301, "HttpStatusCode.Moved"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 302 Found
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe302Found(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(302, "HttpStatusCode.Found"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 302 Redirect
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe302Redirect(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(302, "HttpStatusCode.Redirect"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 303 See Other
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe303SeeOther(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(303, "HttpStatusCode.SeeOther"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 303 Redirect Method
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe303RedirectMethod(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(303, "HttpStatusCode.RedirectMethod"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 304 Not Modified
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe304NotModified(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(304, "HttpStatusCode.NotModified"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 305 Use Proxy
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe305UseProxy(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(305, "HttpStatusCode.UseProxy"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 306 Unused
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe306Unused(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(306, "HttpStatusCode.Unused"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 307 Temporary Redirect
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe307TemporaryRedirect(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(307, "HttpStatusCode.TemporaryRedirect"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 307 Redirect Keep Verb
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe307RedirectKeepVerb(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(307, "HttpStatusCode.RedirectKeepVerb"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 308 Permanent Redirect
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe308PermanentRedirect(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(308, "HttpStatusCode.PermanentRedirect"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 400 BadRequest
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe400BadRequest(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(400, "HttpStatusCode.BadRequest"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 401 Unauthorized
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe401Unauthorized(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(401, "HttpStatusCode.Unauthorized"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 402 Payment Required
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe402PaymentRequired(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(402, "HttpStatusCode.PaymentRequired"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 403 Forbidden
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe403Forbidden(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(403, "HttpStatusCode.Forbidden"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 404 Not Found
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe404NotFound(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(404, "HttpStatusCode.NotFound"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 405 Method Not Allowed
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe405MethodNotAllowed(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(405, "HttpStatusCode.MethodNotAllowed"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 406 Not Acceptable
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe406NotAcceptable(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(406, "HttpStatusCode.NotAcceptable"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 407 Proxy Authentication Required
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe407ProxyAuthenticationRequired(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(407, "HttpStatusCode.ProxyAuthenticationRequired"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 408 Request Timeout
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe408RequestTimeout(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(408, "HttpStatusCode.RequestTimeout"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 409 Conflict
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe409Conflict(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(409, "HttpStatusCode.Conflict"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 410 Gone
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe410Gone(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(410, "HttpStatusCode.Gone"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 411 Length Required
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe411LengthRequired(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(411, "HttpStatusCode.LengthRequired"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 412 Precondition Failed
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe412PreconditionFailed(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(412, "HttpStatusCode.PreconditionFailed"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 413 Request Entity Too Large
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe413RequestEntityTooLarge(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(413, "HttpStatusCode.RequestEntityTooLarge"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 414 Request Uri Too Long
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe414RequestUriTooLong(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(414, "HttpStatusCode.RequestUriTooLong"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 415 Unsupported Media Type
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe415UnsupportedMediaType(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(415, "HttpStatusCode.UnsupportedMediaType"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 416 Requested Range Not Satisfiable
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe416RequestedRangeNotSatisfiable(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(416, "HttpStatusCode.RequestedRangeNotSatisfiable"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 417 Expectation Failed
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe417ExpectationFailed(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(417, "HttpStatusCode.ExpectationFailed"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 418 I'm A Teapot
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe418ImATeapot(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(418, "HttpStatusCode.ImATeapot"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 422 UnprocessableEntity
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe422UnprocessableEntity(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(422, "HttpStatusCode.UnprocessableEntity"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 429 TooManyRequests
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe429TooManyRequests(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(429, "HttpStatusCode.TooManyRequests"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 426 UpgradeRequired
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe426UpgradeRequired(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(426, "HttpStatusCode.UpgradeRequired"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 500 Internal Server Error
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe500InternalServerError(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(500, "HttpStatusCode.InternalServerError"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 501 Not Implemented
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe501NotImplemented(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(501, "HttpStatusCode.NotImplemented"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 502 Bad Gateway
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe502BadGateway(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(502, "HttpStatusCode.BadGateway"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 503 Service Unavailable
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe503ServiceUnavailable(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(503, "HttpStatusCode.ServiceUnavailable"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 504 Gateway Timeout
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe504GatewayTimeout(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(504, "HttpStatusCode.GatewayTimeout"),
            customMessage, actualExpression);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 505 Http Version Not Supported
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldBe505HttpVersionNotSupported(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => StatusCodeAssertion.Assert(actual, StatusCodeExpectation.Exactly(505, "HttpStatusCode.HttpVersionNotSupported"),
            customMessage, actualExpression);
}
