# CONVENTIONS.md

Conventions for modifying **FluentAssertions.Web** — an assertion extension library over
`System.Net.Http.HttpResponseMessage`. Derived from the `master` branch.

Findings are tagged **[Established]** (repeated throughout / explicitly configured), **[Likely]**
(several examples, not universal) or **[Unclear]** (limited or contradictory evidence).

---

## 1. What this library is

It adds HTTP-specific assertions (`response.Should().Be200Ok()`, `.BeAs<T>()`, `.HaveHeader()`,
`.HaveError()`, `.Satisfy()`) and — more importantly — makes every failure message print the **whole HTTP
request and response** so a failing integration test can be diagnosed without a debugger.

It asserts on `HttpResponseMessage` only. There is no `HttpRequestMessage` assertion surface (the request is
read from `response.RequestMessage` for *reporting*, never asserted directly). **[Established]**

---

## 2. Repository layout

```
src/
 FluentAssertions.Web/ ← THE single source of truth for all assertion code
 FluentAssertions.Web.v8/ ← no .cs files; link-compiles the above with FAV8
 AwesomeAssertions.Web/ ← no .cs files; link-compiles the above with FAV8;AAV
 Assertions.Web/ ← ISerializer, AssertionsWebConfig, DeserializationException, shared contract of every flavour
 HttpMessageFormatter/ ← assertion-framework-agnostic HTTP formatter (own NuGet package)
 FluentAssertions.HttpMessageFormatter/ ← 20-line IValueFormatter adapter onto the above
 AwesomeAssertions.HttpMessageFormatter/ ← link-compiles the above with AAV
 *.Web.Serializers.NewtonsoftJson/ ← optional Newtonsoft serializer, one package per flavour
test/
 FluentAssertions.Web.Tests/ ← THE single source of truth for all specs
 FluentAssertions.Web.v8.Tests/ ← link-compiles the above with FAV8
 AwesomeAssertions.Web.Tests/ ← link-compiles the above with FAV8;AAV
 Assertions.Web.Tests/ ← specs of the shared contract assembly (AssertionsWebConfig, DeserializationException)
 HttpMessageFormatter.Tests/ ← formatter-only, framework-agnostic
 Sample.Api.Tests/ (+ .v8, .AwesomeAssertions) ← end-to-end tests against samples/
samples/ ← ASP.NET Core sample APIs used by the e2e tests
```

### 2.1 The multi-flavour compilation model — read this before editing anything **[Established]**

One source tree ships as **three** packages. The sibling projects contain *no* `.cs` files of their own; they
link the canonical files:

```xml
<!-- src/AwesomeAssertions.Web/AwesomeAssertions.Web.csproj -->
<PropertyGroup><DefineConstants>$(DefineConstants);FAV8;AAV</DefineConstants></PropertyGroup>
<Compile Include="..\FluentAssertions.Web\**\*.*"
 Exclude="**\bin\**;**\obj\**;**\Properties\**;**\*.csproj;**\GlobalUsings.cs">
 <Link>%(RecursiveDir)%(Filename)%(Extension)</Link>
</Compile>
```

Consequences you must respect:

| Rule | Why |
|---|---|
| Edit only `src/FluentAssertions.Web/**` and `test/FluentAssertions.Web.Tests/**`. Never add a `.cs` file to `*.v8`, `*.AwesomeAssertions*` projects. | They are generated views; a new file is picked up automatically by the glob. |
| A new file needs **no csproj change** anywhere. | The glob is recursive. |
| Every file that declares a namespace opens with the `#if AAV` namespace switch. | `AwesomeAssertions.Web` must not leak a `FluentAssertions` namespace. |
| Three symbols exist: nothing (FA 6/7), `FAV8` (FA ≥ 8), `FAV8;AAV` (AwesomeAssertions). There is **no** `AAV`-without-`FAV8` combination. | AwesomeAssertions 9 is an FA-8-shaped API. |
| `GlobalUsings.cs` is excluded from the *AwesomeAssertions* glob (it has its own) but **not** from the *v8* glob (v8 reuses the `FluentAssertions.*` usings). | Namespace differences. |

The canonical namespace header, verbatim:

```csharp
#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif
```

---

## 3. Where a new assertion belongs

| The assertion is about… | File in `src/FluentAssertions.Web/` | Declared on |
|---|---|---|
| a status code | `HttpStatusCodeAssertions.cs` | `partial class HttpResponseMessageAssertions` |
| the response body | `HttpResponseContentAssertions.cs` | `partial class HttpResponseMessageAssertions` |
| an arbitrary header, or header presence | `HeadersAssertions.cs` | presence → `partial class HttpResponseMessageAssertions`; value-level → `class HeadersAssertions` |
| the `Location` header | `LocationAssertions.cs` | `class LocationAssertions : HeadersAssertions` |
| an ASP.NET Core `ValidationProblemDetails` body | `BadRequestAssertions.cs` | `class BadRequestAssertions : HttpResponseMessageAssertions` |
| running a user lambda against the response / a deserialized model | `SatisfyHttpResponseMessageAssertions.cs` / `SatisfyModelAssertions.cs` | `partial class HttpResponseMessageAssertions` |

**Every one of these files has a sibling `…Extensions.cs`** (`HttpStatusCodeAssertionsExtensions.cs`,
`HeadersAssertionsExtensions.cs`, …). Adding a public assertion means editing **two** files. **[Established]**

**Grouping inside a file:** `#region <MethodName>` … `#endregion` around each assertion (or each logical pair).
Used exhaustively in `HttpStatusCodeAssertions.cs` and the spec files. **[Established]**

---

## 4. How to implement an assertion

### 4.1 The instance method (in `*.Web` namespace)

```csharp
/// <summary>
/// Asserts that an HTTP response ....
/// </summary>
/// <param name="because">
/// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
/// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
/// </param>
/// <param name="becauseArgs">
/// Zero or more objects to format using the placeholders in <see paramref="because" />.
/// </param>
[CustomAssertion]
public AndConstraint<HttpResponseMessageAssertions> Be200Ok(string because = "", params object[] becauseArgs)
{
#if FAV8
 CurrentAssertionChain
#else
 Execute.Assertion
#endif
 .ForCondition(Subject is not null)
 .BecauseOf(because, becauseArgs)
 .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
 CurrentAssertionChain
#else
 Execute.Assertion
#endif
 .BecauseOf(because, becauseArgs)
 .ForCondition(HttpStatusCode.OK == Subject!.StatusCode)
 .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
 , HttpStatusCode.OK, Subject!.StatusCode, Subject);

 return new AndConstraint<HttpResponseMessageAssertions>(this);
}
```

Non-negotiables **[Established]**:

* `[CustomAssertion]` on **every** public assertion method (there is a guard test for it —
 `test/FluentAssertions.Web.Tests/Internal/ApiAccessibilityTests.cs`).
* The `#if FAV8 CurrentAssertionChain #else Execute.Assertion #endif` block, repeated at **each** assertion
 step. Do not hoist it into a helper — the existing code duplicates it every time.
* Trailing `string because = "", params object[] becauseArgs` and `.BecauseOf(because, becauseArgs)`.
* A guard step first: `ForCondition(Subject is not null)` → `"Expected a {context:response} to assert{reason}, but found <null>."`,
 then `Subject!` afterwards (required: `WarningsAsErrors` includes `CS8602`).
* Return `AndConstraint<T>`; `new AndConstraint<X>(this)` when staying on the same subject.
* Full XML docs on every public member (`GenerateDocumentationFile=True`). Copy the `because`/`becauseArgs`
 boilerplate verbatim; wildcard parameters also carry the standard `* / ?` `<remarks>` block.
  `CS1591` and `CS1573` are build errors in `src/Directory.Build.props`: every public member needs a
  meaningful `<summary>` and every parameter a `<param>`. Fluent assertions also document the returned
  `AndConstraint<T>` and what `.And` exposes next; `<remarks>` covers non-obvious behavior, when a new
  `response.Should()` chain is needed, and framework-specific composition rules.

**Argument validation is a throw, not an assertion failure** — `Guard.ThrowIfArgumentIsNull` /
`ThrowIfArgumentIsNullOrEmpty` (`Internal/Guard.cs`), called *before* the first `Execute.Assertion`, with a
domain-specific message: `"Cannot verify having a header against a <null> header."`. `BeAs` throws
`ArgumentNullException` directly for a null model; `BeValues` throws `ArgumentException` for an empty
collection. **[Established]**

`.ForCondition()` / `.BecauseOf()` call order is **inconsistent** across the codebase (both orders appear,
sometimes in the same method). Either compiles; prefer matching the neighbouring method. **[Unclear]**

### 4.2 The extension method (in `FluentAssertions` namespace)

FA 6/7 already ships `HttpResponseMessage.Should()`, so for that flavour the library *cannot* own `Should()`
and instead hangs extensions off FA's own primitive. FA 8 removed it, so `FAV8` defines its own `Should()` in
`HttpResponseMessageFluentAssertionsExtensions.cs`. Hence:

```csharp
#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary> ... </summary>
[DebuggerNonUserCode]
public static class HttpStatusCodeAssertionsExtensions
{
 /// <summary> ...same docs as the instance method... </summary>
 [CustomAssertion]
 public static AndConstraint<HttpResponseMessageAssertions> Be200Ok(
#pragma warning disable 1573
 this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
 string because = "", params object[] becauseArgs)
 => new HttpResponseMessageAssertions(parent.Subject).Be200Ok(because, becauseArgs);
}
#endif
```

Fixed elements **[Established]**: whole file wrapped in `#if !FAV8`; `namespace FluentAssertions;` (no `AAV`
switch needed — the file is excluded on the `FAV8` flavours that include `AAV`); `[DebuggerNonUserCode]` on the
class; `[CustomAssertion]` on each method; the `#pragma warning disable 1573` pair around the `parent`
parameter (undocumented-parameter warning); expression body delegating to a freshly constructed assertions
object; class name = `<Area>AssertionsExtensions`; XML docs duplicated from the instance method.

### 4.3 Chaining

Return type encodes **what the caller may chain next** — this is a deliberate design, not decoration. Changing an
existing return type is a public API change: it can break source and binary compatibility, so document the current
transition and chain restart instead unless a breaking change is explicitly approved. Every new specialized entry
point must also be documented in the shipped XML docs and the shared [transition
map](docs/Assertions.Web/Readme.md#transitions-between-assertion-groups).

* `Be400BadRequest()` → `AndConstraint<BadRequestAssertions>`, unlocking `.And.HaveError(...)`.
* `Be201Created()`, `Be3XXRedirection()`, `Be30xRedirect…` → `AndConstraint<LocationAssertions>`, unlocking
 `.And.HaveLocation()`.
* `HaveHeader("x")` → `AndConstraint<HeadersAssertions>`, unlocking `.And.BeValue("v")` / `.BeValues(...)` /
 `.Match(...)` / `.BeEmpty()` / `.NotBeEmpty()`.
* Everything else → `AndConstraint<HttpResponseMessageAssertions>`.

Sub-assertion classes derive from `HttpResponseMessageAssertions`, so the whole base surface stays reachable
after `.And.`. When returning a *different* subject object, construct it with the chain on `FAV8`:

```csharp
#if FAV8
 return new AndConstraint<HeadersAssertions>(new HeadersAssertions(Subject, expectedHeader, CurrentAssertionChain));
#else
 return new AndConstraint<HeadersAssertions>(new HeadersAssertions(Subject, expectedHeader));
#endif
```

Every assertions class overrides `protected override string Identifier` — `"HttpResponseMessage"`,
`"BadRequest"`, `"Header"`, `"Header.Location"`. Constructors are dual-signature (`FAV8` takes the extra
`AssertionChain assertionChain`). **[Established]**

### 4.4 Shared infrastructure — use it, don't reinvent it

`HttpResponseMessageAssertions.cs` provides `private protected` helpers: `GetContent()`,
`TryGetSubjectModel<TModel>(out …)`, `TryGetSubjectModel(out …, Type)`, and
`CollectFailuresFromAssertion<T>(Action<T>, T)`. `Internal/`: `HttpResponseMessageExtensions`
(`GetHeaders`, `GetHeaderValues`, `GetFirstHeaderValue`, `GetStringContent`, `GetJsonDocument`),
`JsonExtensions` (`GetPropertiesByName`, `GetChildrenNames`, `GetStringValuesOf`, `GetParentKey`),
`TaskExtensions.ExecuteInDefaultSynchronizationContext`, `StringExtensions`, `ObjectExtensions.ToJson`,
`Guard`, `AssertionsFailures`. **[Established]**

Async is always bridged, never exposed — public assertions are synchronous:

```csharp
Func<Task<JsonDocument>> jsonFunc = () => Subject.GetJsonDocument();
using var json = jsonFunc.ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult();
```

Never `.Result` or bare `.GetAwaiter().GetResult()` — always through
`ExecuteInDefaultSynchronizationContext()` (it wraps `NoSynchronizationContextScope` to avoid deadlocks).
**[Established]**

Header lookups are **case-insensitive** (`StringComparison.OrdinalIgnoreCase`) and span response **and content**
headers (`GetHeaders()` unions both). Do not read `response.Headers` directly. **[Established]**

---

## 5. Failure messages

Style rules, all **[Established]**:

* Begin with `Expected {context:response} to …` (`{context:response}` is a placeholder FA replaces; the
 null-guard variant is `Expected a {context:response} to assert{reason}, but found <null>.`).
* `{reason}` is placed immediately before the terminating `.` or `,` — e.g.
 `"…but it was {0}{reason}.{1}"`, `"…but was not found{reason}. {1}"`.
* Structure: **expectation, then `, but` + what was actually found**
 (`"to have a successful HTTP status code, but it was {0}"`).
* Lower-case after `Expected`; no trailing exclamation; single sentence.
* Negated form mirrors the positive by inserting `not`:
 `to contain the HTTP header {0}, but no such header was found in the actual response` ↔
 `to not contain the HTTP header {0}, but the header was found in the actual response`.
***Always pass `Subject` as the last `FailWith` argument.** The registered `HttpResponseMessageFormatter`
 expands it into the full formatted request+response dump. This is the library's whole reason to exist —
 omitting it produces a message indistinguishable from plain FluentAssertions.
* Nested/aggregated failures are wrapped in `new AssertionsFailures(failures)` and rendered by
 `AssertionsFailuresFormatter` as an indented ` - message` bullet list.
* Long messages are built by string concatenation across lines (`"Expected {context:response} to contain " +
 "the HTTP header {0}…"`), not interpolation — `{0}`-style indices must survive to FA.
* When a message must embed a value FA would otherwise quote oddly, raw interpolated string literals (`$$"""…"""`)
 are used with `{{expectedValue}}` doubled braces (see `HeadersAssertions.BeValue`). **[Likely]**
* Both formatters are registered once, in the `static HttpResponseMessageAssertions()` constructor:
 `Formatter.AddFormatter(new HttpResponseMessageFormatter()); Formatter.AddFormatter(new AssertionsFailuresFormatter());`

---

## 6. FluentAssertions integration

| Aspect | Convention |
|---|---|
| Version | `Directory.Packages.props` pins `FluentAssertions` to `[6.5.1,8.0.0)` — deliberately excluding the commercial 8.x. The v8 flavour opts in with `VersionOverride="8.0.0"`. **Do not widen the central range.****[Established]** |
| Base class | `ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>` |
| Assertion entry | `Execute.Assertion` (FA 6/7) / `CurrentAssertionChain` (FA 8 / AwesomeAssertions) |
| Collecting sub-failures | `using var scope = new AssertionScope(); …; failures = scope.Discard();` then feed the result into a `ForCondition(failures.Length == 0)` + `AssertionsFailures` **[Established]** |
| Wildcard matching | delegate to FA's own `value.Should().Match(pattern)` **inside a discarded `AssertionScope`**, used as a boolean predicate. Never hand-roll a matcher. |
| Equivalency | `subjectModel.Should().BeEquivalentTo(expectedModel, options)`; the options delegate type differs per flavour: `Func<EquivalencyOptions<TModel>,…>` on `FAV8`, `Func<EquivalencyAssertionOptions<TModel>,…>` otherwise |
| Formatting | `IValueFormatter` implementations; the HTTP one is a thin adapter over the framework-agnostic `HttpMessageFormatter` package |

---

## 7. HTTP/web domain

Only `HttpResponseMessage` is asserted. Body handling: content is read as string once, or as a
`System.Text.Json.JsonDocument` for the BadRequest assertions. Deserialization goes through the replaceable
`ISerializer` (`AssertionsWebConfig.Serializer`, default `SystemTextJsonSerializer`); failures surface as
`DeserializationException` and are converted into an assertion failure carrying the serializer's own message.
**[Established]**

BadRequest assertions understand both shapes ASP.NET Core emits — an `errors` wrapper object and a flat
top-level object — selecting via `json.GetPropertiesByName("errors").Any()`. Keep that dual handling in any new
BadRequest assertion. **[Established]**

No ASP.NET Core dependency in `src/FluentAssertions.Web` (only `Microsoft.AspNetCore.WebUtilities`, and only in
`src/HttpMessageFormatter` for multipart parsing). ASP.NET Core appears solely in `samples/` and in
`test/Sample.Api*.Tests` (via `WebApplicationFactory`). **[Established]**

---

## 8. Tests

**Framework:** xUnit v2 (`xunit` 2.9.2) + `Microsoft.NET.Test.Sdk`, `coverlet.collector`. TFMs `net10.0;net11.0`
(`test/Directory.Build.props`). Assertion tool: FluentAssertions itself (+ `FluentAssertions.Analyzers`).
**[Established]**

**Files:** `<Area>AssertionsSpecs.cs` in `test/FluentAssertions.Web.Tests/`, one class per production assertion
file, `#region <MethodName>` per assertion, same `#if AAV` namespace switch. `Internal/` subfolder for
internal-helper tests named `<Type>Tests.cs`. New spec files require no csproj edit and are automatically run by
all three flavours. **[Established]**

**Naming:** `When_<situation>_it_should_<expectation>` — e.g.
`When_asserting_response_with_no_headers_to_have_header_it_should_throw_with_descriptive_message`. The
`Internal/` helper tests instead use `Given<X>_Then<Y>`. **[Established]**

**Shape** — always three commented blocks, and always assert on a delegate, never on the assertion's own
return value:

```csharp
[Fact]
public void When_asserting_response_with_header_to_have_header_it_should_succeed()
{
 // Arrange
 using var subject = new HttpResponseMessage
 {
 Headers = { { "custom-header", "value1" } }
 };

 // Act
 Action act = () => subject.Should().HaveHeader("custom-header");

 // Assert
 act.Should().NotThrow();
}
```

**Constructing the subject****[Established]**:
* `using var subject = new HttpResponseMessage();` — `using var`, named `subject`.
* Status: `new HttpResponseMessage(HttpStatusCode.BadRequest)`.
* Headers via the collection initializer `Headers = { { "name", "value" } }`; a valueless header is
 `{ "custom-header", (string?)null }`.
* Body via `new StringContent(/*lang=json*/"""…""", Encoding.UTF8, "application/json")` — raw string literals
 with the `/*lang=json*/` hint for IDE JSON highlighting.
* No mocking framework anywhere in the repo. No fixtures except `IClassFixture<WebApplicationFactory<Startup>>`
 in the sample-API tests and the serializer-config fixtures.

**What every new assertion must be tested for** — the repeated four-case pattern **[Established]**:

1.**Success**: matching response → `act.Should().NotThrow();`
2.**Failure with diagnostics**: non-matching response, *always* passing the `because` arguments
 `("we want to test the {0}", "reason")`, asserted as
 `act.Should().Throw<XunitException>().WithMessage("*<key fragment>*<value>*reason*");`
 — a wildcard pattern that pins the message's distinguishing fragments **and** proves `because` flows through.
3.**Argument guards**: `subject.Should().HaveHeader(null!)` →
 `act.Should().Throw<ArgumentNullException>().WithMessage("*header*<null>*");`
4.**Null / empty subject and empty content** edge cases.

Negated assertions (`NotHaveHeader`, `NotHaveError`, `NotHaveLocation`, `NotHaveHttpStatusCode`) get their own
regions with the same four cases. **[Established]**

**API guard tests** — `test/FluentAssertions.Web.Tests/Internal/ApiAccessibilityTests.cs` reflects over the
assembly and will fail your change if: a type under a `*.Internal` namespace is public; an
`*AssertionsExtensions` type is outside the `FluentAssertions`/`AwesomeAssertions` namespace; a public
assertion method or extension lacks `[CustomAssertion]`; or an instance assertion has no matching extension
method (name, return type and parameters). Run this project after any public-API change. **[Established]**

**End-to-end tests** (`test/Sample.Api.Tests`) exercise the fluent chain against a real ASP.NET Core host and
are the place to demonstrate a new assertion in realistic use:
`response.Should().Be200Ok().And.BeAs(new { Author = "Adrian", Content = "Hey" });`

---

## 9. Public API & compatibility

* Adding a public assertion = instance method **+** extension method **+** `[CustomAssertion]` **+** XML docs
**+** specs, or `ApiAccessibilityTests` fails.
* XML docs are part of the public API contract: changing an assertion's returned group, composition rule, or
  framework-specific usage means updating its XML docs and the package READMEs/shared guide in the same change.
* It must compile under all three symbol sets. Any FA-8-only or FA-6/7-only API needs an `#if FAV8` branch.
* `netstandard2.0` for all `src/` projects (`src/Directory.Build.props`) — no newer BCL APIs.
 `HttpMessageFormatter` alone multi-targets `netstandard2.0;net8.0;net10.0`.
* `Nullable=enable` everywhere, and `WarningsAsErrors` includes `CS8600;CS8602;CS8603;CS1591;CS1573` —
 nullable and public XML documentation warnings **break the build**. Assertion parameters are non-nullable
 (`string expectedHeader`) and guarded by `Guard`; `Subject!` is used after the null-guard step.
* Internals are `internal` under an `Internal` namespace, exposed to tests via
 `[assembly: InternalsVisibleTo("FluentAssertions.Web.Tests")]` in `Properties/AssemblyInfo.cs`
 (`Properties/` is excluded from the link globs, so each flavour has its own).
* Versioning: each packable project carries its own `<Version>`; `eng/ReleasePackages.props` defines which
 paths and dependencies affect each package. There is deliberately no repo-wide `VersionPrefix` fallback and
 CI does not pass `/p:Version` when building or packing. There is **no documented SemVer policy** and no
 `[Obsolete]` member in the repo — don't invent one. **[Unclear]**

---

## 10. Naming & organization

| Thing | Convention |
|---|---|
| Implementation namespace | `FluentAssertions.Web` / `AwesomeAssertions.Web` (`.Internal` for helpers) |
| Extension namespace | `FluentAssertions` / `AwesomeAssertions` (with `// ReSharper disable once CheckNamespace`) |
| Assertions class | `<Area>Assertions` |
| Extensions class | `<Area>AssertionsExtensions` |
| Spec class / file | `<Area>AssertionsSpecs` |
| Status-code assertion | `Be<code><PascalCaseReasonPhrase>` — `Be200Ok`, `Be404NotFound`, `Be422UnprocessableEntity`; ranges `Be1XXInformational`…`Be5XXServerError` (each preceded by `// ReSharper disable once InconsistentNaming`) |
| Other assertions | FA verbs: `Have…` / `NotHave…` / `Be…` / `OnlyHave…` / `Match…` / `Satisfy` |
| Parameters | `expected…` / `unexpected…`; wildcard ones spelled out: `expectedWildcardErrorMessage`, `expectedWildcardText`, `expectedWildcardValue`; always trailing `because`, `becauseArgs` |
| Generic parameter | `TModel` (`TAsserted`, `TExpectation` in helpers) |
| Constants | `private const string ErrorsPropertyName = "errors";` |
| Protected field | PascalCase, e.g. `protected readonly string Header;` |
| Test subject variable | `subject`; delegate under test `act` |

---

## 11. C# style actually used

***File-scoped namespaces** in `src/` and `test/` (note: `.editorconfig` says
 `csharp_style_namespace_declarations = block_scoped:silent`; the code contradicts it, and
 `test/Sample.Api.Tests` still uses block-scoped. Follow the neighbouring file.) **[Unclear]**
* `GlobalUsings.cs` per project with `global using` for `System.*` and the framework namespaces; **no**
 `ImplicitUsings` in `src/FluentAssertions.Web` (only `src/HttpMessageFormatter` enables it).
* `var` for locals; explicit types for delegate locals (`Func<Task<JsonDocument>> jsonFunc = …`,
 `Action<string> …`) because the target type is needed.
* Expression-bodied members for one-liners and for every extension-method delegation; block bodies for
 assertion methods.
* `is not null` / `is null`, tuple returns `(bool success, string? errorMessage)`, `out` parameters,
 `using var`, LINQ throughout (`.Any(…)`, `.FirstOrDefault(…)`, `.SelectMany(…)`).
* `#pragma warning disable`/`restore` around specific known warnings, never file-wide.
* No records, no primary constructors, no collection expressions, no local functions in `src/`.
* CRLF, 4 spaces, `end_of_line = crlf` (`.editorconfig`).

---

## 12. Dependencies

Central Package Management: **all** versions live in `Directory.Packages.props`
(`ManagePackageVersionsCentrally=true`, `CentralPackageTransitivePinningEnabled=true`). `PackageReference`
elements carry **no** `Version` attribute; the only exceptions are deliberate `VersionOverride`s
(`FluentAssertions` 8.0.0 for the v8 flavour, `Microsoft.AspNetCore.Mvc.Testing` per TFM). Add a new dependency
by adding a `PackageVersion` there first. **[Established]**

Internal project references that must be *bundled into* the nupkg rather than declared as a dependency are
marked `PrivateAssets="all"` and collected by the `CopyProjectReferencesToPackage` target present in each
packable csproj. `HttpMessageFormatter` is the exception — a real, public package reference.
`FluentAssertions.HttpMessageFormatter` is `IsPackable=false`. **[Established]**

---

## 13. Build, test, package

```powershell
dotnet restore
dotnet build FluentAssertions.Web.slnx -c Release # must be warning-clean; CS8600/2/3, CS1591 and CS1573 are errors
dotnet test # runs ALL flavours × net10.0 + net11.0
```

Targeted runs while iterating:

```powershell
dotnet test test\FluentAssertions.Web.Tests
dotnet test test\FluentAssertions.Web.Tests --filter "FullyQualifiedName~HeadersAssertionsSpecs"
dotnet pack --include-symbols -c Release
./build/Test-PackageDocumentation.ps1
```

**Minimum verification before a commit:** `dotnet build` of the solution plus a **full** `dotnet test` from the
repo root. `dotnet test` on `FluentAssertions.Web.Tests` alone is **not** sufficient — the same specs must also
pass compiled as `FAV8` and as `FAV8;AAV`, and a flavour-specific compile error or message difference only
shows up in `FluentAssertions.Web.v8.Tests` / `AwesomeAssertions.Web.Tests`. **[Established]**

There is no formatting/lint step and no `global.json`. CI (`appveyor.yml`) additionally runs SonarCloud with
coverlet OpenCover coverage; coverage collection is opt-in via `/p:CollectCoverage=true`. CI also runs
`build/Test-PackageDocumentation.ps1` on every build (including PRs) to fail when a package is missing XML docs.
Packages are pushed to NuGet only on a tag matching `\d+\.\d+\.\d+`. A new packable project must be added to `appveyor.yml`
`artifacts` **and** `deploy`. **[Established]**

---

## 14. Git

Short imperative subject lines, no prefix convention, no body, no issue trailers — e.g. `Add missing
LocationAssertions`, `Make Be3XXRedirection chain with LocationAssertions`, `Remove confusion from Bad Request
failure message`. Work happens on `features/<topic>` or `fixes/<topic>` branches merged into `master` by PR.
`skip ci` in a message skips the build. **[Established]**

Do not modify: `samples/**` build output, `docs/**` fixtures (`AspNetCore22DeveloperPage.html` is test input),
`appveyor.yml` secure keys.

---

## 15. Easy things to get wrong

1.**Editing a linked copy.** Changing `src/AwesomeAssertions.Web/` or `test/AwesomeAssertions.Web.Tests/`
 achieves nothing — those trees only hold a csproj, `GlobalUsings.cs` and `Properties/`.
2.**Forgetting the second file.** An instance assertion without its `*AssertionsExtensions` counterpart fails
 `ApiAccessibilityTests`, and is unreachable for FA 6/7 users.
3.**Forgetting `[CustomAssertion]`.** Guard-tested; also what keeps FA's caller-identification from naming the
 library's own internals as the subject.
4.**Forgetting `Subject` as the last `FailWith` argument** — the rich HTTP dump silently disappears.
5.**Hard-coding `Execute.Assertion`.** Breaks the `FAV8` and AwesomeAssertions builds.
6.**Missing the namespace `#if AAV` switch** — leaks `FluentAssertions.*` types into `AwesomeAssertions.Web`.
7.**Reading `response.Headers` directly** — misses content headers and case-insensitivity; use
 `GetHeaders()` / `GetHeaderValues()`.
8.**Blocking on async** with `.Result`; use `ExecuteInDefaultSynchronizationContext()`.
9.**Writing a wildcard matcher**; reuse `Should().Match(pattern)` inside a discarded `AssertionScope`.
10.**Adding a `Version=` to a `PackageReference`** instead of a `PackageVersion` in `Directory.Packages.props`.
11.**Using a post-netstandard2.0 or FA-8-only API** without an `#if FAV8` branch.
12.**Asserting on the return value** in a spec instead of wrapping the call in `Action act = () => …`.
13.**Omitting the `because`/`reason` case** from the failure-message test — the `*reason*` fragment in
 `WithMessage` is what proves `BecauseOf` was wired up.