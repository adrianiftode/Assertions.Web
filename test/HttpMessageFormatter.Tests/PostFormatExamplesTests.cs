using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HttpMessageFormatter;
using Xunit;

namespace HttpMessageFormatter.Tests;

/// <summary>
/// Verifies the examples documented for the PostFormat delegate in the readmes.
/// </summary>
public class PostFormatExamplesTests
{
    [Fact]
    public void RemovingTheRequestAuthorizationHeader()
    {
        using var subject = new HttpResponseMessage
        {
            RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost")
        };
        subject.RequestMessage.Headers.Add("Authorization", "Bearer super-secret-token");
        var options = new HttpResponseFormatterOptions
        {
            PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
        };

        var formatted = subject.Format(options);

        Assert.DoesNotContain("Authorization", formatted);
        Assert.DoesNotContain("super-secret-token", formatted);
    }

    [Fact]
    public void RemovingTheNewLines()
    {
        using var subject = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("""{"message":"oops"}""", Encoding.UTF8, "application/json")
        };
        var options = new HttpResponseFormatterOptions
        {
            PostFormat = formatted => formatted.Replace("\r\n", " ").Replace('\n', ' ')
        };

        var formatted = subject.Format(options);

        Assert.Contains("The HTTP response was:", formatted);
        // The dump that follows the fixed "The HTTP response was:" label is printed on a single line.
        var afterHeader = formatted.Substring(formatted.IndexOf("was:", StringComparison.Ordinal) + 4);
        afterHeader = afterHeader.Substring(afterHeader.IndexOf('\n') + 1);
        Assert.DoesNotContain("\n", afterHeader);
        Assert.Contains(" HTTP/1.1 500 InternalServerError ", afterHeader);
    }
}
