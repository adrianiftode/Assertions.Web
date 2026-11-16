using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Shouldly.Web.Internal;

namespace Shouldly;

/// <summary>
/// Shouldly assertions on the HTTP headers of an <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// <para>
/// These are flat Shouldly assertions: every method returns <c>void</c> and takes the header name as a parameter.
/// There is no header assertion object to continue from and no <c>.And</c>; pass the header name to each assertion
/// instead of chaining from a group, for example
/// <c>response.ShouldHaveHeader("Content-Type"); response.ShouldHaveHeaderMatching("Content-Type", "application/json*");</c>.
/// </para>
/// <para>
/// Header presence is checked case-insensitively by <see cref="ShouldHaveHeader"/>,
/// <see cref="ShouldNotHaveHeader"/>, <see cref="ShouldHaveEmptyHeader"/> and <see cref="ShouldHaveNonEmptyHeader"/>.
/// The value-oriented assertions document their own comparison rules.
/// </para>
/// </remarks>
/// <seealso cref="LocationShouldlyExtensions"/>
[ShouldlyMethods]
[DebuggerStepThrough]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class HeadersShouldlyExtensions
{
    /// <summary>
    /// Asserts that an HTTP response has a named header.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The expected header name.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveHeader(this HttpResponseMessage? actual, string header, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(header, nameof(header), "Cannot verify having a header against a <null> header.");
        EnsureHeaderPresent(actual, header, customMessage, nameof(ShouldHaveHeader), actualExpression);
    }

    /// <summary>
    /// Asserts that an HTTP response does not have a named header.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The unexpected header name.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldNotHaveHeader(this HttpResponseMessage? actual, string header, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(header, nameof(header), "Cannot verify not having a header against a <null> header.");
        AssertHeaderAbsent(actual, header, customMessage, nameof(ShouldNotHaveHeader), actualExpression);
    }

    /// <summary>
    /// Asserts that an existing HTTP header has exactly one value equivalent to the expected one.
    /// The comparison ignores the value's case, but not its surrounding whitespace.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The HTTP header name to be asserted.</param>
    /// <param name="value">The expected header value.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveHeaderWithValue(this HttpResponseMessage? actual, string header, string value,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(value, nameof(value),
            "Cannot verify an HTTP header to be a value against a <null> or empty value. Use ShouldHaveEmptyHeader to test if the HTTP header has no value.");
        AssertHeaderWithValue(actual, header, value, customMessage, nameof(ShouldHaveHeaderWithValue), actualExpression);
    }

    /// <summary>
    /// Asserts that an existing HTTP header has all expected values, in any order. Each value is
    /// compared with its expectation case-sensitively. Mirroring the FluentAssertions assertions,
    /// the header itself is looked up case-sensitively here; <see cref="ShouldHaveHeader"/> and the
    /// other assertions are case-insensitive on the header name.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The HTTP header name to be asserted.</param>
    /// <param name="expectedValues">The expected values with which the header values list is compared.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveHeaderWithValues(this HttpResponseMessage? actual, string header,
        IEnumerable<string> expectedValues, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(expectedValues, nameof(expectedValues),
            "Cannot verify an HTTP header to be a collection of expected values against a <null> collection. Use ShouldHaveEmptyHeader to test if the HTTP header has no values.");
        if (!expectedValues.Any())
        {
            throw new ArgumentException(
                "Cannot verify an HTTP header to be a collection of expected values against an empty collection. Use ShouldHaveEmptyHeader to test if the HTTP header has no values.",
                nameof(expectedValues));
        }

        AssertHeaderWithValues(actual, header, expectedValues, customMessage, nameof(ShouldHaveHeaderWithValues), actualExpression);
    }

    /// <summary>
    /// Asserts that an existing HTTP header has at least one value matching a wildcard pattern, where
    /// <c>*</c> matches any run of characters and <c>?</c> matches exactly one.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The HTTP header name to be asserted.</param>
    /// <param name="expectedWildcardValue">The wildcard pattern with which the header values are matched.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveHeaderMatching(this HttpResponseMessage? actual, string header,
        string expectedWildcardValue, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(expectedWildcardValue, nameof(expectedWildcardValue),
            "Cannot verify an HTTP header to be a value against a <null> value. Use ShouldHaveEmptyHeader to test if the HTTP header has no values.");
        AssertHeaderMatching(actual, header, expectedWildcardValue, customMessage, nameof(ShouldHaveHeaderMatching), actualExpression);
    }

    /// <summary>
    /// Asserts that an existing HTTP header has no values.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The HTTP header name to be asserted.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveEmptyHeader(this HttpResponseMessage? actual, string header, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var response = EnsureHeaderPresent(actual, header, customMessage, nameof(ShouldHaveEmptyHeader), actualExpression);
        ShouldlyWebFailure.Rethrow(response,
            () => response.GetHeaderValues(header).ShouldBeEmpty(customMessage, $"{actualExpression}.Headers[\"{header}\"]"));
    }

    /// <summary>
    /// Asserts that an existing HTTP header has at least one value.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="header">The HTTP header name to be asserted.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveNonEmptyHeader(this HttpResponseMessage? actual, string header, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var response = EnsureHeaderPresent(actual, header, customMessage, nameof(ShouldHaveNonEmptyHeader), actualExpression);
        ShouldlyWebFailure.Rethrow(response,
            () => response.GetHeaderValues(header).ShouldNotBeEmpty(customMessage, $"{actualExpression}.Headers[\"{header}\"]"));
    }

    /// <summary>Fails when the HTTP response has no header with a name equal to <paramref name="header"/>, ignoring case.</summary>
    internal static HttpResponseMessage EnsureHeaderPresent(HttpResponseMessage? actual, string header,
        string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        var headerNames = response.GetHeaders().Select(headerName => headerName.Key).ToArray();
        if (!headerNames.Any(name => string.Equals(name, header, StringComparison.OrdinalIgnoreCase)))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                header, headerNames, customMessage, shouldlyMethod, $"{actualExpression}.Headers"));
        }

        return response;
    }

    internal static void AssertHeaderAbsent(HttpResponseMessage? actual, string header, string? customMessage,
        string shouldlyMethod, string? actualExpression)
    {
        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        if (response.GetHeaders().Any(headerName => string.Equals(headerName.Key, header, StringComparison.OrdinalIgnoreCase)))
        {
            var values = response.GetHeaderValues(header).ToArray();
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                header, values, customMessage, shouldlyMethod, $"{actualExpression}.Headers"));
        }
    }

    internal static void AssertHeaderWithValue(HttpResponseMessage? actual, string header, string value,
        string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        var response = EnsureHeaderPresent(actual, header, customMessage, shouldlyMethod, actualExpression);
        var values = response.GetHeaderValues(header).ToArray();
        if (values.Length != 1)
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                value, values, customMessage, shouldlyMethod, $"{actualExpression}.Headers[\"{header}\"]"));
        }

        if (!string.Equals(values[0], value, StringComparison.OrdinalIgnoreCase))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                value, values, customMessage, shouldlyMethod, $"{actualExpression}.Headers[\"{header}\"]"));
        }
    }

    internal static void AssertHeaderWithValues(HttpResponseMessage? actual, string header,
        IEnumerable<string> expectedValues, string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        var response = EnsureHeaderPresent(actual, header, customMessage, shouldlyMethod, actualExpression);
        var values = response.GetHeaders().FirstOrDefault(c => c.Key == header).Value ?? Array.Empty<string>();
        ShouldlyWebFailure.Rethrow(response,
            () => values.ShouldBe(expectedValues, ignoreOrder: true, customMessage,
                $"{actualExpression}.Headers[\"{header}\"]"));
    }

    internal static void AssertHeaderMatching(HttpResponseMessage? actual, string header,
        string expectedWildcardValue, string? customMessage, string shouldlyMethod, string? actualExpression)
    {
        var response = EnsureHeaderPresent(actual, header, customMessage, shouldlyMethod, actualExpression);
        var headerValues = response.GetHeaderValues(header).ToArray();
        if (!headerValues.Any(headerValue => headerValue.WildcardMatch(expectedWildcardValue)))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedWildcardValue, headerValues, customMessage, shouldlyMethod,
                $"{actualExpression}.Headers[\"{header}\"]"));
        }
    }
}