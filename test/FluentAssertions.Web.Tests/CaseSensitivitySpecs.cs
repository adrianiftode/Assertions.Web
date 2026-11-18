#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class CaseSensitivitySpecs
{
    [Fact]
    public void Header_names_are_case_insensitive_for_every_header_assertion()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.TryAddWithoutValidation("X-Case-Test", "one");
        response.Headers.TryAddWithoutValidation("X-Empty-Test", (string?)null);
        response.Content = new StringContent("{}", Encoding.UTF8, "application/json");

#if SH
        // Act / Assert
        response.ShouldHaveHeader("x-CASE-test");
        response.ShouldHaveHeaderWithValue("x-Case-TEST", "one");
        response.ShouldHaveHeaderWithValues("X-case-test", new[] { "one" });
        response.ShouldHaveHeaderMatching("x-CASE-TEST", "o*");
        response.ShouldHaveEmptyHeader("x-EMPTY-test");
        response.ShouldHaveNonEmptyHeader("X-Case-Test");
        response.ShouldHaveHeader("content-TYPE");
#else
        // Act / Assert
        response.Should().HaveHeader("x-CASE-test").And.BeValue("one");
        response.Should().HaveHeader("X-case-TEST").And.BeValues(new[] { "one" });
        response.Should().HaveHeader("x-CASE-TEST").And.Match("o*");
        response.Should().HaveHeader("x-EMPTY-test").And.BeEmpty();
        response.Should().HaveHeader("X-Case-Test").And.NotBeEmpty();
        response.Should().HaveHeader("content-TYPE");
#endif
    }

    [Fact]
    public void Single_header_values_ignore_case_for_compatibility()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.TryAddWithoutValidation("X-Trace", "MiXeD-CaSe");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderWithValue("x-TRACE", "mixed-case");
#else
        // Act / Assert
        response.Should().HaveHeader("x-trACE").And.BeValue("MIXED-CASE");
#endif
    }

    [Fact]
    public void Header_value_collections_are_case_sensitive()
    {
        // Arrange
        using var response = AllowResponse("GET", "POST");

#if SH
        // Act
        Action act = () => response.ShouldHaveHeaderWithValues("aLLoW", new[] { "get", "POST" });

        // Assert
        act.ShouldFailWith("response.Headers[\"aLLoW\"]",
            "should be",
            "get",
            "POST",
            "but was (case sensitive comparison)",
            "GET",
            "POST");
#else
        // Act
        Action act = () => response.Should().HaveHeader("aLLoW").And.BeValues(new[] { "get", "POST" });

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*aLLoW*get*differences*");
#endif
    }

    [Fact]
    public void Wildcards_are_case_sensitive_for_headers_and_content()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Headers.TryAddWithoutValidation("X-Media", "Application/JSON");
        response.Content = new StringContent("Hello World");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderMatching("x-MEDIA", "Application/JSON*");
        response.ShouldMatchInContent("*Hello*");

        Action headerAct = () => response.ShouldHaveHeaderMatching("X-Media", "application/json*");
        Action contentAct = () => response.ShouldMatchInContent("*hello*");

        headerAct.ShouldFailContaining("application/json*", "Application/JSON");
        contentAct.ShouldFailContaining("*hello*", "Hello World");
#else
        // Act / Assert
        response.Should().HaveHeader("x-MEDIA").And.Match("Application/JSON*");
        response.Should().MatchInContent("*Hello*");

        Action headerAct = () => response.Should().HaveHeader("X-Media").And.Match("application/json*");
        Action contentAct = () => response.Should().MatchInContent("*hello*");

        headerAct.Should().Throw<XunitException>()
            .WithMessage("*application/json*no match*");
        contentAct.Should().Throw<XunitException>()
            .WithMessage("*does not since*");
#endif
    }

    [Fact]
    public void Model_property_names_follow_the_serializer_while_string_values_follow_equivalency()
    {
        // Arrange
        using var matchingResponse = JsonResponse("""{"name":"Ada"}""");
        using var differentValueResponse = JsonResponse("""{"name":"Ada"}""");

#if SH
        // Act / Assert
        matchingResponse.ShouldBeAs(new CasingModel { Name = "Ada" });

        Action act = () => differentValueResponse.ShouldBeAs(new CasingModel { Name = "ADA" });
        act.ShouldFailContaining("\"ADA\"", "but was", "\"Ada\"");
#else
        // Act / Assert
        matchingResponse.Should().BeAs(new CasingModel { Name = "Ada" });

        Action act = () => differentValueResponse.Should().BeAs(new CasingModel { Name = "ADA" });
        act.Should().Throw<XunitException>()
            .WithMessage("*differences*Name*");
#endif
    }

    [Fact]
    public void Validation_field_names_are_case_insensitive_but_error_wildcards_are_case_sensitive()
    {
        // Arrange
        using var response = JsonResponse(
            """{"Errors":{"Author":["This field is Required."]}}""",
            HttpStatusCode.BadRequest);

#if SH
        // Act / Assert
        response.ShouldHaveError("aUTHOR", "*Required*");
        response.ShouldHaveErrorMessage("*Required*");

        Action act = () => response.ShouldHaveError("aUTHOR", "*required*");
        act.ShouldFailWith("response.Errors[\"aUTHOR\"]",
            "should have error message",
            "*required*",
            "This field is Required.");
#else
        // Act / Assert
        response.Should().Be400BadRequest().And.HaveError("aUTHOR", "*Required*");
        response.Should().Be400BadRequest().And.HaveErrorMessage("*Required*");

        Action act = () => response.Should().Be400BadRequest().And.HaveError("aUTHOR", "*required*");
        act.Should().Throw<XunitException>()
            .WithMessage("*required*This field is Required.*");
#endif
    }

    private sealed class CasingModel
    {
        public string? Name { get; set; }
    }

    private static HttpResponseMessage AllowResponse(params string[] values)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        };
        response.Content.Headers.TryAddWithoutValidation("Allow", values);
        return response;
    }

    private static HttpResponseMessage JsonResponse(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
}
