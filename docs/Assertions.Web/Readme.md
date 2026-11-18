# Assertions.Web

The shared HTTP assertion contract behind the **Assertions.Web** family of packages:
[FluentAssertions.Web](https://www.nuget.org/packages/FluentAssertions.Web),
[FluentAssertions.Web.v8](https://www.nuget.org/packages/FluentAssertions.Web.v8),
[AwesomeAssertions.Web](https://www.nuget.org/packages/AwesomeAssertions.Web) and
[Shouldly.Web](https://www.nuget.org/packages/Shouldly.Web).

It provides assertions specific to HTTP responses and outputs rich error messages when the tests fail, so less time with debugging is spent. The assertion packages differ only in the framework they extend — FluentAssertions, AwesomeAssertions or Shouldly — and expose the same assertions through each framework's native syntax.

> There is no standalone `Assertions.Web` package to install: it is the shared contract packed into each assertion package. See the repository readme's [Package reference](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#package-reference) for the exact NuGet package ID and `dotnet add package` command of every integration.

For a complete, copy-pasteable integration test that combines the status code, a response header, the typed body and the Location header, see the repository readme's [End-to-end example](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#end-to-end-example).

For developers or AI agents that want to add the library to a project and have tests written against it, see the repository readme's [Prompt for AI agents and rapid test authoring](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#prompt-for-ai-agents-and-rapid-test-authoring).

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

## Single-value and multi-value headers

`HttpHeaders` exposes each header as a collection of string values. The assertions use that collection exactly as .NET exposes it; they never split values on commas.

| FluentAssertions / AwesomeAssertions | Shouldly | Meaning |
| --- | --- | --- |
| `HaveHeader("Allow").And.BeValue("GET")` | `ShouldHaveHeaderWithValue("Allow", "GET")` | The header has **exactly one** value and that whole value is the expected one. Multiple values fail even if one matches. |
| `HaveHeader("Allow").And.BeValues(new[] { "GET", "POST" })` | `ShouldHaveHeaderWithValues("Allow", new[] { "GET", "POST" })` | The header's **complete value collection** is compared as a set; order is ignored. |
| `HaveHeader("Allow").And.Match("GET*")` | `ShouldHaveHeaderMatching("Allow", "GET*")` | **At least one** value matches the wildcard. |
| `HaveHeader("Allow").And.BeEmpty()` | `ShouldHaveEmptyHeader("Allow")` | The header has no values. |

A realistic `Allow` header shows why the distinction matters. When the header is added through the content-header API, .NET exposes `Allow: GET, POST` as two values:

```csharp
response.Content.Headers.TryAddWithoutValidation("Allow", new[] { "GET", "POST" });

// FluentAssertions / AwesomeAssertions
response.Should().HaveHeader("Allow").And.BeValues(new[] { "GET", "POST" });

// Shouldly
response.ShouldHaveHeaderWithValues("Allow", new[] { "GET", "POST" });
```

If the same text is added as a single raw value, .NET exposes one value and no assertion splits it:

```csharp
response.Headers.TryAddWithoutValidation("X-Allow-Raw", "GET, POST");

// Assert the one value that is actually present.
response.Should().HaveHeader("X-Allow-Raw").And.BeValue("GET, POST");
```

Use `BeValues` when a header legitimately carries several values, `BeValue` when exactly one is expected, and `Match` when any matching value is enough.

## Case-sensitivity map

There is no library-wide case switch. Each assertion follows the semantics of the thing it compares:

| Target | Contract | FluentAssertions / AwesomeAssertions | Shouldly |
| --- | --- | --- | --- |
| Header field name | Case-insensitive in **every** assertion, including `HaveHeader`, `NotHaveHeader`, `BeValue`, `BeValues`, `BeEmpty`, `NotBeEmpty`, `Match` and the `Location` variants. | `OrdinalIgnoreCase` lookup | `OrdinalIgnoreCase` lookup |
| One complete raw header value (`BeValue` / `ShouldHaveHeaderWithValue`) | Exactly one value, compared whole. Value casing is ignored for compatibility; whitespace is preserved and commas are not split. | Framework string `BeEquivalentTo` | `StringComparison.OrdinalIgnoreCase` |
| Header value collection (`BeValues` / `ShouldHaveHeaderWithValues`) | The complete exposed collection, in any order. Value elements are compared case-sensitively. | Framework collection equivalency defaults | `ShouldBe(..., ignoreOrder: true)` |
| Wildcard patterns (`Match`, `ShouldHaveHeaderMatching`, `MatchInContent`, `ShouldMatchInContent`, error-message patterns) | Anchored, case-sensitive text patterns. `*` matches any run and `?` matches one character. | Framework `Match` | Shared `WildcardMatch` (also culture-invariant) |
| Validation field names and JSON `errors` property | Case-insensitive, because these locate JSON members rather than compare field values. | `OrdinalIgnoreCase` JSON helpers | Same shared helpers |
| Deserialized property names | Controlled by the configured serializer, not by the assertion framework. | `System.Text.Json` uses `PropertyNameCaseInsensitive = true`; Newtonsoft matches case-insensitively by default. | Same shared serializer contract |
| Deserialized model string values | Framework equivalency defaults, case-sensitive unless the caller supplies options. | FluentAssertions / AwesomeAssertions equivalency | Shouldly equivalency |
| Status codes and `Satisfy` callbacks | Status codes have no casing. `Satisfy` bodies are arbitrary C#, so callback comparisons are caller-defined. | — | — |

HTTP field **names** are case-insensitive, so mixed-case lookups are intentional. Field **values** are not universally case-insensitive: `Allow` method tokens and opaque values such as ETags can be case-sensitive. The single-value APIs retain their historical ignore-case behavior for compatibility, so they are not field-specific semantic comparisons. Use `BeValues` for exposed token collections and `Satisfy(...)` for field-specific raw-value rules.

```csharp
response.Headers.TryAddWithoutValidation("x-trace-id", "mixed-Case");
response.Should().HaveHeader("X-TRACE-ID").And.BeValue("Mixed-case"); // name and value casing are ignored

response.Content.Headers.TryAddWithoutValidation("Allow", new[] { "GET", "POST" });
response.Should().HaveHeader("allow").And.BeValues(new[] { "GET", "POST" }); // name casing is ignored, token casing is not

response.Should().HaveHeader("content-type").And.Match("application/json*"); // wildcard casing must match
```

> .NET classifies `Allow` as a content header and rejects it on `HttpResponseHeaders`, even though HTTP defines it as response metadata. The shared lookup spans response and content headers, so the example above is the supported way to model it; the `Content-Type` assertion exercises the content-header path explicitly.

Shouldly uses the same contract through `ShouldHaveHeaderWithValue`, `ShouldHaveHeaderWithValues` and `ShouldHaveHeaderMatching`; only the method names change.

## How the fluent API is composed

The FluentAssertions and AwesomeAssertions flavours share one fluent API. Every chain starts from the general response assertion returned by `response.Should()`. In `FluentAssertions.Web.v8` and `AwesomeAssertions.Web`, `Should()` returns `HttpResponseMessageAssertions` directly; in `FluentAssertions.Web` (FluentAssertions < 8) the same methods are extension methods over FluentAssertions' own response assertion type.

`HttpResponseMessageAssertions` holds the general assertions: status codes (`Be200Ok`, `Be4XXClientError`, ...), content (`BeAs`, `BeEmpty`, `MatchInContent`), `Satisfy`, and the header entry point `HaveHeader`. The specialized groups extend it:

```text
HttpResponseMessageAssertions        general response assertions
├── HeadersAssertions                assertions on one named header
│   └── LocationAssertions           HeadersAssertions bound to the Location header
└── BadRequestAssertions             validation errors of a Bad Request response
```

Because each specialized group derives from `HttpResponseMessageAssertions`, general assertions remain reachable from `.And`. A specialized group can declare a method with the same name as a general one, and that method wins. For example, `HeadersAssertions.BeEmpty()` checks the values of the selected header, while `response.Should().BeEmpty()` checks response content. Start a new `response.Should()` chain to call the general version.

Every fluent assertion returns `AndConstraint<T>`, and `.And` yields `T`. The return type of a method — not its name — determines which methods are available after `.And`. If a method is not offered after `.And`, the current group does not declare it; call an assertion that returns the group that does, or start a new `response.Should()` chain.

### Transitions between assertion groups

| Assertion | Returns, so `.And` exposes | What can follow |
| --- | --- | --- |
| `response.Should()` in v8/AAV | `HttpResponseMessageAssertions` | status, content, `Satisfy`, `HaveHeader`, `NotHaveHeader` and `NotHaveLocation`; group-specific methods need an entry assertion |
| `response.Should()` in FA 6/7 | FluentAssertions' built-in response assertion | the extension equivalents of the general and specialized entry assertions; after the first custom assertion the custom groups below apply |
| General status assertions | `AndConstraint<HttpResponseMessageAssertions>` | status, content and `Satisfy` assertions |
| Location-bearing status assertions | `AndConstraint<LocationAssertions>` | `HaveLocation`, header-value methods targeting `Location`, and the general assertions |
| `Be400BadRequest()` | `AndConstraint<BadRequestAssertions>` | validation assertions and the general assertions |
| `HaveHeader(name)` from any group | `AndConstraint<HeadersAssertions>` | value assertions on that header and the general assertions |
| `HaveLocation()` on a `LocationAssertions` group | `AndConstraint<HeadersAssertions>` | value assertions on the `Location` header and the general assertions |
| `Match`, `BeValue`, `BeValues`, `BeEmpty`, `NotBeEmpty` on a header group | `AndConstraint<HeadersAssertions>` | other header-value methods on the same header and the general assertions |
| `HaveError`, `OnlyHaveError`, `NotHaveError`, `HaveErrorMessage` | `AndConstraint<BadRequestAssertions>` | other validation methods and the general assertions |
| `NotHaveHeader`, `NotHaveLocation`, content and `Satisfy` methods | `AndConstraint<HttpResponseMessageAssertions>` | status, content and `Satisfy` assertions |

`Be400BadRequest` is the only status assertion that returns `BadRequestAssertions`. The complete location-bearing set is `Be201Created`, `Be202Accepted`, `Be204NoContent`, `Be3XXRedirection`, `Be300MultipleChoices`, `Be300Ambiguous`, `Be301MovedPermanently`, `Be301Moved`, `Be302Found`, `Be302Redirect`, `Be303SeeOther`, `Be303RedirectMethod`, `Be305UseProxy`, `Be307TemporaryRedirect`, `Be307RedirectKeepVerb`, `Be308PermanentRedirect`, `Be409Conflict` and `Be410Gone`. Every other status assertion, including `Be304NotModified()`, returns the general group; use `HaveHeader("Location")` for a 304 response whose `Location` header must be inspected.

Returning a specialized group does not assert its precondition. `Be201Created().And.HaveLocation()` is what asserts that the header is present. `HaveLocation()` itself returns `HeadersAssertions`, so a second `HaveLocation()` is not available until a new chain reaches `LocationAssertions` again. The same rule applies when a validation chain needs the `Location` group or when a header chain needs a status that returns it.

### The two `BeEmpty()` targets

`BeEmpty()` has the same name in two different assertion groups. The chain target, not the method name, decides what is checked:

| Call | Target | Meaning |
| --- | --- | --- |
| `response.Should().BeEmpty()` | response content | The response body is null or an empty string. |
| `response.Should().HaveHeader("X-Optional").And.BeEmpty()` | the selected header | `X-Optional` is present but has no values. It says nothing about the body. |
| `response.Should().Be201Created().And.HaveLocation().And.BeEmpty()` | the `Location` header | `Location` is present but has no values. It says nothing about the body. The same target applies to `Be201Created().And.BeEmpty()` because `LocationAssertions` inherits the header method. |
| `response.Should().Be400BadRequest().And.BeEmpty()` | response content | `BadRequestAssertions` does not declare `BeEmpty`, so the inherited content assertion is used. |
| `response.ShouldHaveEmptyHeader("X-Optional")` | the selected header | Shouldly's explicit name has the same header-value meaning. |
| `response.ShouldBeEmpty()` | response content | Shouldly's response-body assertion. |

To assert both in one test, keep the header assertion in its group and start a fresh chain for the body:

```csharp
response.Should()
    .HaveHeader("X-Optional")
    .And.BeEmpty();        // header values

response.Should()
    .BeEmpty();            // response content
```

```csharp
response.ShouldHaveEmptyHeader("X-Optional"); // header values
response.ShouldBeEmpty();                     // response content
```

`HeadersAssertions.BeEmpty()` deliberately does **not** mean “the response has no headers”; that would be a different assertion. Renaming it would improve discoverability but would break source and binary compatibility across the fluent packages, and `HaveNoHeaders()` would misstate the current semantics. The name is retained; the shipped XML documentation and this table make the target explicit.

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

#### Common combinations

These combinations were checked against the public signatures in every fluent flavour. Shouldly uses the same assertions as independent calls, as shown in [The Shouldly style](#the-shouldly-style-no-groups-no-chain).

| Goal | FluentAssertions / AwesomeAssertions |
| --- | --- |
| Status and body | `response.Should().Be200Ok().And.BeAs(model);` |
| Created status, `Location` presence and value | `response.Should().Be201Created().And.HaveLocation().And.Match("*/api/comments/*");` |
| Created status, `Location` value and body | `response.Should().Be201Created().And.HaveLocation().And.BeValue(url).And.BeAs(model);` |
| Two different headers | `response.Should().HaveHeader("A").And.Match("a*").And.HaveHeader("B").And.Match("b*");` |
| Validation errors and an unrelated header | Assert the errors in one chain, then `response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*");` |
| A 304 response with a `Location` header | `response.Should().Be304NotModified().And.HaveHeader("Location").And.NotBeEmpty();` |

A new chain is required when the next assertion only exists on a group the current chain cannot reach: `HaveLocation` after a header or validation group, or a second `HaveLocation` after the first one has returned `HeadersAssertions`. General methods such as `BeAs` remain available from every group, so no restart is needed for them.

No return type was changed as part of this review. Returning a broader group would be a source and binary breaking change, and moving group-specific entry points onto the base type would weaken the preconditions that the current groups express. The FA 6/7 extensions can still reach `HaveLocation` and the validation methods directly from the initial `response.Should()`; always call `Be400BadRequest()` before validation assertions so the same test works unchanged in v8 and AwesomeAssertions.

## Choosing between `BeAs<T>()` and `Satisfy<T>()`

`Satisfy` is a general-purpose C# extension point: its delegate body can contain arbitrary C# code, not just assertion calls. It can inspect values, perform calculations, apply conditional logic, loop, call helpers, use any framework assertion, and use `await` in the async overloads. When code inside that body fails, the library reports it together with the complete request/response conversation. Preserving HTTP context is a library-wide capability, but it is especially valuable here: arbitrary test code stays inside the HTTP assertion. Manual deserialization followed by plain framework assertions loses that context.

The [worked example](../../readme.md#real-test-logic-inside-satisfy) calculates a total, checks a field conditionally and validates a discount code in the same lambda; only the method name differs between FA/AWE and Shouldly.

The narrower APIs are secondary guidance:

| Goal | FluentAssertions / AwesomeAssertions | Shouldly |
| --- | --- | --- |
| Run arbitrary C# over the deserialized body | `response.Should().Satisfy<TModel>(model => ...);` | `response.ShouldSatisfy<TModel>(model => ...);` |
| Run arbitrary C# over the response itself | `response.Should().Satisfy(response => ...);` | `response.ShouldSatisfy(response => ...);` |
| Compare the complete expected body | `response.Should().BeAs(expectedModel);` | `response.ShouldBeAs(expectedModel);` |
| Assert raw wire text | `response.Should().MatchInContent(wildcard);` | `response.ShouldMatchInContent(wildcard);` |

`BeAs<T>()` deserializes into `expectedModel.GetType()` and compares the complete object graph with the installed framework's equivalency engine; JSON properties that do not map to the expected model are not compared. The optional equivalency configuration is framework-specific and affects the result. Model `Satisfy<T>()` deserializes into `TModel` and runs the callback; it does not perform whole-object equivalency. Its optional `givenModelStructure` parameter is only a type-inference aid and its values are never used.

A validation `ProblemDetails` response has standard members and generated extension data such as `traceId`. A custom DTO with a concrete `TraceId` property is clearest when the whole body is deterministic; otherwise model `ProblemDetails.Extensions` with `[JsonExtensionData]` and assert the value through `Satisfy<T>()`. With the default serializer, extension values are `JsonElement`; custom serializers may materialize them differently, so a typed DTO is usually easier for `BeAs<T>()` equivalency.

```csharp
internal sealed class ExpectedProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? TraceId { get; set; }
}

response.Should().BeAs(new ExpectedProblemDetails
{
    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    Title = "One or more validation errors occurred.",
    Status = 400,
    Errors = new Dictionary<string, string[]>
    {
        ["Author"] = new[] { "The Author field is required." }
    },
    TraceId = "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
});
```

The Shouldly spelling is `response.ShouldBeAs(expectedModel)`. When `traceId` is generated per request, exclude it from equivalency (`options.Excluding(p => p.TraceId)` in the fluent flavours, or `EquivalencyOptions.MembersToIgnore` in Shouldly) and check it separately.

For a subset plus extension data, use `[JsonExtensionData]` and `Satisfy<T>()`:

```csharp
internal sealed class ProblemDetailsWithExtensions
{
    public int? Status { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extensions { get; set; }
}

// FluentAssertions / AwesomeAssertions
response.Should().Satisfy<ProblemDetailsWithExtensions>(problem =>
{
    problem.Status.Should().Be(400);
    var errors = problem.Errors;
    errors.Should().NotBeNull();
    errors!.Should().ContainKey("Author");
    var extensions = problem.Extensions;
    extensions.Should().NotBeNull();
    extensions!.Should().ContainKey("traceId");
    extensions["traceId"].GetString().Should().StartWith("00-");
});

// Shouldly
response.ShouldSatisfy<ProblemDetailsWithExtensions>(problem =>
{
    problem.Status.ShouldBe(400);
    var errors = problem.Errors;
    errors.ShouldNotBeNull();
    errors.ShouldContainKey("Author");
    var extensions = problem.Extensions;
    extensions.ShouldNotBeNull();
    extensions.ShouldContainKey("traceId");
    extensions["traceId"].GetString().ShouldStartWith("00-");
});
```

The failure boundary is identical across integrations:

| Stage | Diagnostic |
| --- | --- |
| Deserialization fails | Both APIs report **“the JSON representation could not be parsed”** and include the serializer exception text (Shouldly uses its **“should be as”** layout). |
| Equivalency/custom assertion fails | `BeAs<T>()` reports **“has differences”** with equivalency details; `Satisfy<T>()` reports inner failures under **“satisfy one or more model assertions”** (Shouldly: **“should satisfy all the conditions specified”** with numbered errors). |
| A `Satisfy<T>()` condition throws unexpectedly | The exception text is captured in the assertion failure instead of escaping as an unrelated crash. |

Every failure includes the complete request/response dump. See the root README's [full ProblemDetails examples](https://github.com/adrianiftode/Assertions.Web/blob/master/readme.md#choosing-between-beast-and-satisfyt) for nullable handling, volatile-value exclusion and multi-condition Shouldly assertions.

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