#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class AssertionChainingTests
{
    [Fact]
    public void When_chaining_a_general_status_and_body_assertion_then_the_general_group_remains_available()
    {
        // Arrange
        using var response = JsonResponse(HttpStatusCode.OK, """{ "author": "John" }""");

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldBe200Ok();
            response.ShouldBeAs(new { author = "John" });
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => response.Should()
            .Be200Ok()
            .And.BeAs(new { author = "John" });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_chaining_a_location_bearing_status_with_have_location_then_header_assertions_can_follow()
    {
        // Arrange
        using var response = JsonResponse(HttpStatusCode.Created, """{ "author": "John" }""");
        response.Headers.Location = new Uri("http://localhost/api/comments/1");

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldBe201Created();
            response.ShouldHaveLocation();
            response.ShouldHaveLocationMatching("*/api/comments/1");
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => response.Should()
            .Be201Created()
            .And.HaveLocation()
            .And.Match("*/api/comments/1")
            .And.BeAs(new { author = "John" });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_chaining_validation_assertions_then_the_bad_request_group_is_retained()
    {
        // Arrange
        using var response = BadRequestResponse();

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldBe400BadRequest();
            response.ShouldHaveError("Author", "*required*");
            response.ShouldHaveErrorMessage("*required*");
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => response.Should()
            .Be400BadRequest()
            .And.HaveError("Author", "*required*")
            .And.HaveErrorMessage("*required*");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_switching_from_validation_to_headers_then_a_new_chain_is_started()
    {
        // Arrange
        using var response = BadRequestResponse();
        response.Headers.Add("X-Correlation-ID", "b70e6f9e-b3c9-4dd3-8e6e-0f0a4c0f1d2a");

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldBe400BadRequest();
            response.ShouldHaveError("Author", "*required*");
            response.ShouldHaveHeaderMatching("X-Correlation-ID", "*-*");
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () =>
        {
            response.Should()
                .Be400BadRequest()
                .And.HaveError("Author", "*required*");

            response.Should()
                .HaveHeader("X-Correlation-ID")
                .And.Match("*-*");
        };

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_chaining_two_headers_then_the_second_header_becomes_the_subject()
    {
        // Arrange
        using var response = JsonResponse(HttpStatusCode.OK, """{ "author": "John" }""");
        response.Headers.Add("X-First", "first-value");
        response.Headers.Add("X-Second", "second-value");

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldHaveHeaderWithValue("X-First", "first-value");
            response.ShouldHaveHeaderWithValue("X-Second", "second-value");
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => response.Should()
            .HaveHeader("X-First")
            .And.Match("first-*")
            .And.HaveHeader("X-Second")
            .And.Match("second-*")
            .And.BeAs(new { author = "John" });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_a_304_response_needs_a_location_then_the_general_header_entry_point_is_used()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.NotModified);
        response.Headers.Location = new Uri("http://localhost/api/comments/1");

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldBe304NotModified();
            response.ShouldHaveLocation();
            response.ShouldHaveLocationMatching("*/api/comments/1");
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => response.Should()
            .Be304NotModified()
            .And.HaveHeader("Location")
            .And.NotBeEmpty()
            .And.Match("*/api/comments/1");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_an_empty_header_and_empty_content_then_each_be_empty_targets_its_own_subject()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        };
        response.Headers.Add("X-Optional", (string?)null);

        // Act
#if SH
        Action act = () =>
        {
            response.ShouldHaveEmptyHeader("X-Optional");
            response.ShouldBeEmpty();
        };

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () =>
        {
            response.Should().HaveHeader("X-Optional").And.BeEmpty();
            response.Should().BeEmpty();
        };

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_a_header_has_values_then_header_be_empty_fails_but_content_be_empty_can_pass()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        };
        response.Headers.Add("X-NonEmpty", "value");

        // Act
#if SH
        Action headerAct = () => response.ShouldHaveEmptyHeader("X-NonEmpty");
        Action contentAct = () => response.ShouldBeEmpty();

        // Assert
        headerAct.ShouldFailWith("response.Headers[\"X-NonEmpty\"]", "should be empty", "value");
        contentAct.ShouldNotThrow();
#else
        Action headerAct = () => response.Should().HaveHeader("X-NonEmpty").And.BeEmpty();
        Action contentAct = () => response.Should().BeEmpty();

        // Assert
        headerAct.Should().Throw<XunitException>()
            .WithMessage("*X-NonEmpty*no header values*");
        contentAct.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_be_empty_follows_a_location_bearing_status_then_it_targets_the_location_header()
    {
        // Arrange
        using var response = new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent(string.Empty)
        };
        response.Headers.Location = new Uri("http://localhost/api/comments/1");

        // Act
#if SH
        Action statusAct = () => response.ShouldBe201Created();
        Action locationAct = () => response.ShouldHaveEmptyHeader("Location");
        Action contentAct = () => response.ShouldBeEmpty();

        // Assert
        statusAct.ShouldNotThrow();
        locationAct.ShouldFailWith("response.Headers[\"Location\"]", "should be empty");
        contentAct.ShouldNotThrow();
#else
        Action locationAct = () => response.Should().Be201Created().And.BeEmpty();
        Action contentAct = () => response.Should().BeEmpty();

        // Assert
        locationAct.Should().Throw<XunitException>()
            .WithMessage("*Location*no header values*");
        contentAct.Should().NotThrow();
#endif
    }

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string json)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

    private static HttpResponseMessage BadRequestResponse()
        => JsonResponse(HttpStatusCode.BadRequest, """
            {
              "errors": {
                "Author": [
                  "The Author field is required."
                ]
              }
            }
            """);
}
