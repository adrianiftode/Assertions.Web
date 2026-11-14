using System;
using System.Net;
using System.Text;

namespace HttpMessageFormatter.Tests;

public class HttpResponseFormatterExtensionsTests
{
    [Fact]
    public void GivenUnspecifiedResponse_ShouldFormatBasicResponse()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

        // Act
        var formatted = subject.Format();

        // Assert
        Assert.Contains("The HTTP response was:", formatted);
        Assert.Contains("HTTP/1.1 200 OK", formatted);
    }

    [Fact]
    public void GivenOptionsWithSmallerMaximumReadableBytes_WhenFormat_ThenOnlyAPartOfTheContentIsPrinted()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(new string('-', 50) + new string('+', 50))
        };
        var options = new HttpResponseFormatterOptions
        {
            MaximumReadableBytes = 20
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains("Content is too large to display", formatted);
        Assert.Contains(new string('-', 20), formatted);
        Assert.DoesNotContain("+", formatted);
    }

    [Fact]
    public void GivenDefaultOptions_WhenFormat_ThenTheWholeContentIsPrinted()
    {
        // Arrange
        var content = string.Concat(Enumerable.Repeat("-abc", 1000));
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(content)
        };
        var options = new HttpResponseFormatterOptions();

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.DoesNotContain("too large", formatted);
        Assert.Contains(content, formatted);
    }

    [Fact]
    public void GivenMaximumReadableBytesNotGreaterThanZero_WhenSet_ThenThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var options = new HttpResponseFormatterOptions();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => options.MaximumReadableBytes = 0);
        Assert.Throws<ArgumentOutOfRangeException>(() => options.MaximumReadableBytes = -1);
    }

    [Fact]
    public void GivenConfiguredResponseFormatter_WhenFormat_ThenTheBuiltInFormattingIsBypassedAndTheDelegateOutputIsUsed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("""{"stackTrace":"line1\r\nline2"}""", Encoding.UTF8, "application/json"),
            RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost")
        };
        var options = new HttpResponseFormatterOptions
        {
            ResponseFormatter = response => $"CUSTOM {response.StatusCode} {response.RequestMessage!.Method} {response.RequestMessage.RequestUri}"
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains("CUSTOM InternalServerError GET http://localhost/", formatted);
        Assert.DoesNotContain("The originating HTTP request was:", formatted);
        Assert.DoesNotContain("application/json", formatted);
    }

    [Fact]
    public void GivenConfiguredResponseFormatter_WhenItThrows_ThenTheExceptionIsReportedInTheFormattedOutput()
    {
        // Arrange
        using var subject = new HttpResponseMessage();
        var options = new HttpResponseFormatterOptions
        {
            ResponseFormatter = _ => throw new InvalidOperationException("boom")
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains("The HTTP response was:", formatted);
        Assert.Contains("An exception occurred while trying to format the HTTP response:", formatted);
        Assert.Contains("boom", formatted);
    }

    [Fact]
    public void GivenConfiguredPostFormat_WhenFormat_ThenItTransformsTheBuiltInFormattedOutputOfTheResponseAndTheRequest()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("""{"stackTrace":"at SomeMethod()\r\nat OtherMethod()"}""", Encoding.UTF8, "application/json"),
            RequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost")
        };
        var options = new HttpResponseFormatterOptions
        {
            PostFormat = formatted => formatted.Replace("\\r\\n", Environment.NewLine)
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains($"at SomeMethod(){Environment.NewLine}at OtherMethod()", formatted);
        Assert.DoesNotContain("\\r\\n", formatted);
        Assert.Contains("The originating HTTP request was:", formatted);
    }

    [Fact]
    public void GivenConfiguredPostFormat_WhenTheBuiltInFormattingBypassedThroughResponseFormatter_ThenPostFormatIsAppliedToItsOutput()
    {
        // Arrange
        using var subject = new HttpResponseMessage();
        var options = new HttpResponseFormatterOptions
        {
            ResponseFormatter = _ => "Custom-Output",
            PostFormat = formatted => formatted.ToUpperInvariant()
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.DoesNotContain("Custom-Output", formatted);
        Assert.Contains("CUSTOM-OUTPUT", formatted);
    }

    [Fact]
    public void GivenConfiguredPostFormat_WhenItThrows_ThenTheUntransformedOutputIsKeptAndTheExceptionIsReported()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent("some content")
        };
        var options = new HttpResponseFormatterOptions
        {
            PostFormat = _ => throw new InvalidOperationException("post-format boom")
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains("some content", formatted);
        Assert.Contains("An exception occurred while trying to post-format the response details:", formatted);
        Assert.Contains("post-format boom", formatted);
    }

    [Fact]
    public void GivenConfiguredPostFormat_WhenItReturnsNull_ThenTheUntransformedOutputIsKept()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent("some content")
        };
        var options = new HttpResponseFormatterOptions
        {
            PostFormat = _ => null!
        };

        // Act
        var formatted = subject.Format(options);

        // Assert
        Assert.Contains("some content", formatted);
    }
}
