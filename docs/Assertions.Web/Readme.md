# Assertions.Web

The shared HTTP assertion contract behind the **Assertions.Web** family of packages:
[FluentAssertions.Web](https://www.nuget.org/packages/FluentAssertions.Web),
[FluentAssertions.Web.v8](https://www.nuget.org/packages/FluentAssertions.Web.v8),
[AwesomeAssertions.Web](https://www.nuget.org/packages/AwesomeAssertions.Web) and
[Shouldly.Web](https://www.nuget.org/packages/Shouldly.Web).

It provides assertions specific to HTTP responses and outputs rich error messages when the tests fail, so less time with debugging is spent. The assertion packages differ only in the framework they extend — FluentAssertions, AwesomeAssertions or Shouldly — and expose the same assertions through each framework's native syntax.

> There is no standalone `Assertions.Web` package to install: it is the shared contract packed into each assertion package. See the repository readme's [Package reference](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#package-reference) for the exact NuGet package ID and `dotnet add package` command of every integration.

For a complete, copy-pasteable integration test that combines the status code, a response header, the typed body and the Location header, see the repository readme's [End-to-end example](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#end-to-end-example).

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

## How the fluent API is composed

The FluentAssertions and AwesomeAssertions flavours share one fluent API. Every chain starts from the general response assertion returned by `response.Should()`. In `FluentAssertions.Web.v8` and `AwesomeAssertions.Web`, `Should()` returns `HttpResponseMessageAssertions` directly; in `FluentAssertions.Web` (FluentAssertions < 8) the same methods are extension methods over FluentAssertions' own response assertion type.

`HttpResponseMessageAssertions` holds the general assertions: status codes (`Be200Ok`, `Be4XXClientError`, ...), content (`BeAs`, `BeEmpty`, `MatchInContent`), `Satisfy`, and the header entry point `HaveHeader`. The specialized groups extend it:

```text
HttpResponseMessageAssertions        general response assertions
├── HeadersAssertions                assertions on one named header
│   └── LocationAssertions           HeadersAssertions bound to the Location header
└── BadRequestAssertions             validation errors of a Bad Request response
```

Because each specialized group derives from `HttpResponseMessageAssertions`, a specialized assertion never hides the general ones: after `.And` you can still assert the status, the body or `Satisfy`. A group only adds the methods for its own concern.

Every fluent assertion returns `AndConstraint<T>`, and `.And` yields `T`. The return type of a method — not its name — therefore determines which methods are available after `.And`. If a method is not offered after `.And`, the current group does not declare it; call an assertion that returns the group that does, or start a new `response.Should()` chain.

### Transitions between assertion groups

| Assertion | Returns, so `.And` exposes | What can follow |
| --- | --- | --- |
| `response.Should()` | `HttpResponseMessageAssertions` | any general or specialized assertion |
| `Be200Ok()`, `Be404NotFound()`, `Be5XXServerError()`, ... | `AndConstraint<HttpResponseMessageAssertions>` | status, content and `Satisfy` assertions |
| `Be201Created()`, `Be202Accepted()`, `Be204NoContent()`, `Be3XXRedirection()`, `Be409Conflict()`, `Be410Gone()`, ... | `AndConstraint<LocationAssertions>` | Location header assertions (`HaveLocation`, `BeValue`, `BeValues`, `Match`, ...) and the general assertions |
| `Be400BadRequest()` | `AndConstraint<BadRequestAssertions>` | validation assertions (`HaveError`, `OnlyHaveError`, `NotHaveError`, `HaveErrorMessage`) and the general assertions |
| `HaveHeader("X-Correlation-ID")` | `AndConstraint<HeadersAssertions>` | value assertions on that header (`BeValue`, `BeValues`, `BeEmpty`, `NotBeEmpty`, `Match`) and the general assertions |
| `HaveLocation()` on a `LocationAssertions` group | `AndConstraint<HeadersAssertions>` | value assertions on the `Location` header and the general assertions |
| `HaveError(...)`, `OnlyHaveError(...)`, `NotHaveError(...)`, `HaveErrorMessage(...)` | `AndConstraint<BadRequestAssertions>` | the other validation assertions and the general assertions |

`Be400BadRequest` is the only status assertion that returns `BadRequestAssertions`, and the status assertions listed above are the ones that return `LocationAssertions`. Every other status assertion returns the general `HttpResponseMessageAssertions`.

### Examples

General status and content assertions stay in the general group:

```csharp
response.Should()
    .Be200Ok()                                  // AndConstraint<HttpResponseMessageAssertions>
    .And.BeAs(new { Author = "John" });         // still the general group
```

A successful response with a `Location` header moves through two specialized groups:

```csharp
response.Should()
    .Be201Created()                             // AndConstraint<LocationAssertions>
    .And.HaveLocation()                         // AndConstraint<HeadersAssertions>, header = "Location"
    .And.Match("*/api/comments/*")              // AndConstraint<HeadersAssertions>
    .And.BeAs(new { Author = "John" });         // general assertions remain available
```

`HaveLocation()` returns `HeadersAssertions`, not `LocationAssertions`, but the header under assertion is still `Location`, so the following `Match` or `BeValue` targets it. It is declared on `LocationAssertions`, so in `FluentAssertions.Web.v8` and `AwesomeAssertions.Web` it is reached through a status assertion that returns that group (`Be201Created().And.HaveLocation()`), not from `response.Should()` or from a `HeadersAssertions` chain.

Validation assertions follow a Bad Request status:

```csharp
response.Should()
    .Be400BadRequest()                          // AndConstraint<BadRequestAssertions>
    .And.HaveError("Author", "*required*")      // AndConstraint<BadRequestAssertions>
    .And.HaveErrorMessage("*required*");        // general assertions are still available too
```

A header assertion is a separate concern and does not need the validation chain:

```csharp
response.Should().Be400BadRequest().And.HaveError("Author", "*required*");

response.Should()
    .HaveHeader("X-Correlation-ID")             // AndConstraint<HeadersAssertions>
    .And.Match("*-*");                          // targets X-Correlation-ID
```

Each `HaveHeader(name)` sets the header under test for the following header assertions, so consecutive `HaveHeader` calls switch the header being asserted. Because a chain stays in the returned group, starting a new `response.Should()` is the clearest way to move to an unrelated concern; it is a stylistic choice rather than a technical requirement for the general assertions, which every specialized group inherits.

### The Shouldly style: no groups, no chain

Shouldly.Web exposes the same assertions as extension methods on `HttpResponseMessage`. Every method returns `void`: there is no returned assertion object, no `.And`, and no transition between groups — each call is independent.

```csharp
response.ShouldBe201Created();                  // void
response.ShouldHaveLocation();                  // void
response.ShouldHaveLocationMatching("*/api/comments/*");
response.ShouldBeAs(new { Author = "John" });

response.ShouldBe400BadRequest();               // status first: the validation calls do not check it
response.ShouldHaveError("Author", "*required*");
response.ShouldOnlyHaveError("Author", "*required*");

response.ShouldHaveHeader("X-Correlation-ID");
response.ShouldHaveHeaderMatching("X-Correlation-ID", "*-*");
```

The `ShouldBe400BadRequest()` call is a real requirement, not just syntax: `ShouldHaveError` and the other validation methods only inspect the body, so the status must be asserted separately when it matters.

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

Like for the serializer, `AssertionsWebConfig.UseResponseFormatterOptions` overrides the response formatter options for the current async flow until the returned scope is disposed, in every flavour. Scopes nest and do not leak to other tests running in parallel. Inside a scope, reading `AssertionsWebConfig.ResponseFormatterOptions` returns the scoped options, while setting it still changes the global one:

```csharp
using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpResponseFormatterOptions
{
    PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
}))
{
    response.Should().Satisfy<Person>(person => person.FirstName.Should().Be("Ada"));
}
```

## The full API

The complete listing of every assertion, with the `dotnet test` failure output for each group, lives in the [repository readme](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md):

- [Full API (FluentAssertions / AwesomeAssertions names)](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#full-api)
- [End-to-end example in every flavour](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#end-to-end-example)
- [Shouldly.Web API (the flat Shouldly naming)](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-api)
- [Shouldly.Web: the Shouldly names for the same assertions](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb)
- [Worked examples](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#fluentassertionswebawesomeassertionsweb-examples)
- [Worked examples for Shouldly.Web](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#shouldlyweb-examples)