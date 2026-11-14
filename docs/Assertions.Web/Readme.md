# Assertions.Web

The shared HTTP assertion contract behind the **Assertions.Web** family of packages:
[FluentAssertions.Web](https://www.nuget.org/packages/FluentAssertions.Web),
[FluentAssertions.Web.v8](https://www.nuget.org/packages/FluentAssertions.Web.v8),
[AwesomeAssertions.Web](https://www.nuget.org/packages/AwesomeAssertions.Web) and
[Shouldly.Web](https://www.nuget.org/packages/Shouldly.Web).

It provides assertions specific to HTTP responses and outputs rich error messages when the tests fail, so less time with debugging is spent. The assertion packages differ only in the framework they extend — FluentAssertions, AwesomeAssertions or Shouldly — and expose the same assertions through each framework's native syntax.

## Assertions at a glance

FluentAssertions and AwesomeAssertions syntax:

```csharp
response.Should().Be200Ok();                                               // status codes: Be200Ok, Be404NotFound, Be400BadRequest, Be5XXServerError, ...
response.Should().Be400BadRequest().And.HaveError("Author", "*required*"); // validation errors carried by a 400 response
response.Should().BeAs(new { Author = "John" });                           // the body is equivalent to an object
response.Should().Satisfy<IEnumerable<Comment>>(comments =>               // any assertions over the deserialized body
    comments.Should().HaveCount(2).And.OnlyHaveUniqueItems(c => c.CommentId));
response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*");         // headers
response.Should().MatchInContent("*\"author\"*");                          // raw content
```

Shouldly syntax — the same assertions, no `Should()`/`And` chain, each call returns `void`:

```csharp
response.ShouldBe200Ok();
response.ShouldBe400BadRequest();
response.ShouldBeAs(new { Author = "John" });
response.ShouldSatisfy<IEnumerable<Comment>>(comments => comments.ShouldHaveCount(2));
response.ShouldHaveHeader("X-Correlation-ID");
response.ShouldMatchInContent("*\"author\"*");
```

## When a test fails, you see the whole conversation

Every failure message produced by these packages is followed by **the complete HTTP response _and_ the originating HTTP request** — status line, headers and both bodies, the same way an HTTP interceptor like Fiddler would render them. There is no need to attach a debugger or to watch `response.Content.ReadAsStringAsync().Result` anymore: the reason for the failure and the payload that caused it are already in the test output.

```text
  Error Message:
   Expected response to be HttpStatusCode.OK {value: 200}, but found HttpStatusCode.Created {value: 201}.

The HTTP response was:

HTTP/1.1 201 Created
Location: http://localhost/api/Comments/1
X-Correlation-ID: 42c3a077-12ec-470e-8569-ddeaade90127
Content-Type: application/json; charset=utf-8

{
  "author": "John",
  "content": "Hey, you...",
  "commentId": 1
}

The originating HTTP request was:

POST http://localhost/api/comments HTTP 1.1
Content-Type: application/json; charset=utf-8
Content-Length: 50
{
  "author": "John",
  "content": "Hey, you..."
}
```

## Configuration

> **Breaking in 3.0:** the separate holders `FluentAssertionsWebConfig` and `AwesomeAssertionsWebConfig` are replaced by the single `AssertionsWebConfig`, which carries both `Serializer` and `ResponseFormatterOptions`.

### Deserialization

By default `System.Text.Json` is used to deserialize the response content. The related `System.Text.Json.JsonSerializerOptions` is accessible via the `SystemTextJsonSerializerConfig.Options` static field:

```csharp
SystemTextJsonSerializerConfig.Options.PropertyNameCaseInsensitive = false;
```

Newtonsoft.Json support is **optional** — `System.Text.Json` is the default serializer and already ships with every assertion library. The serializer is also replaceable by implementing the `ISerializer` interface; the Newtonsoft.Json one ships as the single **Assertions.Web.Serializers.NewtonsoftJson** package, shared by every flavour:

```csharp
AssertionsWebConfig.Serializer = new NewtonsoftJsonSerializer();
```

### Response formatting

The failure messages include a readable rendering of the HTTP response. By default, only the first `10 * 128 * 1024` bytes of the response content are printed, the rest is replaced by a warning. To change this limit globally:

```csharp
AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
{
    MaximumReadableBytes = 4 * 1024
};
```

The change must be done before the test is run. These global options only apply to the assertion packages; the [HttpMessageFormatter](https://www.nuget.org/packages/HttpMessageFormatter) library itself keeps no global state and accepts the same `HttpResponseFormatterOptions` parameter object per call.

Two optional delegates further customize the formatted output of both the response and the originating request:

- `ResponseFormatter` (`Func<HttpResponseMessage, string>`) — replaces the built-in formatting entirely: it receives the complete `HttpResponseMessage` and returns the text printed into the failure message. When set, the built-in pipeline, including the content processors and the `MaximumReadableBytes` limit, is bypassed.
- `PostFormat` (`Func<string, string>`) — transforms the final formatted output, no matter if it came from the built-in formatting or from `ResponseFormatter`. For example, to make escaped stack-trace newlines readable:

```csharp
AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
{
    PostFormat = formatted => formatted.Replace("\\r\\n", Environment.NewLine)
};
```

Another typical use is removing the `Authorization` header of the originating request from the output, so no credentials are ever printed in the failure message. The headers are rendered one per line, so the line can be dropped based on its prefix (this needs `using System.Text.RegularExpressions;`):

```csharp
AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
{
    PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
};
```

New lines can also be removed from the printed response and request details, so the whole dump is printed on a single line, after the `The HTTP response was:` header. This is useful on CI, where log parsers like the ones of GitHub Actions or Azure DevOps can mangle multi-line output:

```csharp
AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
{
    PostFormat = formatted => formatted.Replace("\r\n", " ").Replace('\n', ' ')
};
```

Both delegates are optional; when neither is set, the formatting is unchanged. If a delegate throws, the exception is reported into the formatted output, similar to any other formatting error.

## The full API

The complete listing of every assertion, with the `dotnet test` failure output for each group, lives in the [repository readme](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md):

- [Full API (FluentAssertions / AwesomeAssertions names)](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#full-api)
- [Shouldly.Web API (the flat Shouldly naming)](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-api)
- [Shouldly.Web: the Shouldly names for the same assertions](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb)
- [Worked examples](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#fluentassertionswebawesomeassertionsweb-examples)
- [Worked examples for Shouldly.Web](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-examples)