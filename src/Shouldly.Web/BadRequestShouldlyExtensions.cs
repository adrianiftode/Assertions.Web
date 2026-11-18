using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Assertions.Web.Internal;
using Shouldly.Web.Internal;

namespace Shouldly;

/// <summary>
/// Shouldly assertions on the validation errors of a Bad Request HTTP response, following the
/// semantics of the FluentAssertions.Web <c>BadRequestAssertions</c>. Like there, none of these
/// check the status code: call <see cref="HttpStatusCodeShouldlyExtensions.ShouldBe400BadRequest"/> first.
/// </summary>
/// <remarks>
/// <para>
/// These are flat Shouldly assertions: every method returns <c>void</c> and stands on its own. There is no
/// <c>.And</c> and no <c>BadRequestAssertions</c>-style group to continue from; write each expectation as a
/// separate call on the response, for example
/// <c>response.ShouldBe400BadRequest(); response.ShouldHaveError("Email", "*required*");</c>.
/// </para>
/// <para>
/// The status assertion and the validation assertions are independent. Calling a validation assertion does not
/// imply that the response is a Bad Request, so assert the status explicitly when that matters.
/// </para>
/// <para>
/// Validation field names and the JSON <c>errors</c> property are matched case-insensitively, while wildcard error
/// messages are case-sensitive text patterns.
/// </para>
/// </remarks>
/// <seealso cref="HttpStatusCodeShouldlyExtensions"/>
[ShouldlyMethods]
[DebuggerStepThrough]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class BadRequestShouldlyExtensions
{
    /// <summary>
    /// Asserts that the Bad Request response body has a field with an error message matching a
    /// wildcard pattern, where <c>*</c> matches any run of characters and <c>?</c> matches exactly one.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedErrorField">The expected error field name.</param>
    /// <param name="expectedWildcardErrorMessage">The wildcard pattern with which the field's error messages are matched.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveError(this HttpResponseMessage? actual, string expectedErrorField,
        string expectedWildcardErrorMessage, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedErrorField, nameof(expectedErrorField),
            "Cannot verify having an error against a <null> or empty field name.");
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedWildcardErrorMessage, nameof(expectedWildcardErrorMessage),
            "Cannot verify having an error against a <null> or empty wildcard error message.");

        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        using var errors = ValidationErrors.Read(response);
        if (!errors.HasField(expectedErrorField))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedErrorField, errors.Fields, customMessage, "should have error", $"{actualExpression}.Errors"));
        }

        var messages = errors.MessagesOf(expectedErrorField);
        if (!messages.Any(message => message.WildcardMatch(expectedWildcardErrorMessage)))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedWildcardErrorMessage, messages, customMessage,
                "should have error message", $"{actualExpression}.Errors[\"{expectedErrorField}\"]"));
        }
    }

    /// <summary>
    /// Asserts that the Bad Request response body has exactly one error field whose sole message
    /// matches a wildcard pattern.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedErrorField">The expected error field name.</param>
    /// <param name="expectedWildcardErrorMessage">The wildcard pattern with which the field's error messages are matched.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldOnlyHaveError(this HttpResponseMessage? actual, string expectedErrorField,
        string expectedWildcardErrorMessage, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedErrorField, nameof(expectedErrorField),
            "Cannot verify having only an error against a <null> or empty field name.");
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedWildcardErrorMessage, nameof(expectedWildcardErrorMessage),
            "Cannot verify having only an error against a <null> or empty wildcard error message.");

        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        using var errors = ValidationErrors.Read(response);
        if (!errors.HasField(expectedErrorField))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedErrorField, errors.Fields, customMessage, "should only have error", $"{actualExpression}.Errors"));
        }

        var siblingFields = errors.SiblingsOf(expectedErrorField);
        if (siblingFields.Count != 1)
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedErrorField, siblingFields, customMessage, "should only contain this error field", $"{actualExpression}.Errors"));
        }

        var messages = errors.MessagesOf(expectedErrorField);
        if (!messages.Any(message => message.WildcardMatch(expectedWildcardErrorMessage)))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedWildcardErrorMessage, messages, customMessage,
                "should have error message", $"{actualExpression}.Errors[\"{expectedErrorField}\"]"));
        }

        if (messages.Count != 1)
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedWildcardErrorMessage, messages, customMessage,
                "should only have error message", $"{actualExpression}.Errors[\"{expectedErrorField}\"]"));
        }
    }

    /// <summary>
    /// Asserts that the Bad Request response body does not have an error field with a given name.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedErrorField">The unexpected error field name.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldNotHaveError(this HttpResponseMessage? actual, string expectedErrorField,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedErrorField, nameof(expectedErrorField),
            "Cannot verify not having an error against a <null> or empty field name.");

        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        using var errors = ValidationErrors.Read(response);
        if (errors.HasField(expectedErrorField))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedErrorField, errors.Fields, customMessage, "should not have error", $"{actualExpression}.Errors"));
        }
    }

    /// <summary>
    /// Asserts that the Bad Request response body has some error message matching a wildcard pattern,
    /// whatever field it belongs to.
    /// </summary>
    /// <param name="actual">The HTTP response under test.</param>
    /// <param name="expectedWildcardErrorMessage">The wildcard pattern with which all error messages are matched.</param>
    /// <param name="customMessage">Extra text shown under <c>Additional Info</c> when the assertion fails.</param>
    /// <param name="actualExpression">Captured by the compiler; do not pass it.</param>
    public static void ShouldHaveErrorMessage(this HttpResponseMessage? actual, string expectedWildcardErrorMessage,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Guard.ThrowIfArgumentIsNullOrEmpty(expectedWildcardErrorMessage, nameof(expectedWildcardErrorMessage),
            "Cannot verify having an error against a <null> or empty wildcard error message.");

        var response = ResponseGuard.EnsureNotNull(actual, customMessage, actualExpression);
        using var errors = ValidationErrors.Read(response);
        if (!errors.AllMessages.Any(message => message.WildcardMatch(expectedWildcardErrorMessage)))
        {
            throw ShouldlyWebFailure.Create(response, new ExpectedActualShouldlyMessage(
                expectedWildcardErrorMessage, errors.AllMessages, customMessage,
                "should have error message", $"{actualExpression}.Errors"));
        }
    }
}