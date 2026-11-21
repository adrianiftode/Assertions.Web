## Assertions.Web

HTTP assertions for .NET that turn a failing test into the whole request and response conversation, so you debug less.

The repository is home to a family of assertion frameworks on top of the shared [`Assertions.Web`](https://github.com/adrianiftode/Assertions.Web/tree/master/src/Assertions.Web) contract and the [`HttpMessageFormatter`](https://github.com/adrianiftode/Assertions.Web/tree/master/src/HttpMessageFormatter) request/response renderer.

### Status

[![Build status](https://ci.appveyor.com/api/projects/status/93qtbyftww0snl4x/branch/master?svg=true)](https://ci.appveyor.com/project/adrianiftode/fluentassertions-web/branch/master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=adrianiftode_FluentAssertions.Web&metric=alert_status)](https://sonarcloud.io/project/overview?id=adrianiftode_FluentAssertions.Web)

[![NuGet](https://img.shields.io/nuget/v/FluentAssertions.Web.svg?label=FluentAssertions.Web)](https://www.nuget.org/packages/FluentAssertions.Web)
[![NuGet FA v8](https://img.shields.io/nuget/v/FluentAssertions.Web.v8.svg?label=FluentAssertions.Web.v8)](https://www.nuget.org/packages/FluentAssertions.Web.v8)

[![NuGet AwesomeAssertions](https://img.shields.io/nuget/v/AwesomeAssertions.Web.svg?label=AwesomeAssertions.Web)](https://www.nuget.org/packages/AwesomeAssertions.Web)

[![NuGet Shouldly.Web](https://img.shields.io/nuget/v/Shouldly.Web.svg?label=Shouldly.Web)](https://www.nuget.org/packages/Shouldly.Web)

[![NuGet HttpMessageFormatter](https://img.shields.io/nuget/v/HttpMessageFormatter.svg?label=HttpMessageFormatter)](https://www.nuget.org/packages/HttpMessageFormatter/)

## Package reference

The repository and product are called **Assertions.Web**, but there is no standalone `Assertions.Web` package to install. `Assertions.Web` is the shared contract that is packed into every assertion package. Install the package named after the assertion framework your tests already use; no integration is the default. The assertion-framework packages are listed alphabetically.

| Assertion framework | NuGet package ID and install | Syntax / composition | Documentation |
|---|---|---|---|
| AwesomeAssertions | [`AwesomeAssertions.Web`](https://www.nuget.org/packages/AwesomeAssertions.Web) — `dotnet add package AwesomeAssertions.Web` | Fluent `response.Should().…And…`; `using AwesomeAssertions;` | [Quick start](#quick-start-awesomeassertionsweb) · [Usage](#awesomeassertionsweb) · [End-to-end](#end-to-end-example-fluentassertions-and-awesomeassertions) |
| FluentAssertions < 8.0.0 | [`FluentAssertions.Web`](https://www.nuget.org/packages/FluentAssertions.Web) — `dotnet add package FluentAssertions.Web` | Fluent `response.Should().…And…`; `using FluentAssertions;`. Some FA 6/7-only entry points from `Should()` are not portable to v8. | [Quick start](#quick-start-fluentassertionsweb) · [Usage](#fluentassertionsweb) · [End-to-end](#end-to-end-example-fluentassertions-and-awesomeassertions) |
| FluentAssertions >= 8.0.0 (commercial) | [`FluentAssertions.Web.v8`](https://www.nuget.org/packages/FluentAssertions.Web.v8) — `dotnet add package FluentAssertions.Web.v8` | The same fluent chain and namespace as FA 6/7; prefer the portable status-first paths shown in the canonical example. | [Quick start](#quick-start-fluentassertionswebv8) · [Usage](#fluentassertionswebv8) · [End-to-end](#end-to-end-example-fluentassertions-and-awesomeassertions) |
| Shouldly >= 5.0.0-preview.2 | [`Shouldly.Web`](https://www.nuget.org/packages/Shouldly.Web) — `dotnet add package Shouldly.Web --prerelease` | Flat `response.ShouldXxx();`; `using Shouldly;`; no `.And`. | [Quick start](#quick-start-shouldlyweb) · [Usage](#shouldlyweb) · [End-to-end](#end-to-end-assertion-example-shouldlyweb) |

> `Shouldly.Web` is currently a prerelease, so `--prerelease` is required until Shouldly 5.0.0 is stable.

Two secondary packages complete the ecosystem, without being assertion libraries themselves:

| Package | Install | When to use |
|---|---|---|
| [`Assertions.Web.Serializers.NewtonsoftJson`](https://www.nuget.org/packages/Assertions.Web.Serializers.NewtonsoftJson) | `dotnet add package Assertions.Web.Serializers.NewtonsoftJson` | *Optional*: every assertion library already ships with `System.Text.Json` deserialization built in; use this package only to switch the default serializer to Newtonsoft.Json — see [Optional Global Configuration](#optional-global-configuration). |
| [`HttpMessageFormatter`](https://www.nuget.org/packages/HttpMessageFormatter) | `dotnet add package HttpMessageFormatter` | The *shared* rendering engine behind all four assertion libraries. It has no assertions and no dependency on any assertion framework, so it can format HTTP messages in any other context too — see [HttpResponse Formatter](#httpresponse-formatter). |

Every assertion package targets `netstandard2.0`, packs its own copy of the `Assertions.Web` contract and renders failure output with the shared `HttpMessageFormatter`. The packages differ only in the assertion framework they extend; the assertions themselves are the same set, exposed through each framework's native syntax. Additional assertion-framework integrations can be added as new packages without changing the structure of this reference.

For a complete, copy-pasteable integration test that combines the status code, a response header, the typed body and the Location header, see [End-to-end example](#end-to-end-example).

For the composition rules — the general entry point, the specialized assertion groups, the type each assertion returns and what `.And` exposes next — see [How the fluent API is composed](docs/Assertions.Web/Readme.md#how-the-fluent-api-is-composed).

## Quick starts

### Quick start: FluentAssertions.Web

```csharp
response.Should().Be200Ok();
response.Should().BeAs(new { Author = "John", Content = "Hey, you..." });
response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*");
response.Should().Satisfy<IEnumerable<Comment>>(comments => comments.Should().HaveCount(2));
```

```shell
dotnet add package FluentAssertions.Web
```

### Quick start: FluentAssertions.Web.v8

The same code as above (starting with FluentAssertions 8.0.0 FluentAssertions is a commercial product, so it ships as a separate package depending on the commercial versions):

```shell
dotnet add package FluentAssertions.Web.v8
```

### Quick start: AwesomeAssertions.Web

`Should()` and `And` work exactly like in the FluentAssertions flavour:

```shell
dotnet add package AwesomeAssertions.Web
```

### Quick start: Shouldly.Web

With Shouldly the assertions are called directly on the response and there is no `Should()`/`And` chaining — each assertion is its own call:

```csharp
response.ShouldBe200Ok();
response.ShouldBeAs(new { Author = "John", Content = "Hey, you..." });
response.ShouldHaveHeader("X-Correlation-ID");
response.ShouldMatchInContent("*\"author\"*");
```

> Prerelease: Shouldly.Web is in preview until Shouldly 5.0.0 is stable, and requires Shouldly >= 5.0.0-preview.2 (`EquivalencyOptions`).

```shell
dotnet add package Shouldly.Web --prerelease
```

### Quick start: Newtonsoft serializer

> Optional: the assertion libraries already ship the default `System.Text.Json` serializer. Install this package only to switch the default to Newtonsoft.Json.

```shell
dotnet add package Assertions.Web.Serializers.NewtonsoftJson
```

```csharp
AssertionsWebConfig.Serializer = new NewtonsoftJsonSerializer();
```

### Quick start: HttpMessageFormatter

> Shared: this is the rendering engine behind the assertion libraries. It carries no assertion framework dependency, so it can be used standalone, in any context.

```shell
dotnet add package HttpMessageFormatter
```

```csharp
using HttpMessageFormatter;

var formatted = response.Format();
```

## End-to-end example

One canonical fluent integration test covers FluentAssertions 6/7, FluentAssertions v8 and AwesomeAssertions; an equivalent Shouldly test shows that framework's standalone composition. The scenario is the normal usage path of an HTTP API: `POST /api/comments` creates a comment, and the response carries

- a `201 Created` status code,
- an `X-Correlation-ID` response header,
- the created comment as typed JSON, and
- a `Location` header that points at the created resource.

The fixture and the Arrange/Act parts are identical in every flavour; only the `Assert` block and the `using` directive change. No flavour is the default — use the package for the assertion framework your tests already use.

The examples share this framework-neutral fixture (`using Microsoft.AspNetCore.Mvc.Testing;`). Put the `[Fact]` of the flavour you use inside it:

```csharp
public class CommentsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CommentsApiTests(WebApplicationFactory<Program> factory) => _factory = factory;
}
```

> `Program` is the API's entry point. If it is not visible to the test project, add `public partial class Program { }` to the API project, which is the usual `WebApplicationFactory` setup.

### End-to-end example: FluentAssertions and AwesomeAssertions

The fluent API is the same across `FluentAssertions.Web`, `FluentAssertions.Web.v8` and `AwesomeAssertions.Web`. The following test uses the FluentAssertions namespace; use the package and namespace that match the assertion framework already used by your test project:

- `FluentAssertions.Web`: `using FluentAssertions;`
- `FluentAssertions.Web.v8`: `using FluentAssertions;`
- `AwesomeAssertions.Web`: `using AwesomeAssertions;`

The fixture and Arrange/Act are shared by all flavours:

```csharp
using System.Text;
using FluentAssertions;

[Fact]
public async Task Post_ReturnsCreated_WithHeader_Location_AndTypedBody()
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
    response.Should().Be201Created()
        .And.HaveLocation().And.Match("*/api/Comments/*")
        .And.HaveHeader("X-Correlation-ID").And.Match("*-*")
        .And.BeAs(new { Author = "John", Content = "Hey, you..." });
}
```

`HaveLocation()` must come before switching to another header: it is declared on the `LocationAssertions` group, and `HaveHeader("X-Correlation-ID")` moves the chain to `HeadersAssertions`. See [How the fluent API is composed](docs/Assertions.Web/Readme.md#how-the-fluent-api-is-composed) for the full transition map.

For `AwesomeAssertions.Web`, change only the using directive to `using AwesomeAssertions;`; the assertion statements above are identical. `FluentAssertions.Web.v8` uses the same code and namespace as `FluentAssertions.Web`; it targets FluentAssertions >= 8.0.0. In FluentAssertions < 8 the built-in `response.Should()` also offers a `HaveLocation()` extension directly, but the chained form shown here works in all three fluent flavours.

### End-to-end assertion example: Shouldly.Web

Shouldly uses independent extension methods rather than a fluent `Should()`/`And` chain. Reuse the same fixture and Arrange/Act above, add `using Shouldly;`, and replace only the Assert block:

```csharp
using Shouldly;

response.ShouldBe201Created();
response.ShouldHaveHeaderMatching("X-Correlation-ID", "*-*");
response.ShouldHaveLocationMatching("*/api/Comments/*");
response.ShouldBeAs(new { Author = "John", Content = "Hey, you..." });
```

### Flavour differences

All four flavours support every assertion in this scenario, so no flavour needs a different scenario or omits a step. The genuine differences are syntax and packaging:

- **FluentAssertions.Web and FluentAssertions.Web.v8** expose the same `Should()`/`And` chain. The assertions and the `using FluentAssertions;` directive are identical; only the package and the FluentAssertions version it targets differ.
- **AwesomeAssertions.Web** exposes the same `Should()`/`And` chain too. Only the package and `using AwesomeAssertions;` change.
- **Shouldly.Web** has no `Should()`/`And` chain because its assertions return `void`, so each assertion is its own call. `Match` on a named header becomes `ShouldHaveHeaderMatching`, `HaveLocation().And.Match(...)` becomes `ShouldHaveLocationMatching`, and `BeAs` becomes `ShouldBeAs`.
- **Shouldly.Web is a prerelease** until Shouldly 5.0.0 is stable, so install it with `dotnet add package Shouldly.Web --prerelease`.
- The typed body assertion accepts any POCO or record, not only the anonymous type used here.

## When a test fails, you see the whole conversation

Every failure message produced by this library is followed by **the complete HTTP response _and_ the originating HTTP request** — status line, headers and both bodies, the same way an HTTP interceptor like Fiddler would render them. There is no need to attach a debugger or to watch `response.Content.ReadAsStringAsync().Result` anymore: the reason for the failure and the payload that caused it are already in the test output.

A typical test:

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

Running this with `dotnet test` against an endpoint that answers `201 Created` instead of `200 OK` prints:

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_ReturnsOk [614 ms]
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
  Stack Trace:
     at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
   at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
   at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
   at FluentAssertions.Web.HttpResponseMessageAssertions.Be200Ok(String because, Object[] becauseArgs)
   at Sample.Api.Tests.CommentsControllerTests.Post_ReturnsOk()
--- End of stack trace from previous location ---
```

The same text is shown in the *Test Detail Summary* of Visual Studio and Rider, and ends up in the CI logs, so the failure can usually be assessed without even running the test locally:

![FailedTest1](https://github.com/adrianiftode/Assertions.Web/blob/master/docs/images/FailedTest1.png?raw=true)

A couple of details about that output:

- when there is no originating request (for example because the `HttpResponseMessage` was created by hand), the last section reads `The originating HTTP request was <null>.`
- when the response content is disposed it is reported as `***** Content is disposed so it cannot be read. *****`
- when the response content is bigger than `ResponseFormatterOptions.MaximumReadableBytes` (1.25 MB by default), it is truncated and a `***** Content is too large to display and only a part is printed. *****` warning is printed instead — see [Response Formatting](#response-formatting)

## Why?

Writing tests for ASP.NET Core APIs and or any APIs by using the HttpClient classes leads to some repetitive code and also often to an incomplete one. When the test fails, the developer needs to debug the test in order to assess the failure reason, as the test itself did not usually consider what happens in this case.

Thus this library solves two problems:

##### Focus on the Assert part and not on the HttpClient related APIs, neither on the response deserialization

Once the response is ready you'll want to assert it. With first level properties like `StatusCode` is somehow easy, especially with FluentAssertions/AwesomeAssertions, but often we need more, like to deserialize the content into an object of a certain type and then to Assert it. Or to simply assert something about the response content itself. Soon duplication code occurs and the urge to reduce it is just the next logical step.

##### Debugging failed tests interrupts the programmer's flow state
 When a test is failing, the following actions are taken most of the time:
- attach the debugger to the line containing ```var response = await client..```
- debug the failing test
- add an Watch for ``` response.Content.ReadAsStringAsync().Result ``` and see the actual response content

**And this can be avoided**, if the *Test Detail Summary* contains the request and the response information, providing a similar experience as with an HTTP interceptor like Fiddler. See [When a test fails, you see the whole conversation](#when-a-test-fails-you-see-the-whole-conversation) for the exact output.

## Migrating to 3.0

Version 3.0 consolidates the configuration and the Newtonsoft.Json serializer into flavour-agnostic packages. The changes are breaking but mechanical — there are no compatibility shims — so an upgrade is a package swap plus a find-and-replace of the config holder.

### 1. The config holder is now `AssertionsWebConfig`

The per-flavour holders `FluentAssertionsWebConfig` (FluentAssertions.Web / .v8) and `AwesomeAssertionsWebConfig` (AwesomeAssertions.Web) are replaced by the single `AssertionsWebConfig` in the `Assertions.Web` namespace, referenced by every flavour. It carries the same two members, with the same defaults and the same null guards: `Serializer` and `ResponseFormatterOptions`.

**Before (2.x):**

```csharp
FluentAssertionsWebConfig.ResponseFormatterOptions =
    new HttpResponseFormatterOptions { MaximumReadableBytes = 1024 };

// or, on the AwesomeAssertions.Web flavour:
AwesomeAssertionsWebConfig.ResponseFormatterOptions =
    new HttpResponseFormatterOptions { MaximumReadableBytes = 1024 };
```

**After (3.0):**

```csharp
using Assertions.Web;

AssertionsWebConfig.ResponseFormatterOptions =
    new HttpResponseFormatterOptions { MaximumReadableBytes = 1024 };
```

The same replacement applies to every usage in [Optional Global Configuration](#optional-global-configuration).

### 2. One shared Newtonsoft.Json serializer package

`FluentAssertions.Web.Serializers.NewtonsoftJson` and `AwesomeAssertions.Web.Serializers.NewtonsoftJson` are replaced by the single **`Assertions.Web.Serializers.NewtonsoftJson`** package, and its types moved to the `Assertions.Web` namespace — the same `using` as the config holder:

```shell
dotnet remove package FluentAssertions.Web.Serializers.NewtonsoftJson  # on whichever flavour(s) you used
dotnet add package Assertions.Web.Serializers.NewtonsoftJson
```

```csharp
using Assertions.Web;

AssertionsWebConfig.Serializer = new NewtonsoftJsonSerializer();
NewtonsoftJsonSerializerConfig.Options.Converters.Add(new YesNoBooleanJsonConverter());
```

### 3. Other types that moved to `Assertions.Web`

`ISerializer`, `DeserializationException` and `SystemTextJsonSerializerConfig` (the default `System.Text.Json` serializer's options) are also in the `Assertions.Web` namespace now — add `using Assertions.Web;` where you referenced them.

### What did not change

- The assertions themselves: every `Should()`/`And` method, the status-code assertions, `BeAs`, `Satisfy`, `Match`, the header assertions and the deserializers behave exactly as before, with the same failure output.
- **HttpMessageFormatter** keeps its own version and is unaffected.
- **Shouldly.Web** is new in this release and stays prerelease (`1.0.0-preview.x`) until Shouldly 5.0.0 is stable.

## Full documentation

### Assertions at a glance

```csharp
response.Should().Be200Ok();                                               // status codes: Be200Ok, Be404NotFound, Be400BadRequest, Be5XXServerError, ...
response.Should().Be400BadRequest().And.HaveError("Author", "*required*"); // validation errors carried by a 400 response
response.Should().BeAs(new { Author = "John" });                           // the body is equivalent to an object
response.Should().Satisfy<IEnumerable<Comment>>(comments =>               // any assertions over the deserialized body
    comments.Should().HaveCount(2).And.OnlyHaveUniqueItems(c => c.CommentId));
response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*");         // headers
response.Should().MatchInContent("*\"author\"*");                          // raw content
```

For the Shouldly flavour this is the same set of assertions, written Shouldly-style — see [Shouldly.Web](#shouldlyweb) below. The [end-to-end example](#end-to-end-example) combines the status, header, typed body and Location assertions in one test for every flavour.

### How the fluent API is composed

[How the fluent API is composed](docs/Assertions.Web/Readme.md#how-the-fluent-api-is-composed) explains which assertion group each method returns, what `.And` exposes next, and how the Shouldly integration expresses the same assertions without chaining.

### Case-sensitivity map

[Case-sensitivity map](docs/Assertions.Web/Readme.md#case-sensitivity-map) records the comparison rule for header names, header values, wildcard patterns, validation fields, body text and deserialized models. Header names are always case-insensitive; value and model rules are intentionally method- and framework-specific.

### Choosing between `BeAs<T>()` and `Satisfy<T>()`

`Satisfy` is deliberately an escape hatch: its delegate body can contain arbitrary C# code, not just assertion calls. It is a general-purpose extension point, not an assertion DSL. It can inspect values, calculate results, branch, loop, call helper methods, use any framework assertion, and use `await` in the async overloads. When something inside that body fails, Assertions.Web reports it together with the complete request/response conversation. If the same logic were run after manually deserializing the response and using ordinary assertions, that HTTP context would be lost. Preserving the conversation is a library-wide capability, but it is especially valuable here because `Satisfy` keeps otherwise arbitrary test code inside that context.

The narrower APIs are secondary guidance:

| Call | Use when | What happens |
| --- | --- | --- |
| `Satisfy<TModel>(model => ...)` | test logic needs arbitrary C# over a deserialized model | The body is deserialized into `TModel` and the callback runs. No whole-object equivalency is performed. |
| `Satisfy(...)` on `HttpResponseMessage` | test logic needs arbitrary C# over the response itself | No deserialization occurs. |
| `BeAs(expectedModel)` | the complete expected body is known | The body is deserialized into `expectedModel.GetType()` and the whole object graph is compared with the framework's equivalency engine. JSON properties that do not map to the expected model are not compared. |
| `MatchInContent(wildcard)` | the raw wire text matters | The body is matched as text without deserialization. |

#### Real test logic inside `Satisfy`

The following callback calculates a total, conditionally checks a field, and validates a discount code in the same lambda. None of that is special assertion syntax, but a failure still carries the HTTP conversation:

```csharp
internal sealed class CartResponse
{
    public string? Status { get; set; }
    public decimal Total { get; set; }
    public string? DiscountCode { get; set; }
    public List<CartItem> Items { get; set; } = new();
}

internal sealed class CartItem
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

response.Should().Satisfy<CartResponse>(cart =>
{
    var calculatedTotal = cart.Items.Sum(item => item.Price * item.Quantity);
    if (cart.Total != calculatedTotal)
    {
        throw new InvalidOperationException($"The total was {cart.Total}, but the items add up to {calculatedTotal}.");
    }

    if (string.Equals(cart.Status, "Discounted", StringComparison.OrdinalIgnoreCase))
    {
        var discountCode = cart.DiscountCode;
        if (string.IsNullOrWhiteSpace(discountCode) || discountCode.Length != 8)
        {
            throw new InvalidOperationException("A discounted cart must have an eight-character discount code.");
        }
    }
});
```

Shouldly uses the same lambda with `response.ShouldSatisfy<CartResponse>(...)`; only the method name changes. `Satisfy` on the response itself follows the same rule with `HttpResponseMessage` as the callback argument; it does not deserialize the body. The logic is still ordinary C# and its failures still retain the HTTP conversation.

The optional configuration passed to `BeAs<T>()` is handed to the installed assertion framework's equivalency engine. Framework defaults and configuration — member selection, strictness, collection ordering, and similar rules — therefore affect the result. The lambda shape is the same in FA 6/7, FA v8 and AwesomeAssertions; Shouldly takes an `EquivalencyOptions` instance.

The failure boundary is the same in every integration:

| Stage | Diagnostic the caller gets |
| --- | --- |
| Body cannot be deserialized | Both APIs report **“the JSON representation could not be parsed”** and include the serializer's exception text. Shouldly uses its **“should be as”** layout with the same serializer message. |
| Deserialization succeeds, equivalency or custom assertion fails | `BeAs<T>()` reports **“has differences”** with the framework's equivalency details; `Satisfy<T>()` reports inner failures under **“satisfy one or more model assertions”**. Shouldly reports **“should satisfy all the conditions specified”** with numbered errors. |
| Unexpected exception inside a `Satisfy<T>()` condition | It is captured and reported as an assertion failure with the exception text rather than escaping as an unrelated test crash. |

Every failure is followed by the complete request/response dump, so the payload that produced the diagnostic is available without a debugger.

#### A ProblemDetails response with standard and additional properties

ASP.NET Core's validation `ProblemDetails` payload contains standard members plus generated extension data such as `traceId`:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Author": [ "The Author field is required." ]
  },
  "traceId": "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
}
```

There are two ways to expose an extension member. A custom DTO with a concrete `TraceId` property, as below, is the clearest choice when the name and type are part of the contract. Alternatively, model the standard `ProblemDetails.Extensions` dictionary with `[JsonExtensionData]`; with the default serializer its values are `JsonElement`, while a custom serializer may materialize them differently, so the DTO is usually easier for `BeAs<T>()` equivalency.

```csharp
internal sealed class ExpectedProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? TraceId { get; set; }
}

// FluentAssertions / AwesomeAssertions
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

// Shouldly
response.ShouldBeAs(new ExpectedProblemDetails { /* same values */ });
```

Use `BeAs<T>()` here when a change to any of those values should fail the test. With a generated `traceId`, assert the standard fields by equivalency and exclude the volatile member, then check the extension separately:

```csharp
var expected = new ExpectedProblemDetails
{
    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    Title = "One or more validation errors occurred.",
    Status = 400,
    Errors = new Dictionary<string, string[]>
    {
        ["Author"] = new[] { "The Author field is required." }
    }
};

// FluentAssertions / AwesomeAssertions
response.Should().BeAs(expected, options => options.Excluding(p => p.TraceId));

// Shouldly
response.ShouldBeAs(expected, new EquivalencyOptions
{
    MembersToIgnore = { "TraceId" }
});
```

Use `Satisfy<T>()` when the test should inspect selected standard fields and extension data without stating the entire expected graph. `JsonExtensionData` exposes non-standard members such as `traceId`:

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
    errors["Author"].Should().ContainSingle()
        .Which.Should().Match("*required*");

    var extensions = problem.Extensions;
    extensions.Should().NotBeNull();
    extensions!.Should().ContainKey("traceId");
    var traceId = extensions["traceId"].GetString();
    traceId.Should().NotBeNullOrWhiteSpace();
    traceId.Should().StartWith("00-");
});

// Shouldly
response.ShouldSatisfy<ProblemDetailsWithExtensions>(problem =>
{
    problem.Status.ShouldBe(400);
    var errors = problem.Errors;
    errors.ShouldNotBeNull();
    errors.ShouldContainKey("Author");
    errors["Author"][0].ShouldContain("required");

    var extensions = problem.Extensions;
    extensions.ShouldNotBeNull();
    extensions.ShouldContainKey("traceId");
    var traceId = extensions["traceId"].GetString();
    traceId.ShouldNotBeNullOrEmpty();
    traceId.ShouldStartWith("00-");
});
```

In Shouldly, pass a collection of `Action<ProblemDetailsWithExtensions>` conditions to `ShouldSatisfy` when every failed condition should be reported; one statement lambda stops at its first failure. The optional `givenModelStructure` parameter on the fluent and Shouldly `Satisfy` overloads is only a type-inference aid; its values are never used.

### FluentAssertions.Web/AwesomeAssertions.Web Examples

- Asserting that the response content of an HTTP POST request is equivalent to a certain object

```csharp
[Fact]
public async Task Post_ReturnsOkAndWithContent()
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
    response.Should().BeAs(new
    {
        Author = "John",
        Content = "Hey, you..."
    });
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the API persisted a different <code>content</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_ReturnsOkAndWithContent [41 ms]
  Error Message:
   Expected response to have a content equivalent to a model, but it has differences:

    - expected property response.Content to be the same string, but they differ at index 0:
   ↓ (actual)
  "Hey, you..."
  "Expected, but not really there..."
   ↑ (expected).
.

The HTTP response was:

HTTP/1.1 201 Created
Location: http://localhost/api/Comments/1
X-Correlation-ID: 4f1b4ccb-d4f2-4105-954c-f0a999ce8f27
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
  Stack Trace:
     ...
```

</details>

- Asserting that the response is 200 OK and the content is like an array of specific objects:

```csharp
[Fact]
public async Task Get_Returns_Ok_With_CommentsList()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments");

    // Assert
    response.Should().Be200Ok().And.BeAs(new[]
    {
        new { Author = "Adrian", Content = "Hey" },
        new { Author = "Johnny", Content = "Hey!" }
    });
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the response contained one comment more than expected)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Returns_Ok_With_CommentsList [403 ms]
  Error Message:
   Expected response to have a content equivalent to a model, but it has differences:

    - expected response to be a collection with 1 item(s), but {{ Author = Adrian, Content = Hey }, { Author = Johnny, Content = Hey! }}"
"contains 1 item(s) more than"
"{{ Author = Adrian, Content = Hey }}.
.

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 7150524b-1b2f-4fc5-9574-8b797d776c95
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1
  Stack Trace:
     ...
```

</details>

- Asserting that the response is an HTTP 400 BadRequest and contains a single error message

```csharp
[Fact]
public async Task Post_WithNoAuthorButWithContent_ReturnsBadRequestWithAnErrorMessageRelatedToAuthorOnly()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.PostAsync("/api/comments", new StringContent(
    """
    {
      "content": "Hey, you..."
    }
    """, Encoding.UTF8, "application/json"));

    // Assert
    response.Should().Be400BadRequest()
        .And.OnlyHaveError("Author", "The Author field is required.");
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the response reported an error for the <code>Content</code> field as well, so there is more than one)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_WithNoAuthorButWithContent_ReturnsBadRequestWithAnErrorMessageRelatedToAuthorOnly [31 ms]
  Error Message:
   Expected response to only contain an error message related to the "Author" field, but more than this one was found.

The HTTP response was:

HTTP/1.1 400 BadRequest
X-Correlation-ID: 5b4b76b1-a1fe-4088-bbf1-5c14f82f4dc3
Content-Type: application/problem+json; charset=utf-8

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Author": [
      "The Author field is required."
    ],
    "Content": [
      "The Content field is required."
    ]
  },
  "traceId": "00-fd941763cb7e1f0fd046d1551ddddc92-6d3df58bb1d79a64-00"
}

The originating HTTP request was:

POST http://localhost/api/comments HTTP 1.1
Content-Type: application/json; charset=utf-8
Content-Length: 35
{
  "author": "",
  "content": ""
}
  Stack Trace:
     ...
```

</details>

- Asserting the response content once deserialized into a strongly typed object it satisfies a certain assertion

```csharp
[Fact]
public async Task Get_Returns_Ok_With_CommentsList_With_TwoUniqueComments()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments");

    // Assert
    response.Should().Satisfy<IEnumerable<Comment>>(model =>
            model.Should().HaveCount(2).And.OnlyHaveUniqueItems(c => c.CommentId));
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the collection had a different count)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Returns_Ok_With_CommentsList_With_TwoUniqueComments [12 ms]
  Error Message:
   Expected response to satisfy one or more model assertions, but it wasn't:

    - expected model to contain 3 item(s), but found 2: {
    Sample.Api.Controllers.Comment
    {
        Author = "Adrian",
        CommentId = 1,
        Content = "Hey"
    },
    Sample.Api.Controllers.Comment
    {
        Author = "Johnny",
        CommentId = 2,
        Content = "Hey!"
    }
}
.


The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 29da9489-0e1e-48b7-8f80-3e3a97bbc749
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1
  Stack Trace:
     ...
```

</details>

- Asserting the response content once deserialized into a anonymous object it satisfies a certain assertion

```csharp
[Fact]
public async Task Get_WithCommentId_Returns_A_NonSpam_Comment()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments/1");

    // Assert
    response.Should().Satisfy(givenModelStructure: new
    {
        Author = default(string),
        Content = default(string)
    }, assertion: model =>
        {
            model.Author.Should().NotBe("I DO SPAM!");
            model.Content.Should().NotContain("BUY MORE");
        });
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: <code>Author</code> was expected to be <code>"I DO SPAM!"</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_WithCommentId_Returns_A_NonSpam_Comment [20 ms]
  Error Message:
   Expected response to satisfy one or more model assertions, but it wasn't:

    - expected model.Author to be the same string, but they differ at index 0:
   ↓ (actual)
  "Adrian"
  "I DO SPAM!"
   ↑ (expected).


The HTTP response was:

HTTP/1.1 200 OK
x-vendor: vendor
X-Correlation-ID: 6a779339-4740-4166-95ee-04a6563fed8e
Content-Type: application/json; charset=utf-8

{
  "author": "Adrian",
  "content": "Hey",
  "commentId": 1
}

The originating HTTP request was:

GET http://localhost/api/comments/1 HTTP 1.1
  Stack Trace:
     ...
```

</details>

- Asserting the response has a header with the name `X-Correlation-ID` and the value matches a certain pattern

```csharp
[Fact]
public async Task Get_Should_Contain_a_Header_With_Correlation_Id()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/values");

    // Assert
    response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*", "we want to test the correlation id is a Guid like one");
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the pattern used was <code>*-not-a-guid*</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Should_Contain_a_Header_With_Correlation_Id [13 ms]
  Error Message:
   Expected response to contain the HTTP header "X-Correlation-ID" having a value matching "*-not-a-guid*", but there was no match because we want to test the correlation id is a Guid like one.

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 35f25ad0-dfac-4e78-9f0a-a13072dfce93
Content-Type: application/json; charset=utf-8

[
  "value1",
  "value2"
]

The originating HTTP request was:

GET http://localhost/api/values HTTP 1.1
  Stack Trace:
     ...
```

</details>

- Asserting the response has a header with the name `x-vendor` and the value is not empty

```csharp
[Fact]
public async Task Get_Should_Contain_a_NonEmpty_Header_With_Vendor()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments/1");

    // Assert
    response.Should().HaveHeader("x-vendor").And.NotBeEmpty();
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the endpoint called did not return the header at all — <code>GET /api/comments</code> instead of <code>GET /api/comments/1</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Should_Contain_a_NonEmpty_Header_With_Vendor [3 ms]
  Error Message:
   Expected response to contain the HTTP header "x-vendor", but no such header was found in the actual response.

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: e78196c0-438e-402a-b90a-baddfb3c6e01
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1
  Stack Trace:
     ...
```

</details>

Many more examples can be found in the [Samples](https://github.com/adrianiftode/Assertions.Web/tree/master/samples) projects and in the Specs files from the [FluentAssertions.Web.Tests](https://github.com/adrianiftode/Assertions.Web/tree/master/test/FluentAssertions.Web.Tests) project

### Shouldly.Web Examples

The same scenarios as above, written against the Shouldly flavour. The Arrange and Act parts are identical; only the assertions differ. There is no `Should()`/`And` chain: every assertion is its own call on the response, and, where a custom message is used, it is passed as the first optional argument. The named assertions are listed in [Shouldly.Web API](#shouldlyweb-api).

- Asserting that the response content of an HTTP POST request is equivalent to a certain object

```csharp
[Fact]
public async Task Post_ReturnsOkAndWithContent()
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
    response.ShouldBeAs(new
    {
        Author = "John",
        Content = "Hey, you..."
    });
}
```


<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the API persisted a different <code>content</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_ReturnsOkAndWithContent [72 ms]
  Error Message:
   Shouldly.ShouldAssertException : Comparing object equivalence, at path:
response.Content [<anonymous type>]
    Content [System.String]

    Expected value to be
"Expected, but not really there..."
    but was
"Hey, you..."

The HTTP response was:

HTTP/1.1 201 Created
Location: http://localhost/api/Comments/1
X-Correlation-ID: 3259aec0-1585-4b15-94ff-8c882729c3ce
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
  Stack Trace:
     ...
```

</details>

- Asserting that the response is 200 OK and the content is like an array of specific objects
```csharp
[Fact]
public async Task Get_Returns_Ok_With_CommentsList()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments");

    // Assert
    response.ShouldBe200Ok();
    response.ShouldBeAs(new[]
    {
        new { Author = "Adrian", Content = "Hey" },
        new { Author = "Johnny", Content = "Hey!" }
    });
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the response contained one comment more than expected)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Returns_Ok_With_CommentsList [15 ms]
  Error Message:
   Shouldly.ShouldAssertException : Comparing object equivalence, at path:
response.Content [<anonymous type>[]]
    Count

    Expected value to be
1
    but was
2

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 78751d19-8bde-4d99-80fb-0388497151f0
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1
  Stack Trace:
     ...
```

</details>

- Asserting that the response is an HTTP 400 BadRequest and contains a single error message

```csharp
[Fact]
public async Task Post_WithNoAuthorButWithContent_ReturnsBadRequestWithAnErrorMessageRelatedToAuthorOnly()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.PostAsync("/api/comments", new StringContent(
    """
    {
      "content": "Hey, you..."
    }
    """, Encoding.UTF8, "application/json"));

    // Assert
    response.ShouldBe400BadRequest();
    response.ShouldOnlyHaveError("Author", "The Author field is required.");
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the response reported an error for the <code>Content</code> field as well, so there is more than one)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_WithNoAuthorButWithContent_ReturnsBadRequestWithAnErrorMessageRelatedToAuthorOnly [47 ms]
  Error Message:
   Shouldly.ShouldAssertException : response.Errors
    should only contain this error field
"Author"
    but was
["Author", "Content"]

The HTTP response was:

HTTP/1.1 400 BadRequest
X-Correlation-ID: 21af9dfd-1216-48c8-b289-7ad77883521b
Content-Type: application/problem+json; charset=utf-8

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Author": [
      "The Author field is required."
    ],
    "Content": [
      "The Content field is required."
    ]
  },
  "traceId": "00-66708776909c03ac083400fc5ff5fce1-dc2b2c50b9a6f9d2-00"
}

The originating HTTP request was:

POST http://localhost/api/comments HTTP 1.1
Content-Type: application/json; charset=utf-8
Content-Length: 95
{
  "author": "",
  "content": ""
}
  Stack Trace:
     ...
```

</details>

- Asserting the response content once deserialized into a strongly typed object it satisfies a certain assertion

```csharp
[Fact]
public async Task Get_Returns_Ok_With_CommentsList_With_TwoUniqueComments()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments");

    // Assert
    response.ShouldSatisfy<IEnumerable<Comment>>([
        model => model.Count().ShouldBe(2),
        model => model.Select(c => c.CommentId).ShouldBeUnique()]);
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: both conditions fail, so both are reported)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Returns_Ok_With_CommentsList_With_TwoUniqueComments [440 ms]
  Error Message:
   Shouldly.ShouldAssertException : response.Content
    should satisfy all the conditions specified, but does not.
The following errors were found ...
---------------- Error 1 ----------------
    model.Count()
        should be
    3
        but was
    2

---------------- Error 2 ----------------
    model.Select(c => c.CommentId)
        should contain
    3
        but was actually
    [1, 2]

-----------------------------------------

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: fcebb384-604e-4b95-9404-ecb69e87932a
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1

  Stack Trace:
     ...
```

</details>

> **Note:** Each entry of the collection is a separate condition, so **every** failing assertion is reported as its own `Error 1`, `Error 2`, … block — the same as FluentAssertions does for a multi-statement lambda body. A single assertion lambda, by contrast, runs as one condition and stops at its first failure: when migrating from FluentAssertions, pass the statements of that lambda as the condition list instead.

- Asserting the response content once deserialized into a anonymous object it satisfies a certain assertion

```csharp
[Fact]
public async Task Get_WithCommentId_Returns_A_NonSpam_Comment()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments/1");

    // Assert
    response.ShouldSatisfy(new
    {
        Author = default(string),
        Content = default(string)
    }, [
        model => model.Author.ShouldNotBe("I DO SPAM!"),
        model => model.Content.ShouldNotContain("BUY MORE")]);
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: <code>Author</code> was expected to be <code>"I DO SPAM!"</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_WithCommentId_Returns_A_NonSpam_Comment [50 ms]
  Error Message:
   Shouldly.ShouldAssertException : response.Content
    should satisfy all the conditions specified, but does not.
The following errors were found ...
---------------- Error 1 ----------------
    model.Author
        should be
    "I DO SPAM!"
        but was
    "Adrian"
        difference
    Expected: "I DO SPAM!"
    Actual:   "Adrian"

-----------------------------------------

The HTTP response was:

HTTP/1.1 200 OK
x-vendor: vendor
X-Correlation-ID: c496e17e-9fb3-4025-8ee0-83a86e978711
Content-Type: application/json; charset=utf-8

{
  "author": "Adrian",
  "content": "Hey",
  "commentId": 1
}

The originating HTTP request was:

GET http://localhost/api/comments/1 HTTP 1.1

  Stack Trace:
     ...
```

</details>

- Asserting the response has a header with the name `X-Correlation-ID` and the value matches a certain pattern

```csharp
[Fact]
public async Task Get_Should_Contain_a_Header_With_Correlation_Id()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/values");

    // Assert
    response.ShouldHaveHeaderMatching("X-Correlation-ID", "*-*",
        "we want to test the correlation id is a Guid like one");
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the pattern used was <code>*-not-a-guid*</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Should_Contain_a_Header_With_Correlation_Id [13 ms]
  Error Message:
   Shouldly.ShouldAssertException : response.Headers["X-Correlation-ID"]
    should have header matching
"*-not-a-guid*"
    but was
["0a588fed-ba7e-48ba-b8a3-e0b1946967c1"]

Additional Info:
    we want to test the correlation id is a Guid like one

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 0a588fed-ba7e-48ba-b8a3-e0b1946967c1
Content-Type: application/json; charset=utf-8

[
  "value1",
  "value2"
]

The originating HTTP request was:

GET http://localhost/api/values HTTP 1.1

  Stack Trace:
     ...
```

</details>

- Asserting the response has a header with the name `x-vendor` and the value is not empty

```csharp
[Fact]
public async Task Get_Should_Contain_a_NonEmpty_Header_With_Vendor()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/api/comments/1");

    // Assert
    response.ShouldHaveNonEmptyHeader("x-vendor");
}
```

<details>
<summary><code>dotnet test</code> output when this assertion fails <em>(here: the endpoint called did not return the header at all — <code>GET /api/comments</code> instead of <code>GET /api/comments/1</code>)</em></summary>

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Get_Should_Contain_a_NonEmpty_Header_With_Vendor [440 ms]
  Error Message:
   Shouldly.ShouldAssertException : response.Headers
    should have non empty header
"x-vendor"
    but was
["X-Correlation-ID", "Content-Type"]

The HTTP response was:

HTTP/1.1 200 OK
X-Correlation-ID: 3a130b8a-6a2b-4316-8ed5-132d1d04ebfe
Content-Type: application/json; charset=utf-8

[
  {
    "author": "Adrian",
    "content": "Hey",
    "commentId": 1
  },
  {
    "author": "Johnny",
    "content": "Hey!",
    "commentId": 2
  }
]

The originating HTTP request was:

GET http://localhost/api/comments HTTP 1.1

  Stack Trace:
     ...
```

</details>

When an assertion fails, the test output follows the [Shouldly message layout](#shouldlyweb) — `should be`/`but was` for the expected and actual values, an optional custom message under `Additional Info` — followed by the same HTTP response and originating request dump shown above.

Many more examples can be found in the [Shouldly.Web.Tests](https://github.com/adrianiftode/Assertions.Web/tree/master/test/Shouldly.Web.Tests) and [Sample.Api.Shouldly.Tests](https://github.com/adrianiftode/Assertions.Web/tree/master/test/Sample.Api.Shouldly.Tests) projects, which run the same shared specs as the FluentAssertions and AwesomeAssertions flavours.

### Full API

The tables below list every assertion of the FluentAssertions and AwesomeAssertions flavours. Each group is followed by a short example and, where it helps, by the `dotnet test` output you get when the assertion does not hold, so it is clear what is actually being reported. The Shouldly flavour exposes the same assertions under the flat naming described in [Shouldly.Web API](#shouldlyweb-api), and the [Shouldly.Web](#shouldlyweb) section shows how the names map.

|  *HttpResponseMessageAssertions* | Contains a number of methods to assert that an HttpResponseMessage is in the expected state related to the HTTP content. |
| --- | --- |
| **Should().BeEmpty()** | Asserts that HTTP response content is empty. |
| **Should().BeAs&lt;TModel&gt;()** | Asserts that HTTP response content can be an equivalent representation of the expected model. |
| **Should().HaveHeader()** | Asserts that an HTTP response has a named header. |
| **Should().NotHaveHeader()** | Asserts that an HTTP response does not have a named header. |
| **Should().HaveHttpStatusCode()** | Asserts that an HTTP response has an HTTP status with the specified code. |
| **Should().NotHaveHttpStatusCode()** | Asserts that an HTTP response does not have an HTTP status with the specified code. |
| **Should().MatchInContent()** | Asserts that HTTP response has content that matches a wildcard pattern. |
| **Should().Satisfy&lt;TModel&gt;()** | Asserts that the HTTP response content, once deserialized to `TModel`, satisfies an assertion. |
| **Should().Satisfy()** | Asserts that the `HttpResponseMessage` itself satisfies an assertion. |

```csharp
response.Should().BeAs(new { Author = "John", Content = "Hey, you..." });
response.Should().HaveHttpStatusCode(HttpStatusCode.Accepted);
response.Should().MatchInContent("*\"commentId\": 1*");
response.Should().Satisfy<IEnumerable<Comment>>(comments => comments.Should().HaveCount(2));
response.Should().Satisfy(r => r.Headers.Contains("X-Correlation-ID").Should().BeTrue());
```

<details>
<summary><code>dotnet test</code> output for <code>HaveHttpStatusCode(HttpStatusCode.Accepted)</code> when it fails</summary>

```text
  Error Message:
   Expected response to be HttpStatusCode.Accepted {value: 202}, but found HttpStatusCode.Created {value: 201}.

The HTTP response was:

HTTP/1.1 201 Created
Location: http://localhost/api/Comments/1
X-Correlation-ID: 6ec3cb4b-36cf-4003-b95b-c2d577a9f739
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

</details>

|  *Should().HaveHeader().And.* | Contains a number of methods to assert that an HttpResponseMessage is in the expected state related to HTTP headers. |
| --- | --- |
| **BeEmpty()** | Asserts that an existing HTTP header in an HTTP response has no values. |
| **NotBeEmpty()** | Asserts that an existing HTTP header in an HTTP response has any values. |
| **BeValue()** | Asserts that an existing HTTP header in an HTTP response has exactly one value, equal to the expected value. |
| **BeValues()** | Asserts that an existing HTTP header in an HTTP response has the complete expected collection of values, in any order. |
| **Match()** | Asserts that an existing HTTP header in an HTTP response contains at least a value that matches a wildcard pattern. |

```csharp
response.Should().HaveHeader("X-Correlation-ID").And.NotBeEmpty();
response.Should().HaveHeader("X-Correlation-ID").And.BeValue("5f1615eb-549e-4afa-a015-8c95fd8715c9");
response.Should().HaveHeader("Set-Cookie").And.BeValues(new[] { "a=1", "b=2" });
response.Should().HaveHeader("X-Correlation-ID").And.Match("*-*", "it should look like a Guid");
```

Header values are used exactly as `HttpHeaders` exposes them; no assertion splits comma-separated text. For example, .NET exposes `Allow: GET, POST` as two values when the header is added through the content-header API:

```csharp
response.Content.Headers.TryAddWithoutValidation("Allow", new[] { "GET", "POST" });
response.Should().HaveHeader("Allow").And.BeValues(new[] { "GET", "POST" });
```

If the header is added as one raw string (`"GET, POST"`), it is one value and must be asserted with `BeValue("GET, POST")` or `BeValues(new[] { "GET, POST" })`. See [Single-value and multi-value headers](docs/Assertions.Web/Readme.md#single-value-and-multi-value-headers) for the full comparison table, including the Shouldly equivalents.

`BeEmpty()` in this table asserts that the header selected by `HaveHeader` has no values; it does not mean that the response has no headers. To assert both an empty header and an empty body in one test, keep the header assertion in its group and start a fresh chain for the content:

```csharp
response.Should().HaveHeader("X-Optional").And.BeEmpty(); // header values
response.Should().BeEmpty();                              // response content
```

With Shouldly the names are already distinct: `ShouldHaveEmptyHeader("X-Optional")` targets the header, while `ShouldBeEmpty()` targets the body. See [The two `BeEmpty()` targets](docs/Assertions.Web/Readme.md#the-two-beempty-targets) for the full table and the API compatibility decision.

<details>
<summary><code>dotnet test</code> output for <code>And.BeValue("other-vendor")</code> when it fails</summary>

```text
  Error Message:
   Expected response to contain the "x-vendor" HTTP header and the expected header value/header to be equivalent to the same string, but they differ at index 0:
   ↓ (actual)
  "vendor"
  "other-vendor"
   ↑ (expected).

The HTTP response was:

HTTP/1.1 200 OK
x-vendor: vendor
X-Correlation-ID: e3903b0e-36b3-495d-a8f3-dfda4cee1566
Content-Type: application/json; charset=utf-8

{
  "author": "Adrian",
  "content": "Hey",
  "commentId": 1
}

The originating HTTP request was:

GET http://localhost/api/comments/1 HTTP 1.1
```

</details>

|  *Should().Be400BadRequest().And.* | Contains a number of methods to assert that an HttpResponseMessage is in the expected state related to HTTP Bad Request response |
| --- | --- |
| **HaveError()** | Asserts that a Bad Request HTTP response content contains an error message identifiable by an expected field name and a wildcard error text. |
| **OnlyHaveError()** | Asserts that a Bad Request HTTP response content contains only a single error message identifiable by an expected field name and a wildcard error text. |
| **NotHaveError()** | Asserts that a Bad Request HTTP response content does not contain an error message identifiable by an expected field name and a wildcard error text. |
| **HaveErrorMessage()** | Asserts that a Bad Request HTTP response content contains an error message identifiable by an wildcard error text. |

```csharp
response.Should().Be400BadRequest()
    .And.HaveError("Author", "*required*")
    .And.NotHaveError("Content")
    .And.HaveErrorMessage("*one or more validation errors*");
```

<details>
<summary><code>dotnet test</code> output for <code>HaveError()</code> when it fails</summary>

```text
  Error Message:
   Expected response to contain an error message related to the "Content" field, but was not found.

The HTTP response was:

HTTP/1.1 400 BadRequest
X-Correlation-ID: c61ba4fa-ea03-484b-8345-ad6243e1e9ab
Content-Type: application/problem+json; charset=utf-8

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Author": [
      "The Author field is required."
    ]
  },
  "traceId": "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
}

The originating HTTP request was:

POST http://localhost/api/comments HTTP 1.1
Content-Type: application/json; charset=utf-8
Content-Length: 30
{
  "content": "Hey, you..."
}
```

</details>

|  *HaveLocation* Header related assertions. | |
| --- | --- |
| Should().Be201Created().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 201 status code has a location header. |
| Should().Be202Accepted().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 202 status code has a location header. |
| Should().Be204NoContent().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 204 status code has a location header. |
| Should().Be300Ambiguous().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 300 status code has a location header. |
| Should().Be300MultipleChoices().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 300 status code has a location header. |
| Should().Be301Moved().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 301 status code has a location header. |
| Should().Be301MovedPermanently().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 301 status code has a location header. |
| Should().Be302Found().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 302 status code has a location header. |
| Should().Be302Redirect().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 302 status code has a location header. |
| Should().Be303RedirectMethod().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 303 status code has a location header. |
| Should().Be303SeeOther().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 303 status code has a location header. |
| Should().Be305UseProxy().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 305 status code has a location header. |
| Should().Be307RedirectKeepVerb().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 307 status code has a location header. |
| Should().Be307TemporaryRedirect().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 307 status code has a location header. |
| Should().Be308PermanentRedirect().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 308 status code has a location header. |
| Should().Be3XXRedirection().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP redirection response has a location header. |
| Should().Be409Conflict().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 409 status code has a location header. |
| Should().Be410Gone().And.**HaveLocation()**.And.BeValue() |  Asserts that an HTTP response with 410 status code has a location header. |

```csharp
response.Should().Be201Created()
    .And.HaveLocation()
    .And.Match("*/api/Comments/1");
```

These are the only status assertions that return `LocationAssertions`. `Be304NotModified()` returns the general group, so use `HaveHeader("Location")` to inspect a `Location` header on a 304 response. After `HaveLocation()` the chain is on `HeadersAssertions`; start a new `response.Should()` chain with a location-bearing status if another `HaveLocation()` call is needed.

<details>
<summary><code>dotnet test</code> output for <code>Be201Created()</code> when the response is a 400 instead</summary>

```text
  Error Message:
   Expected response to be HttpStatusCode.Created {value: 201}, but found HttpStatusCode.BadRequest {value: 400}.

The HTTP response was:

HTTP/1.1 400 BadRequest
X-Correlation-ID: 55b73f11-0235-4110-85d3-1ad3477fdd3e
Content-Type: application/problem+json; charset=utf-8

{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Author": [
      "The Author field is required."
    ],
    "Content": [
      "The Content field is required."
    ]
  },
  "traceId": "00-88afb99689f9c20df02c7b18e409e45f-c1994f2de745b13d-00"
}

The originating HTTP request was:

POST http://localhost/api/comments HTTP 1.1
Content-Type: application/json; charset=utf-8
Content-Length: 19
{
  "content": ""
}
```

</details>

|  *Fine grained status assertions.* | |
| --- | --- |
| **Should().Be1XXInformational()** |  Asserts that an HTTP response has an HTTP status code representing an informational response. |
| **Should().Be2XXSuccessful()** | Asserts that an HTTP response has a successful HTTP status code. |
| **Should().Be3XXRedirection()** | Asserts that an HTTP response has an HTTP status code representing a redirection response. |
| **Should().Be4XXClientError()** | Asserts that an HTTP response has an HTTP status code representing a client error. |
| **Should().Be5XXServerError()** | Asserts that an HTTP response has an HTTP status code representing a server error. |
| **Should().Be100Continue()** | Asserts that an HTTP response has the HTTP status 100 Continue |
| **Should().Be101SwitchingProtocols()** | Asserts that an HTTP response has the HTTP status 101 Switching Protocols |
| **Should().Be200Ok()** | Asserts that an HTTP response has the HTTP status 200 Ok |
| **Should().Be201Created()** | Asserts that an HTTP response has the HTTP status 201 Created |
| **Should().Be202Accepted()** | Asserts that an HTTP response has the HTTP status 202 Accepted |
| **Should().Be203NonAuthoritativeInformation()** | Asserts that an HTTP response has the HTTP status 203 Non Authoritative Information |
| **Should().Be204NoContent()** | Asserts that an HTTP response has the HTTP status 204 No Content |
| **Should().Be205ResetContent()** | Asserts that an HTTP response has the HTTP status 205 Reset Content |
| **Should().Be206PartialContent()** | Asserts that an HTTP response has the HTTP status 206 Partial Content |
| **Should().Be300Ambiguous()** | Asserts that an HTTP response has the HTTP status 300 Ambiguous |
| **Should().Be300MultipleChoices()** | Asserts that an HTTP response has the HTTP status 300 Multiple Choices |
| **Should().Be301Moved()** | Asserts that an HTTP response has the HTTP status 301 Moved |
| **Should().Be301MovedPermanently()** | Asserts that an HTTP response has the HTTP status 301 Moved Permanently |
| **Should().Be302Found()** | Asserts that an HTTP response has the HTTP status 302 Found |
| **Should().Be302Redirect()** | Asserts that an HTTP response has the HTTP status 302 Redirect |
| **Should().Be303RedirectMethod()** | Asserts that an HTTP response has the HTTP status 303 Redirect Method |
| **Should().Be303SeeOther()** | Asserts that an HTTP response has the HTTP status 303 See Other |
| **Should().Be304NotModified()** | Asserts that an HTTP response has the HTTP status 304 Not Modified |
| **Should().Be305UseProxy()** | Asserts that an HTTP response has the HTTP status 305 Use Proxy |
| **Should().Be306Unused()** | Asserts that an HTTP response has the HTTP status 306 Unused |
| **Should().Be307RedirectKeepVerb()** | Asserts that an HTTP response has the HTTP status 307 Redirect Keep Verb |
| **Should().Be307TemporaryRedirect()** | Asserts that an HTTP response has the HTTP status 307 Temporary Redirect |
| **Should().Be308PermanentRedirect()** | Asserts that an HTTP response has the HTTP status 308 Permanent Redirect |
| **Should().Be400BadRequest()** | Asserts that an HTTP response has the HTTP status 400 BadRequest |
| **Should().Be401Unauthorized()** | Asserts that an HTTP response has the HTTP status 401 Unauthorized |
| **Should().Be402PaymentRequired()** | Asserts that an HTTP response has the HTTP status 402 Payment Required |
| **Should().Be403Forbidden()** | Asserts that an HTTP response has the HTTP status 403 Forbidden |
| **Should().Be404NotFound()** | Asserts that an HTTP response has the HTTP status 404 Not Found |
| **Should().Be405MethodNotAllowed()** | Asserts that an HTTP response has the HTTP status 405 Method Not Allowed |
| **Should().Be406NotAcceptable()** | Asserts that an HTTP response has the HTTP status 406 Not Acceptable |
| **Should().Be407ProxyAuthenticationRequired()** | Asserts that an HTTP response has the HTTP status 407 Proxy Authentication Required |
| **Should().Be408RequestTimeout()** | Asserts that an HTTP response has the HTTP status 408 Request Timeout |
| **Should().Be409Conflict()** | Asserts that an HTTP response has the HTTP status 409 Conflict |
| **Should().Be410Gone()** | Asserts that an HTTP response has the HTTP status 410 Gone |
| **Should().Be411LengthRequired()** | Asserts that an HTTP response has the HTTP status 411 Length Required |
| **Should().Be412PreconditionFailed()** | Asserts that an HTTP response has the HTTP status 412 Precondition Failed |
| **Should().Be413RequestEntityTooLarge()** | Asserts that an HTTP response has the HTTP status 413 Request Entity Too Large |
| **Should().Be414RequestUriTooLong()** | Asserts that an HTTP response has the HTTP status 414 Request Uri Too Long |
| **Should().Be415UnsupportedMediaType()** | Asserts that an HTTP response has the HTTP status 415 Unsupported Media Type |
| **Should().Be416RequestedRangeNotSatisfiable()** | Asserts that an HTTP response has the HTTP status 416 Requested Range Not Satisfiable |
| **Should().Be417ExpectationFailed()** | Asserts that an HTTP response has the HTTP status 417 Expectation Failed |
| **Should().Be418ImATeapot()** | Asserts that an HTTP response has the HTTP status 418 I'm A Teapot |
| **Should().Be422UnprocessableEntity()** | Asserts that an HTTP response has the HTTP status 422 Unprocessable Entity |
| **Should().Be426UpgradeRequired()** | Asserts that an HTTP response has the HTTP status 426 UpgradeRequired |
| **Should().Be429TooManyRequests()** | Asserts that an HTTP response has the HTTP status 429 Too Many Requests |
| **Should().Be500InternalServerError()** | Asserts that an HTTP response has the HTTP status 500 Internal Server Error |
| **Should().Be501NotImplemented()** | Asserts that an HTTP response has the HTTP status 501 Not Implemented |
| **Should().Be502BadGateway()** | Asserts that an HTTP response has the HTTP status 502 Bad Gateway |
| **Should().Be503ServiceUnavailable()** | Asserts that an HTTP response has the HTTP status 503 Service Unavailable |
| **Should().Be504GatewayTimeout()** | Asserts that an HTTP response has the HTTP status 504 Gateway Timeout |
| **Should().Be505HttpVersionNotSupported()** | Asserts that an HTTP response has the HTTP status 505 Http Version Not Supported |
| **Should().Be102Processing()** | Asserts that an HTTP response has the HTTP status 102 Processing |
| **Should().Be103EarlyHints()** | Asserts that an HTTP response has the HTTP status 103 Early Hints |
| **Should().Be207MultiStatus()** | Asserts that an HTTP response has the HTTP status 207 Multi-Status |
| **Should().Be208AlreadyReported()** | Asserts that an HTTP response has the HTTP status 208 Already Reported |
| **Should().Be226IMUsed()** | Asserts that an HTTP response has the HTTP status 226 IM Used |
| **Should().Be421MisdirectedRequest()** | Asserts that an HTTP response has the HTTP status 421 Misdirected Request |
| **Should().Be423Locked()** | Asserts that an HTTP response has the HTTP status 423 Locked |
| **Should().Be424FailedDependency()** | Asserts that an HTTP response has the HTTP status 424 Failed Dependency |
| **Should().Be425TooEarly()** | Asserts that an HTTP response has the HTTP status 425 Too Early |
| **Should().Be428PreconditionRequired()** | Asserts that an HTTP response has the HTTP status 428 Precondition Required |
| **Should().Be431RequestHeaderFieldsTooLarge()** | Asserts that an HTTP response has the HTTP status 431 Request Header Fields Too Large |
| **Should().Be451UnavailableForLegalReasons()** | Asserts that an HTTP response has the HTTP status 451 Unavailable For Legal Reasons |
| **Should().Be506VariantAlsoNegotiates()** | Asserts that an HTTP response has the HTTP status 506 Variant Also Negotiates |
| **Should().Be507InsufficientStorage()** | Asserts that an HTTP response has the HTTP status 507 Insufficient Storage |
| **Should().Be508LoopDetected()** | Asserts that an HTTP response has the HTTP status 508 Loop Detected |
| **Should().Be510NotExtended()** | Asserts that an HTTP response has the HTTP status 510 Not Extended |
| **Should().Be511NetworkAuthenticationRequired()** | Asserts that an HTTP response has the HTTP status 511 Network Authentication Required |

```csharp
response.Should().Be2XXSuccessful();
response.Should().Be404NotFound();
response.Should().Be5XXServerError();
```

A failure here looks exactly like the status code outputs shown above: the expected/actual status codes, then the full response and the originating request.

#### Shouldly.Web API

The same groups as above, with the flat Shouldly naming: the `Should()`/`And` chain is replaced by standalone calls on the response, and every optional custom message is the first optional argument. The failure output follows the [Shouldly layout](#shouldlyweb): `should be`/`but was`, an optional custom message under `Additional Info`, then the same HTTP conversation dump.

|  *Response and content assertions.* | |
| --- | --- |
| **ShouldBeEmpty()** | Asserts that HTTP response content is empty. |
| **ShouldBeAs&lt;TModel&gt;()** | Asserts that HTTP response content can be an equivalent representation of the expected model. |
| **ShouldHaveHeader()** | Asserts that an HTTP response has a named header. |
| **ShouldNotHaveHeader()** | Asserts that an HTTP response does not have a named header. |
| **ShouldHaveHttpStatusCode()** | Asserts that an HTTP response has an HTTP status with the specified code. |
| **ShouldNotHaveHttpStatusCode()** | Asserts that an HTTP response does not have an HTTP status with the specified code. |
| **ShouldMatchInContent()** | Asserts that HTTP response has content that matches a wildcard pattern. |
| **ShouldSatisfy&lt;TModel&gt;()** | Asserts that the HTTP response content, once deserialized to `TModel`, satisfies one or more assertions; passing them as a collection reports every failing assertion. |
| **ShouldSatisfy()** | Asserts that the `HttpResponseMessage` itself satisfies one or more assertions; passing them as a collection reports every failing assertion. |

```csharp
response.ShouldBeAs(new { Author = "John", Content = "Hey, you..." });
response.ShouldHaveHttpStatusCode(HttpStatusCode.Accepted);
response.ShouldMatchInContent("*\"commentId\": 1*");
response.ShouldSatisfy<IEnumerable<Comment>>(comments => comments.Count().ShouldBe(2));

// Several conditions, each its own entry: every failing assertion is reported, not only the first.
response.ShouldSatisfy([
    r => r.ShouldHaveHeader("X-Correlation-ID"),
    r => r.Headers.AcceptRanges.ShouldContain("byte")]);
```

|  *Header value assertions.* | |
| --- | --- |
| **ShouldHaveHeaderWithValue()** | Asserts that an existing HTTP header in an HTTP response has exactly one value, equal to the expected value. |
| **ShouldHaveHeaderWithValues()** | Asserts that an existing HTTP header in an HTTP response has the complete expected collection of values, in any order. |
| **ShouldHaveHeaderMatching()** | Asserts that an existing HTTP header in an HTTP response contains at least a value that matches a wildcard pattern. |
| **ShouldHaveEmptyHeader()** | Asserts that an existing HTTP header in an HTTP response has no values. |
| **ShouldHaveNonEmptyHeader()** | Asserts that an existing HTTP header in an HTTP response has any values. |

```csharp
response.ShouldHaveHeader("X-Correlation-ID");
response.ShouldNotHaveHeader("x-cache");
response.ShouldHaveHeaderWithValue("X-Correlation-ID", "5f1615eb-549e-4afa-a015-8c95fd8715c9");
response.ShouldHaveHeaderWithValues("Set-Cookie", new[] { "a=1", "b=2" });
response.ShouldHaveHeaderMatching("X-Correlation-ID", "*-*", "it should look like a Guid");
response.ShouldHaveEmptyHeader("x-optional");
response.ShouldHaveNonEmptyHeader("x-vendor");
```

The Shouldly equivalents follow the same value model as the fluent assertions, including `Allow`:

```csharp
response.Content.Headers.TryAddWithoutValidation("Allow", new[] { "GET", "POST" });
response.ShouldHaveHeaderWithValues("Allow", new[] { "GET", "POST" });
```

|  *Bad Request error assertions.* | |
| --- | --- |
| **ShouldHaveError()** | Asserts that a Bad Request HTTP response content contains an error message identifiable by an expected field name and a wildcard error text. |
| **ShouldOnlyHaveError()** | Asserts that a Bad Request HTTP response content contains only a single error message identifiable by an expected field name and a wildcard error text. |
| **ShouldNotHaveError()** | Asserts that a Bad Request HTTP response content does not contain an error message identifiable by an expected field name and a wildcard error text. |
| **ShouldHaveErrorMessage()** | Asserts that a Bad Request HTTP response content contains an error message identifiable by an wildcard error text. |

```csharp
response.ShouldBe400BadRequest();
response.ShouldHaveError("Author", "*required*");
response.ShouldNotHaveError("Content");
response.ShouldHaveErrorMessage("*one or more validation errors*");
```

|  *Location header assertions.* | |
| --- | --- |
| **ShouldHaveLocation()** | Asserts that an HTTP response has a Location header. |
| **ShouldNotHaveLocation()** | Asserts that an HTTP response does not have a Location header. |
| **ShouldHaveLocationWithValue()** | Asserts that the Location header of an HTTP response has exactly one value equivalent to the expected one. |
| **ShouldHaveLocationWithValues()** | Asserts that the Location header of an HTTP response has an expected list of header values. |
| **ShouldHaveLocationMatching()** | Asserts that the Location header of an HTTP response contains at least a value that matches a wildcard pattern. |

```csharp
response.ShouldBe201Created();
response.ShouldHaveLocation();
response.ShouldHaveLocationWithValue("http://localhost/api/Comments/1");
response.ShouldHaveLocationWithValues(new[] { "http://localhost/api/Comments/1" });
response.ShouldHaveLocationMatching("*/api/Comments/1");
response.ShouldNotHaveLocation();
```

|  *Fine grained status assertions.* | |
| --- | --- |
| **ShouldBe1XXInformational()** |  Asserts that an HTTP response has an HTTP status code representing an informational response. |
| **ShouldBe2XXSuccessful()** | Asserts that an HTTP response has a successful HTTP status code. |
| **ShouldBe3XXRedirection()** | Asserts that an HTTP response has an HTTP status code representing a redirection response. |
| **ShouldBe4XXClientError()** | Asserts that an HTTP response has an HTTP status code representing a client error. |
| **ShouldBe5XXServerError()** | Asserts that an HTTP response has an HTTP status code representing a server error. |
| **ShouldBe100Continue()** | Asserts that an HTTP response has the HTTP status 100 Continue |
| **ShouldBe101SwitchingProtocols()** | Asserts that an HTTP response has the HTTP status 101 Switching Protocols |
| **ShouldBe200Ok()** | Asserts that an HTTP response has the HTTP status 200 Ok |
| **ShouldBe201Created()** | Asserts that an HTTP response has the HTTP status 201 Created |
| **ShouldBe202Accepted()** | Asserts that an HTTP response has the HTTP status 202 Accepted |
| **ShouldBe203NonAuthoritativeInformation()** | Asserts that an HTTP response has the HTTP status 203 Non Authoritative Information |
| **ShouldBe204NoContent()** | Asserts that an HTTP response has the HTTP status 204 No Content |
| **ShouldBe205ResetContent()** | Asserts that an HTTP response has the HTTP status 205 Reset Content |
| **ShouldBe206PartialContent()** | Asserts that an HTTP response has the HTTP status 206 Partial Content |
| **ShouldBe300Ambiguous()** | Asserts that an HTTP response has the HTTP status 300 Ambiguous |
| **ShouldBe300MultipleChoices()** | Asserts that an HTTP response has the HTTP status 300 Multiple Choices |
| **ShouldBe301Moved()** | Asserts that an HTTP response has the HTTP status 301 Moved |
| **ShouldBe301MovedPermanently()** | Asserts that an HTTP response has the HTTP status 301 Moved Permanently |
| **ShouldBe302Found()** | Asserts that an HTTP response has the HTTP status 302 Found |
| **ShouldBe302Redirect()** | Asserts that an HTTP response has the HTTP status 302 Redirect |
| **ShouldBe303RedirectMethod()** | Asserts that an HTTP response has the HTTP status 303 Redirect Method |
| **ShouldBe303SeeOther()** | Asserts that an HTTP response has the HTTP status 303 See Other |
| **ShouldBe304NotModified()** | Asserts that an HTTP response has the HTTP status 304 Not Modified |
| **ShouldBe305UseProxy()** | Asserts that an HTTP response has the HTTP status 305 Use Proxy |
| **ShouldBe306Unused()** | Asserts that an HTTP response has the HTTP status 306 Unused |
| **ShouldBe307RedirectKeepVerb()** | Asserts that an HTTP response has the HTTP status 307 Redirect Keep Verb |
| **ShouldBe307TemporaryRedirect()** | Asserts that an HTTP response has the HTTP status 307 Temporary Redirect |
| **ShouldBe308PermanentRedirect()** | Asserts that an HTTP response has the HTTP status 308 Permanent Redirect |
| **ShouldBe400BadRequest()** | Asserts that an HTTP response has the HTTP status 400 BadRequest |
| **ShouldBe401Unauthorized()** | Asserts that an HTTP response has the HTTP status 401 Unauthorized |
| **ShouldBe402PaymentRequired()** | Asserts that an HTTP response has the HTTP status 402 Payment Required |
| **ShouldBe403Forbidden()** | Asserts that an HTTP response has the HTTP status 403 Forbidden |
| **ShouldBe404NotFound()** | Asserts that an HTTP response has the HTTP status 404 Not Found |
| **ShouldBe405MethodNotAllowed()** | Asserts that an HTTP response has the HTTP status 405 Method Not Allowed |
| **ShouldBe406NotAcceptable()** | Asserts that an HTTP response has the HTTP status 406 Not Acceptable |
| **ShouldBe407ProxyAuthenticationRequired()** | Asserts that an HTTP response has the HTTP status 407 Proxy Authentication Required |
| **ShouldBe408RequestTimeout()** | Asserts that an HTTP response has the HTTP status 408 Request Timeout |
| **ShouldBe409Conflict()** | Asserts that an HTTP response has the HTTP status 409 Conflict |
| **ShouldBe410Gone()** | Asserts that an HTTP response has the HTTP status 410 Gone |
| **ShouldBe411LengthRequired()** | Asserts that an HTTP response has the HTTP status 411 Length Required |
| **ShouldBe412PreconditionFailed()** | Asserts that an HTTP response has the HTTP status 412 Precondition Failed |
| **ShouldBe413RequestEntityTooLarge()** | Asserts that an HTTP response has the HTTP status 413 Request Entity Too Large |
| **ShouldBe414RequestUriTooLong()** | Asserts that an HTTP response has the HTTP status 414 Request Uri Too Long |
| **ShouldBe415UnsupportedMediaType()** | Asserts that an HTTP response has the HTTP status 415 Unsupported Media Type |
| **ShouldBe416RequestedRangeNotSatisfiable()** | Asserts that an HTTP response has the HTTP status 416 Requested Range Not Satisfiable |
| **ShouldBe417ExpectationFailed()** | Asserts that an HTTP response has the HTTP status 417 Expectation Failed |
| **ShouldBe418ImATeapot()** | Asserts that an HTTP response has the HTTP status 418 I'm A Teapot |
| **ShouldBe422UnprocessableEntity()** | Asserts that an HTTP response has the HTTP status 422 Unprocessable Entity |
| **ShouldBe426UpgradeRequired()** | Asserts that an HTTP response has the HTTP status 426 UpgradeRequired |
| **ShouldBe429TooManyRequests()** | Asserts that an HTTP response has the HTTP status 429 Too Many Requests |
| **ShouldBe500InternalServerError()** | Asserts that an HTTP response has the HTTP status 500 Internal Server Error |
| **ShouldBe501NotImplemented()** | Asserts that an HTTP response has the HTTP status 501 Not Implemented |
| **ShouldBe502BadGateway()** | Asserts that an HTTP response has the HTTP status 502 Bad Gateway |
| **ShouldBe503ServiceUnavailable()** | Asserts that an HTTP response has the HTTP status 503 Service Unavailable |
| **ShouldBe504GatewayTimeout()** | Asserts that an HTTP response has the HTTP status 504 Gateway Timeout |
| **ShouldBe505HttpVersionNotSupported()** | Asserts that an HTTP response has the HTTP status 505 Http Version Not Supported |
| **ShouldBe102Processing()** | Asserts that an HTTP response has the HTTP status 102 Processing |
| **ShouldBe103EarlyHints()** | Asserts that an HTTP response has the HTTP status 103 Early Hints |
| **ShouldBe207MultiStatus()** | Asserts that an HTTP response has the HTTP status 207 Multi-Status |
| **ShouldBe208AlreadyReported()** | Asserts that an HTTP response has the HTTP status 208 Already Reported |
| **ShouldBe226IMUsed()** | Asserts that an HTTP response has the HTTP status 226 IM Used |
| **ShouldBe421MisdirectedRequest()** | Asserts that an HTTP response has the HTTP status 421 Misdirected Request |
| **ShouldBe423Locked()** | Asserts that an HTTP response has the HTTP status 423 Locked |
| **ShouldBe424FailedDependency()** | Asserts that an HTTP response has the HTTP status 424 Failed Dependency |
| **ShouldBe425TooEarly()** | Asserts that an HTTP response has the HTTP status 425 Too Early |
| **ShouldBe428PreconditionRequired()** | Asserts that an HTTP response has the HTTP status 428 Precondition Required |
| **ShouldBe431RequestHeaderFieldsTooLarge()** | Asserts that an HTTP response has the HTTP status 431 Request Header Fields Too Large |
| **ShouldBe451UnavailableForLegalReasons()** | Asserts that an HTTP response has the HTTP status 451 Unavailable For Legal Reasons |
| **ShouldBe506VariantAlsoNegotiates()** | Asserts that an HTTP response has the HTTP status 506 Variant Also Negotiates |
| **ShouldBe507InsufficientStorage()** | Asserts that an HTTP response has the HTTP status 507 Insufficient Storage |
| **ShouldBe508LoopDetected()** | Asserts that an HTTP response has the HTTP status 508 Loop Detected |
| **ShouldBe510NotExtended()** | Asserts that an HTTP response has the HTTP status 510 Not Extended |
| **ShouldBe511NetworkAuthenticationRequired()** | Asserts that an HTTP response has the HTTP status 511 Network Authentication Required |

```csharp
response.ShouldBe2XXSuccessful();
response.ShouldBe404NotFound();
response.ShouldBe5XXServerError();
```

A failure here looks exactly like the status code outputs shown above: the expected/actual status codes, then the full response and the originating request, in the `should be`/`but was` layout.

### FluentAssertions.Web

#### FluentAssertions.Web vs FluentAssertions.Mvc vs FluentAssertions.Http

**FluentAssertions.Web** does not extend the assertions for the ASP.NET Core *Controllers*, if you are looking for that, then consider [FluentAssertions.Mvc](https://github.com/fluentassertions/fluentassertions.mvc).

When FluentAssertions.Web was created, [FluentAssertions.Http](https://github.com/balanikas/FluentAssertions.Http) also existed at the time, solving the same problem when considering the asserting language.
Besides the extra assertions added by FluentAssertions.Web, an important effort is put by this library on what happens when a test fails.

A complete integration test is in [End-to-end example: FluentAssertions.Web](#end-to-end-example-fluentassertions-and-awesomeassertions).

#### The HttpResponsesMessage assertions from FluentAssertions vs. FluentAssertions.Web

In the [6.4.0](https://fluentassertions.com/releases/#640) release FluentAssertions introduced a set of related assertions: *BeSuccessful, BeRedirection, HaveClientError, HaveServerError, HaveError, HaveStatusCode, NotHaveStatusCode*.

This library can still be used with FluentAssertions and it did not become obsoleted, not only because of the rich set of assertions, but also for the comprehensive output messages that are displayed when the test fails, feature that is not present in the main library, but in FluentAssertions.Web one.

### FluentAssertions.Web.v8

Starting 8.0.0, FA is not an FOSS anymore. **FluentAssertions.Web** will maintain both FOSS (< 8.0.0) and the Commercial versions of FA (>= 8.0.0), so they will be deployed as separate Nuget packages:
    - **FluentAssertions.Web** will continue to dependend on the FOSS versions
    - **FluentAssertions.Web.v8** will dependend on the Commercial versions

A complete integration test is in [End-to-end example: FluentAssertions.Web.v8](#end-to-end-example-fluentassertions-and-awesomeassertions).

### AwesomeAssertions.Web

AwesomeAssertions.Web is the AwesomeAssertions flavour of the same assertions. It exposes the identical `Should()`/`And` API as the FluentAssertions flavour, so all the examples and the [Full API](#full-api) above apply unchanged. It depends on AwesomeAssertions instead of FluentAssertions.

A complete integration test is in [End-to-end example: AwesomeAssertions.Web](#end-to-end-example-fluentassertions-and-awesomeassertions).

### Shouldly.Web

Shouldly.Web is the Shouldly flavour, a community-maintained, unofficial extension that is **not endorsed by the Shouldly project**. It requires Shouldly >= 5.0.0-preview.2 and is itself prerelease until Shouldly 5.0.0 is stable.

A complete integration test is in [End-to-end example: Shouldly.Web](#end-to-end-assertion-example-shouldlyweb).

Instead of `response.Should().Be200Ok()`, Shouldly's `ShouldlyMethodsAttribute` lets the same assertions be called directly on the response, and each assertion returns `void` — there is no `Should()`/`And` chain, so every assertion is its own call (you can also pass a custom message as the first optional argument):

```csharp
response.ShouldBe200Ok();
response.ShouldBe400BadRequest();
response.ShouldHaveError("Author", "*required*");
response.ShouldBeAs(new { Author = "John", Content = "Hey, you..." });
response.ShouldSatisfy<IEnumerable<Comment>>(comments => comments.ShouldHaveCount(2));
response.ShouldHaveHeader("X-Correlation-ID");
response.ShouldMatchInContent("*\"author\"*");
```

> No chaining is available in the Shouldly flavour: the assertions return `void`, so each call is a standalone assertion.

The named assertions map to the FluentAssertions/AwesomeAssertions ones described in the [Full API](#full-api) section, and are listed in full, following the same structure, in [Shouldly.Web API](#shouldlyweb-api). Worked examples are in [Shouldly.Web Examples](#shouldlyweb-examples):

> **Note on multiple failures:** To report *all* failing assertions, pass each one as a separate condition (a collection expression, array or `IEnumerable` of `Action`s), exactly as FluentAssertions' `Should().Satisfy(...)` reports every statement of a multi-statement lambda: each failing condition gets its own `Error 1`, `Error 2`, … block. A single assertion lambda runs as one condition and stops at its first failure. The [shared specs](https://github.com/adrianiftode/Assertions.Web/tree/master/test/FluentAssertions.Web.Tests) show both forms.

| FluentAssertions.Web | Shouldly.Web |
|---|---|
| `Should().Be200Ok()` | `ShouldBe200Ok()` |
| `Should().BeAs<T>()` | `ShouldBeAs<T>()` |
| `Should().BeEmpty()` | `ShouldBeEmpty()` |
| `Should().HaveHeader()` / `NotHaveHeader()` | `ShouldHaveHeader()` / `ShouldNotHaveHeader()` |
| `Should().HaveHeader().And.BeValue()` | `ShouldHaveHeaderWithValue()` |
| `Should().HaveHeader().And.BeValues()` | `ShouldHaveHeaderWithValues()` |
| `Should().HaveHeader().And.Match()` | `ShouldHaveHeaderMatching()` |
| `Should().HaveHeader().And.BeEmpty()` / `NotBeEmpty()` | `ShouldHaveEmptyHeader()` / `ShouldHaveNonEmptyHeader()` |
| `Should().Satisfy<T>()` / `Should().Satisfy()` | `ShouldSatisfy<T>()` / `ShouldSatisfy()` |
| `Should().MatchInContent()` | `ShouldMatchInContent()` |
| `Should().HaveHttpStatusCode()` / `NotHaveHttpStatusCode()` | `ShouldHaveHttpStatusCode()` / `ShouldNotHaveHttpStatusCode()` |
| `Should().HaveLocation()` etc. | `ShouldHaveLocation()`, `ShouldHaveLocationWithValue()`, `ShouldHaveLocationWithValues()`, `ShouldHaveLocationMatching()`, `ShouldNotHaveLocation()` |
| `Should().Be400BadRequest().And.HaveError()` | `ShouldHaveError()` |
| `Should().Be400BadRequest().And.OnlyHaveError()` | `ShouldOnlyHaveError()` |
| `Should().Be400BadRequest().And.NotHaveError()` | `ShouldNotHaveError()` |
| `Should().Be400BadRequest().And.HaveErrorMessage()` | `ShouldHaveErrorMessage()` |

The failure messages follow the Shouldly layout, so the expected/actual values are rendered as `should be`/`but was`, followed by the same HTTP conversation dump. When a custom message is passed it is reported under `Additional Info`:

```text
  Failed Sample.Api.Tests.CommentsControllerTests.Post_ReturnsCreated [614 ms]
  Error Message:
   response.StatusCode
    should be
HttpStatusCode.Created
    but was
HttpStatusCode.OK

Additional Info:
    we need it

The HTTP response was:

HTTP/1.1 200 OK

The originating HTTP request was <null>.
  Stack Trace:
     ...
```

The dump after `The HTTP response was:` is produced by the same `HttpMessageFormatter` the other flavours use, so everything said about the [failure output](#when-a-test-fails-you-see-the-whole-conversation) applies here as well.

### Optional Global Configuration

> **Breaking in 3.0:** the separate holders `FluentAssertionsWebConfig` and `AwesomeAssertionsWebConfig` are replaced by the single `AssertionsWebConfig` (from `Assertions.Web`, referenced by every flavour), which carries both `Serializer` and `ResponseFormatterOptions`. See [Migrating to 3.0](#migrating-to-30) for the full guide.

#### Deserialization

##### System.Text.Json

By default `System.Text.Json` is used to deserialize the response content. The related `System.Text.Json.JsonSerializerOptions` used to configure the serializer is accessible via the `SystemTextJsonSerializerConfig.Options` static field from Assertions.Web. So if you want to make the serializer case sensitive, then the related setting is changed like this:

```csharp
SystemTextJsonSerializerConfig.Options.PropertyNameCaseInsensitive = false;
```

The change must be done before the test is run and this depends on the testing framework. Check the NewtonsoftSerializerTests from this repo to see how it can be done with xUnit.

##### Newtonsoft.Json

Newtonsoft.Json support is **optional**: `System.Text.Json` is the default serializer and is already shipped with every assertion library. Install this package only if you want to switch. The serializer itself is also replaceable — you can implement your own, by implementing the `ISerializer` interface.

The Newtonsoft.Json serializer ships as the single **Assertions.Web.Serializers.NewtonsoftJson** package, shared by every flavour:

```
dotnet add package Assertions.Web.Serializers.NewtonsoftJson
```

[![NuGet](https://img.shields.io/nuget/v/Assertions.Web.Serializers.NewtonsoftJson.svg?label=Assertions.Web.Serializers.NewtonsoftJson)](https://www.nuget.org/packages/Assertions.Web.Serializers.NewtonsoftJson)


To set the default serializer to **Newtonsoft.Json** one, use the following configuration:

```csharp
AssertionsWebConfig.Serializer = new NewtonsoftJsonSerializer();
```

The related `Newtonsoft.Json.JsonSerializerSettings` used to configure the Newtonsoft.Json serializer is accesible via the `NewtonsoftJsonSerializerConfig.Options` static field. So if you want to add a custom converter, then the related setting is changed like this:

```csharp
NewtonsoftJsonSerializerConfig.Options.Converters.Add(new YesNoBooleanJsonConverter());
```

##### Choosing the serializer without touching the global state

`AssertionsWebConfig.UseSerializer` overrides the serializer for the current async flow until the returned scope is disposed, in every flavour. Scopes nest and do not leak to other tests running in parallel. The serializers can be given their own options for that purpose:

```csharp
var apiOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { Converters = { new MyConverter() } };

using (AssertionsWebConfig.UseSerializer(new SystemTextJsonSerializer(apiOptions)))
{
    response.Should().Satisfy<Person>(person => person.FirstName.Should().Be("Ada"));
}

using (AssertionsWebConfig.UseSerializer(new NewtonsoftJsonSerializer(new JsonSerializerSettings { Converters = { new YesNoBooleanJsonConverter() } })))
{
    response.ShouldBeAs(new { Approved = true });
}
```

The parameterless constructors keep reading the global `SystemTextJsonSerializerConfig.Options` and `NewtonsoftJsonSerializerConfig.Options`. Inside a scope, reading `AssertionsWebConfig.Serializer` returns the scoped serializer, while setting it still changes the global one.

> **Warning:** the assertions rely on the default deserialization settings. For System.Text.Json these are case-insensitive property names, enums and numbers read from strings, trailing commas, and an empty string read as `null` for nullable value types. Options or settings that change them can make an assertion behave unexpectedly, for example leave a property `null` instead of failing. To keep the defaults, start from a copy: `new JsonSerializerOptions(SystemTextJsonSerializerConfig.Options) { ... }`.

#### Response Formatting

The assertion failure messages include a readable rendering of the HTTP response (see [When a test fails, you see the whole conversation](#when-a-test-fails-you-see-the-whole-conversation) for an example). By default, only the first `10 * 128 * 1024` bytes of the response content are printed, the rest being replaced by a warning message. To change this limit globally, set the `ResponseFormatterOptions`:

```csharp
AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
{
    MaximumReadableBytes = 4 * 1024
};
```

The change must be done before the test is run, like for the serializer configuration above. The global options only apply to the messages produced by the assertion packages. The **HttpMessageFormatter** library itself keeps no global state and accepts the same `HttpResponseFormatterOptions` parameter object per call:

```csharp
var formatted = response.Format(new HttpResponseFormatterOptions
{
    MaximumReadableBytes = 4 * 1024
});
```

Besides the content limit, `HttpResponseFormatterOptions` exposes two optional delegates that give full control over the formatted output of both the HTTP response and the originating HTTP request:

- `ResponseFormatter` (`Func<HttpResponseMessage, string>`) — when configured, it replaces the built-in formatting entirely. It receives the complete `HttpResponseMessage` (the originating request is available through its `RequestMessage` property) and returns the text printed into the failure message. When it is set, the built-in pipeline, including the content processors and the `MaximumReadableBytes` limit, is bypassed.
- `PostFormat` (`Func<string, string>`) — when configured, it transforms the final formatted output, whether it was produced by the built-in formatting or by `ResponseFormatter`. It can be used, for example, to make escaped newlines in stack traces readable, to redact sensitive values like cookies or API keys, or to normalize volatile values like correlation ids.

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

Both delegates are optional; when neither is set, the formatting is unchanged. If a delegate throws, the exception is reported into the formatted output, similar to any other formatting error, so a failing customization cannot break the assertion message itself.

##### Changing the response formatting without touching the global state

Like for the serializer, `AssertionsWebConfig.UseResponseFormatterOptions` overrides the response formatter options for the current async flow until the returned scope is disposed, in every flavour. Scopes nest and do not leak to other tests running in parallel:

```csharp
using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpResponseFormatterOptions
{
    PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
}))
{
    response.Should().Satisfy<Person>(person => person.FirstName.Should().Be("Ada"));
}
```

Inside a scope, reading `AssertionsWebConfig.ResponseFormatterOptions` returns the scoped options, while setting it still changes the global one.

### HttpResponse Formatter

The internal HTTP Request/Response formatter used by the assertion packages is published as the **shared** **HttpMessageFormatter** package. It is not an assertion library: it carries no dependency on any assertion framework, so beyond powering the failure output of the assertion packages it can be reused in any other context where HTTP requests and responses need to be rendered for inspection and debugging.

Basic usage:
 - start from an HTTPResponseMessage instance
 - reference the HttpMessageFormatter package from Nuget
 - import the extension method _using HttpMessageFormatter;_
 - call the *Format* method on the HttpResponseMessage instance

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

---

## Prompt for AI agents and rapid test authoring

Use this prompt when asking an agent to add, migrate or extend HTTP response tests with this library. It restates the version-sensitive rules that cannot be inferred reliably from method names alone.

```text
You are adding or updating HTTP integration tests for <endpoint under test> in <project or repository>. Use the assertion-framework integration already present in the test project. Treat the response as an HTTP conversation: status, headers, Location, body and failure diagnostics.

0. Before editing
- Read the test project file, central package file (Directory.Packages.props if present), and one or two existing integration tests. Identify the assertion framework, its version, test framework, namespaces, naming conventions and the full-suite command.
- Copy the using directives from an existing test that uses the same integration. Do not guess namespaces.
- Use exactly one HTTP assertion flavour. The FluentAssertions and AwesomeAssertions flavours both define Should() for HttpResponseMessage, so installing more than one creates ambiguous calls. If no integration exists, choose the package matching the assertion framework the tests already use. Use published NuGet packages only, never a project reference to library source.
- Keep the response disposable (using var response = await ...) and assert while it is still readable.

1. Flavour rules
- FluentAssertions < 8.0.0: install FluentAssertions.Web.
- FluentAssertions >= 8.0.0 (commercial): install FluentAssertions.Web.v8. Should() and the assertion classes live in the FluentAssertions namespace for this flavour.
- AwesomeAssertions: install AwesomeAssertions.Web. Should() and core assertions come from AwesomeAssertions; the assertion classes live in AwesomeAssertions.Web.
- Shouldly >= 5.0.0-preview.2: install Shouldly.Web --prerelease and use the Shouldly namespace. Its calls are flat void extension methods: no Should(), no .And and no group transitions. Each call is an independent statement.
- There is no standalone Assertions.Web NuGet package. Assertions.Web is the shared configuration contract packed into every assertion package, not the normal test entry point.

2. Discover the installed API
- Read the installed package Readme.md and XML documentation from the NuGet global packages folder (%USERPROFILE%\.nuget\packages on Windows, ~/.nuget/packages elsewhere) before guessing names or parameter order. Version-specific details matter more than web search results.
- Confirm exact signatures for status, header, Location, body and error assertions, including wildcard syntax and return types.

3. Fluent chains (FluentAssertions / AwesomeAssertions)
- Start with response.Should(). Assert the exact status with a named method such as Be200Ok(), Be201Created(), Be400BadRequest(), Be404NotFound() or Be415UnsupportedMediaType(). Use a family assertion such as Be2XXSuccessful() only when the exact code is intentionally not part of the contract.
- Return types control chaining. .And exposes the group returned by the previous assertion, not a uniform menu. Specialized groups inherit general methods, but group-only methods are not everywhere. If a call does not compile, do not invent an API: call the entry assertion that returns the needed group or start a new response.Should() chain.
- Be201Created() returns LocationAssertions, but it does not assert that Location exists. Write Be201Created().And.HaveLocation().And.Match("*/api/..."). HaveLocation() is available on location-bearing status groups and, in FA 6/7, as an extension from the initial chain. It is not reachable after Be400BadRequest() or from a header group. Use wildcards instead of expecting a dedicated ends-with assertion.
- In FA v8 and AwesomeAssertions, call Be400BadRequest() before HaveError(...), OnlyHaveError(...), NotHaveError(...) or HaveErrorMessage(...). FA 6/7 also exposes these as extensions from the initial chain, but status-first is clearer and portable. Keep validation assertions immediately after the status. Field names are framework-emitted names, so use wildcards for volatile messages. HaveErrorMessage checks error messages, not necessarily ProblemDetails.title.
- HaveHeader(name) asserts presence and binds the chain to that header. Use BeValue when the header has exactly one complete value, BeValues when the complete value collection is expected, and Match(wildcard) when at least one value should match. Assertions never split comma-containing values. For Allow: GET, POST exposed as two values, use BeValues(new[] { "GET", "POST" }); if .NET exposes one raw GET, POST value, use BeValue("GET, POST"). Header names are case-insensitive in every assertion, including BeValues.
- There is no Content-Type helper; media types use the generic header API. JSON: HaveHeader("Content-Type").And.Match("application/json*"). ProblemDetails: HaveHeader("Content-Type").And.Match("application/problem+json*"). Vendor +json: HaveHeader("Content-Type").And.Match("application/vnd.api+json*"). Wildcard matching is anchored and case-sensitive, so include the trailing * (it accepts the charset parameter) and write media types in lower case. Include the full subtype, because application/json* also matches application/json-patch+json. Shouldly: ShouldHaveHeaderMatching("Content-Type", "application/json*"). See "Asserting media types" in docs/Assertions.Web/Readme.md.
- Treat Satisfy<T>(...) and Satisfy(...) as arbitrary C# extension points over a deserialized model or the response itself: inspect values, perform calculations, branch, loop, call helpers, use any framework assertion, and await in the async overloads. They are not limited to assertion chaining. When code inside either callback fails, the library reports the failure with the complete request/response conversation. Preserving HTTP context is a library-wide capability, but it is especially important here because otherwise arbitrary test code remains inside that context. Use BeAs(model) only when the complete expected object graph is known: it deserializes into expectedModel.GetType() and compares the whole model. Use MatchInContent(...) for deliberate wire-text checks, and plain framework assertions only inside Satisfy<T> or for non-HTTP values. Satisfy(...) on the response itself does not deserialize the body.
- After header work, start a fresh response.Should() chain before content checks. This is essential for BeEmpty(): HeadersAssertions.BeEmpty() checks the selected header's values, while response.Should().BeEmpty() checks the body. General methods such as BeAs and Satisfy are inherited, so a fresh chain is a clarity guard there, not always a compiler requirement.

4. Shouldly style
- Translate fluent calls to standalone calls: ShouldBe201Created(), ShouldHaveLocation(), ShouldHaveLocationMatching(...), ShouldHaveHeader(...), ShouldHaveHeaderWithValue(...), ShouldHaveHeaderWithValues(...), ShouldHaveHeaderMatching(...), ShouldBeAs(...), ShouldSatisfy<T>(...), ShouldHaveError(...), ShouldNotHaveLocation(), ShouldMatchInContent(...) and ShouldBeEmpty().
- Never write .And or chain one HTTP assertion into another. Assert the status separately before validation, header or body checks. When several independent conditions are checked through ShouldSatisfy, prefer a collection of conditions so every failure is reported.

5. Common mistakes to avoid
- Do not install multiple flavours or reference library source.
- Do not assume a missing JSON property produces a validation error. Probe the API: explicit null, malformed JSON or an unsupported media type may be what actually yields 400 or 415.
- Do not assert exact framework-generated messages, volatile traceIds or absolute test-host URLs when a wildcard path is enough. For ProblemDetails, model the standard fields with BeAs and assert a generated traceId extension through Satisfy<T> by presence or format, not by an exact value.
- Do not replace library HTTP assertions with raw status/header/content reads when an equivalent exists. In particular, do not manually deserialize and then use only plain framework assertions when a custom check matters: use Satisfy<T>() or response Satisfy(...) so failures include the HTTP conversation.
- Do not mutate global serializer or response formatter settings in a normal test.
- Do not weaken an existing assertion to make a migration easier.

6. Verification and report
- Run the complete suite with the project's command, normally dotnet test --nologo from the repository root. Do not claim success unless the command exits successfully.
- If compilation fails, inspect the current assertion group and the installed XML docs; fix the transition rather than changing packages. If an assertion fails, read the complete request/response dump.
- Optionally mutate an expected status, header, count or property once to prove the new assertion can fail, then restore it.
- Report the exact package ID/version chosen or reused, files changed, endpoint scenarios asserted, exact assertion calls, any limitations encountered, and the exact command with Passed, Failed, Skipped and Total counts.
```

The benchmark sessions that produced the best results failed most often on discoverability rather than library behaviour: selecting the wrong package, misunderstanding which group `.And` exposed, reaching `HaveLocation()` from the wrong chain, and treating a multi-value header as one comma-separated value. This prompt turns those findings into a pre-flight checklist and requires evidence-based verification instead of an unsupported "tests passed" claim.
