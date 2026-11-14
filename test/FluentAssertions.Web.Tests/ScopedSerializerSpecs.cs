#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class ScopedSerializerSpecs
{
    private class Person
    {
        public string? FirstName { get; set; }
    }

    private static readonly JsonSerializerOptions SnakeCaseOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private static HttpResponseMessage CreateResponse()
        => new()
        {
            Content = new StringContent(/*lang=json,strict*/ """{ "first_name" : "Ada" }""", Encoding.UTF8, "application/json")
        };

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_inside_a_serializer_scope_it_should_use_the_scoped_serializer()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () =>
        {
            using (AssertionsWebConfig.UseSerializer(new SystemTextJsonSerializer(SnakeCaseOptions)))
            {
#if SH
                subject.ShouldSatisfy<Person>(model => model.FirstName.ShouldBe("Ada"));
#else
                subject.Should().Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));
#endif
            }
        };

        // Assert
#if SH
        act.ShouldNotThrow();
#else
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_content_to_be_as_model_inside_a_serializer_scope_it_should_use_the_scoped_serializer()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () =>
        {
            using (AssertionsWebConfig.UseSerializer(new SystemTextJsonSerializer(SnakeCaseOptions)))
            {
#if SH
                subject.ShouldBeAs(new Person { FirstName = "Ada" });
#else
                subject.Should().BeAs(new Person { FirstName = "Ada" });
#endif
            }
        };

        // Assert
#if SH
        act.ShouldNotThrow();
#else
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_after_the_serializer_scope_it_should_use_the_global_serializer()
    {
        // Arrange
        using var subject = CreateResponse();
        using (AssertionsWebConfig.UseSerializer(new SystemTextJsonSerializer(SnakeCaseOptions)))
        {
        }

        // Act
        Action act = () =>
        {
#if SH
            subject.ShouldSatisfy<Person>(model => model.FirstName.ShouldBe("Ada"));
#else
            subject.Should().Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));
#endif
        };

        // Assert
#if SH
        act.ShouldThrow<ShouldAssertException>();
#else
        act.Should().Throw<XunitException>();
#endif
    }
}
