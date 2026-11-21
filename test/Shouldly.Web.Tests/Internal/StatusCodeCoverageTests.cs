using System.Net;
using System.Reflection;

namespace Shouldly.Web.Tests.Internal;

/// <summary>
/// Coverage baseline for the individual status-code assertions. Two sources define the baseline, and neither is
/// assumed to be exhaustive on its own: every named value of the .NET <see cref="HttpStatusCode"/> enum, and every
/// code currently registered with IANA. The IANA list is checked in; recheck the registry when updating it.
/// Codes that are retained for compatibility (418 and 306) are included so their assertions stay covered.
/// </summary>
public class StatusCodeCoverageTests
{
    private static readonly int[] IanaRegisteredCodes =
    {
        100, 101, 102, 103,
        200, 201, 202, 203, 204, 205, 206, 207, 208, 226,
        300, 301, 302, 303, 304, 305, 306, 307, 308,
        400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418,
        421, 422, 423, 424, 425, 426, 428, 429, 431, 451,
        500, 501, 502, 503, 504, 505, 506, 507, 508, 510, 511
    };

    private static readonly HashSet<string> ShouldlyMethodNames = typeof(HttpStatusCodeShouldlyExtensions)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Select(method => method.Name)
        .ToHashSet(StringComparer.Ordinal);

    [Fact]
    public void Every_IANA_registered_status_code_has_an_individual_shouldly_assertion()
    {
        var missing = IanaRegisteredCodes
            .Where(code => !HasIndividualAssertion(code))
            .ToArray();

        missing.ShouldBeEmpty();
    }

    [Fact]
    public void Every_named_HttpStatusCode_enum_value_has_an_individual_shouldly_assertion()
    {
        var missing = Enum.GetValues(typeof(HttpStatusCode))
            .Cast<HttpStatusCode>()
            .Select(status => (int)status)
            .Where(code => !HasIndividualAssertion(code))
            .ToArray();

        missing.ShouldBeEmpty();
    }

    private static bool HasIndividualAssertion(int code)
    {
        var prefix = $"ShouldBe{code}";
        return ShouldlyMethodNames.Any(name =>
            name.StartsWith(prefix, StringComparison.Ordinal) &&
            name.Length > prefix.Length &&
            !char.IsDigit(name[prefix.Length]));
    }
}
