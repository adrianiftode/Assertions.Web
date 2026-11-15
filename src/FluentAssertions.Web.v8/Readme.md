# FluentAssertions.Web.v8

A [FluentAssertions](https://fluentassertions.com/) extension over the `HttpResponseMessage` object. It provides assertions specific to HTTP responses and outputs rich error messages that include the HTTP Request and Response, so test failures show the whole conversation and less time with debugging is spent.

This package targets the **commercial** versions of FluentAssertions (>= 8.0.0). If you are using FluentAssertions < 8.0.0, use [FluentAssertions.Web](https://www.nuget.org/packages/FluentAssertions.Web).

## Install

```shell
dotnet add package FluentAssertions.Web.v8
```

## Example

The assertions are identical to the FOSS flavour:

```csharp
[Fact]
public async Task Post_ReturnsOk()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.PostAsync("/api/comments", new StringContent(
    """
    {
      "author": "John",
      "content": "Hey, you..."
    }
    """, Encoding.UTF8, "application/json"));

    // Assert
    response.Should().Be200Ok();
}
```

When the assertion fails, the test output contains the expected and actual status codes followed by the complete HTTP response and the originating HTTP request.

## Documentation

The shared [Assertions.Web documentation](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/Assertions.Web/Readme.md) describes the assertions at a glance, the failure output, the configuration and links to the [full API](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#full-api) and the [worked examples](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#fluentassertionswebawesomeassertionsweb-examples) in the repository readme. For the composition rules — which assertion group each method returns and what `.And` exposes next — see [How the fluent API is composed](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/Assertions.Web/Readme.md#how-the-fluent-api-is-composed). For a complete, copy-pasteable integration test that covers the status code, a response header, the typed body and the Location header, see the [end-to-end example](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#end-to-end-example-fluentassertionswebv8).