using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Shouldly.Web.Internal;

namespace Shouldly;

/// <summary>
/// Shouldly assertions on the Location header of an <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// <para>
/// These are flat Shouldly assertions: every method returns <c>void</c>, targets the <c>Location</c> header and
/// stands on its own. There is no <c>.And</c> and no location assertion group to continue from.
/// </para>
/// <para>
/// These assertions do not check the status code. A location-bearing status assertion is a separate call, for
/// example <c>response.ShouldBe201Created(); response.ShouldHaveLocation();</c>.
/// </para>
/// </remarks>
/// <seealso cref="HeadersShouldlyExtensions"/>
[ShouldlyMethods]
[DebuggerStepThrough]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class LocationShouldlyExtensions
{
    /// <summary>
    /// Asserts that an HTTP response has a Location header.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveLocation(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => HeadersShouldlyExtensions.EnsureHeaderPresent(actual, "Location", customMessage, nameof(ShouldHaveLocation), actualExpression);

    /// <summary>
    /// Asserts that an HTTP response does not have a Location header.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldNotHaveLocation(this HttpResponseMessage? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        => HeadersShouldlyExtensions.AssertHeaderAbsent(actual, "Location", customMessage, nameof(ShouldNotHaveLocation), actualExpression);

    /// <summary>
    /// Asserts that the Location header of an HTTP response has exactly one value equivalent to the
    /// expected one. The comparison ignores the value's case, but not its surrounding whitespace.
    /// </summary>
    /// <remarks>
    /// The HTTP field name is matched case-insensitively. The ignore-case value comparison is a compatibility
    /// promise, not a field-specific semantic comparison.
    /// </remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedValue">The expected Location header value.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveLocationWithValue(this HttpResponseMessage? actual, string expectedValue,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedValue, nameof(expectedValue),
            "Cannot verify an HTTP header to be a value against a <null> or empty value. Use ShouldHaveEmptyHeader to test if the HTTP header has no value.");
        HeadersShouldlyExtensions.AssertHeaderWithValue(actual, "Location", expectedValue, customMessage,
            nameof(ShouldHaveLocationWithValue), actualExpression);
    }

    /// <summary>
    /// Asserts that the Location header of an HTTP response has at least one value matching a wildcard
    /// pattern, where <c>*</c> matches any run of characters and <c>?</c> matches exactly one.
    /// </summary>
    /// <remarks>The wildcard pattern is an anchored, case-sensitive text pattern.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedWildcardValue">The wildcard pattern with which the header values are matched.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveLocationMatching(this HttpResponseMessage? actual, string expectedWildcardValue,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNull(expectedWildcardValue, nameof(expectedWildcardValue),
            "Cannot verify an HTTP header to be a value against a <null> value. Use ShouldHaveEmptyHeader to test if the HTTP header has no values.");
        HeadersShouldlyExtensions.AssertHeaderMatching(actual, "Location", expectedWildcardValue, customMessage,
            nameof(ShouldHaveLocationMatching), actualExpression);
    }

    /// <summary>
    /// Asserts that the Location header of an HTTP response has all expected values, in any order.
    /// Each value is compared with its expectation case-sensitively.
    /// </summary>
    /// <remarks>The HTTP field name is matched case-insensitively, as required by HTTP.</remarks>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedValues">The expected values with which the Location header values are compared.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveLocationWithValues(this HttpResponseMessage? actual, IEnumerable<string> expectedValues,
        string? customMessage = null,
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

        HeadersShouldlyExtensions.AssertHeaderWithValues(actual, "Location", expectedValues, customMessage,
            nameof(ShouldHaveLocationWithValues), actualExpression);
    }
}