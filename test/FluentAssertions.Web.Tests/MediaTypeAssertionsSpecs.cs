#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class MediaTypeAssertionsSpecs
{
    [Fact]
    public void Json_media_type_matches_the_documented_wildcard_with_a_charset_parameter()
    {
        // Arrange
        using var response = MediaTypeResponse("application/json");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderMatching("Content-Type", "application/json*");
#else
        // Act / Assert
        response.Should().HaveHeader("Content-Type").And.Match("application/json*");
#endif
    }

    [Fact]
    public void Problem_details_media_type_matches_the_documented_wildcard()
    {
        // Arrange
        using var response = MediaTypeResponse("application/problem+json", HttpStatusCode.BadRequest);

#if SH
        // Act / Assert
        response.ShouldHaveHeaderMatching("Content-Type", "application/problem+json*");

        Action act = () => response.ShouldHaveHeaderMatching("Content-Type", "application/json*");
        act.ShouldFailContaining("application/json*", "application/problem+json");
#else
        // Act / Assert
        response.Should().Be400BadRequest();
        response.Should().HaveHeader("Content-Type").And.Match("application/problem+json*");

        Action act = () => response.Should().HaveHeader("Content-Type").And.Match("application/json*");
        act.Should().Throw<XunitException>()
            .WithMessage("*application/json*no match*");
#endif
    }

    [Fact]
    public void Vendor_json_media_type_matches_the_documented_wildcard()
    {
        // Arrange
        using var response = MediaTypeResponse("application/vnd.api+json");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderMatching("Content-Type", "application/vnd.api+json*");
#else
        // Act / Assert
        response.Should().HaveHeader("Content-Type").And.Match("application/vnd.api+json*");
#endif
    }

    [Fact]
    public void Short_wildcard_also_matches_longer_json_subtypes()
    {
        // Arrange: this documents the over-match called out in the media-type guidance.
        using var response = MediaTypeResponse("application/json-patch+json");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderMatching("Content-Type", "application/json*");
#else
        // Act / Assert
        response.Should().HaveHeader("Content-Type").And.Match("application/json*");
#endif
    }

    [Fact]
    public void Exact_value_assertion_compares_the_whole_content_type_including_charset()
    {
        // Arrange
        using var response = MediaTypeResponse("application/json");

#if SH
        // Act / Assert
        response.ShouldHaveHeaderWithValue("Content-Type", "application/json; charset=utf-8");

        Action act = () => response.ShouldHaveHeaderWithValue("Content-Type", "application/json");
        act.ShouldFailContaining("application/json", "application/json; charset=utf-8");
#else
        // Act / Assert
        response.Should().HaveHeader("Content-Type").And.BeValue("application/json; charset=utf-8");

        Action act = () => response.Should().HaveHeader("Content-Type").And.BeValue("application/json");
        act.Should().Throw<XunitException>()
            .WithMessage("*application/json*");
#endif
    }

    private static HttpResponseMessage MediaTypeResponse(string mediaType, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new(statusCode)
        {
            Content = new StringContent("{}", Encoding.UTF8, mediaType)
        };
}
