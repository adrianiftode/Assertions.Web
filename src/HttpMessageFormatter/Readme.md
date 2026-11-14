 **HttpMessageFormatter** is the shared HTTP request/response rendering engine behind the [FluentAssertions.Web](https://www.nuget.org/packages/FluentAssertions.Web), [FluentAssertions.Web.v8](https://www.nuget.org/packages/FluentAssertions.Web.v8), [AwesomeAssertions.Web](https://www.nuget.org/packages/AwesomeAssertions.Web) and [Shouldly.Web](https://www.nuget.org/packages/Shouldly.Web) assertion libraries, published on its own so it can be used in any other context too.

It formats HTTP request and response messages for inspection and debugging, producing rich, readable output that includes headers, content, status codes, and more. This library has no dependencies on any assertion framework, making it suitable for general-purpose use.

### Basic Usage

```csharp
using HttpMessageFormatter;

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
}

```

### Limiting The Printed Content

By default, only the first 10 * 128 * 1024 characters of the content are printed. Pass an
`HttpResponseFormatterOptions` parameter object to change this limit:

```csharp
using HttpMessageFormatter;

var formatted = subject.Format(new HttpResponseFormatterOptions
{
    MaximumReadableBytes = 4 * 1024
});
```

### Customizing The Formatted Output

`HttpResponseFormatterOptions` also exposes two optional delegates that give full control over the
formatted output of both the response and the originating request:

- `ResponseFormatter` (`Func<HttpResponseMessage, string>`) — when configured, it replaces the
  built-in formatting entirely. It receives the complete `HttpResponseMessage` (the originating
  request is available through its `RequestMessage` property) and returns the text to be printed.
  When it is set, the built-in pipeline, including the content processors and the
  `MaximumReadableBytes` limit, is bypassed.
- `PostFormat` (`Func<string, string>`) — when configured, it transforms the final formatted output,
  whether it was produced by the built-in formatting or by `ResponseFormatter`. It can be used, for
  example, to make escaped newlines in stack traces readable or to redact sensitive values.

```csharp
var formatted = subject.Format(new HttpResponseFormatterOptions
{
    PostFormat = formattedOutput => formattedOutput.Replace("\\r\\n", Environment.NewLine)
});
```

Both delegates are optional; when neither is set, the formatting is unchanged. If a delegate throws,
the exception is reported into the formatted output, similar to any other formatting error.
