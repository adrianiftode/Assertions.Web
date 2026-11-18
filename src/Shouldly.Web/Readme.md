# Shouldly.Web

A [Shouldly](https://shouldly.io/) extension over the `HttpResponseMessage` object. It provides HTTP-specific assertions whose failure messages include the HTTP Request and Response, so test failures show the whole conversation and less time with debugging is spent.

> This is an unofficial, community-maintained extension and is **not endorsed by the Shouldly project**. It requires Shouldly >= 5.0.0-preview.2 and is itself a prerelease until Shouldly 5.0.0 is stable.

## Install

```shell
dotnet add package Shouldly.Web --prerelease
```

## Example

With Shouldly the assertions are called directly on the response, and unlike the FluentAssertions/AwesomeAssertions flavours there is no `Should()`/`And` chain — each assertion is its own call:

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
    response.ShouldBe200Ok();
}
```

When the assertion fails, the test output follows the Shouldly layout (`should be`/`but was`) and continues with the complete HTTP response and the originating HTTP request.

## Reporting every failing assertion

Pass the assertions of a `ShouldSatisfy` check as a collection of conditions and every failing one is reported, not only the first:

```csharp
response.ShouldSatisfy([
    r => r.ShouldHaveHeader("X-Correlation-ID"),
    r => r.Headers.AcceptRanges.ShouldContain("byte")]);
```

Each failing condition appears as its own `Error 1`, `Error 2`, … block, matching what FluentAssertions reports for a multi-statement lambda. A single assertion lambda runs as one condition and stops at its first failure, so when migrating from FluentAssertions pass the statements of its body as the condition list instead.

## Documentation

The shared [Assertions.Web documentation](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/Assertions.Web/Readme.md) describes the assertions at a glance, the failure output and the configuration. For how these standalone calls compare to the fluent assertion groups — and why there is no `.And` — see [How the fluent API is composed](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/Assertions.Web/Readme.md#how-the-fluent-api-is-composed). The [Shouldly.Web section](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb) of the repository readme maps every FluentAssertions assertion name to its Shouldly counterpart, and the [Shouldly.Web Examples](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-examples) and [Shouldly.Web API](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-api) sections list them in detail. For a complete, copy-pasteable integration test that covers the status code, a response header, the typed body and the Location header, see the [end-to-end example](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#end-to-end-assertion-example-shouldlyweb). For comparison rules across header names, values, wildcards and models, see the [case-sensitivity map](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/Assertions.Web/Readme.md#case-sensitivity-map).