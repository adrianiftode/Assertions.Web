#if !SH
#if AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class SerializerAssertionsSpecs
{
    private class Person
    {
        public string? FirstName { get; set; }
    }

    private const string SnakeCaseJson = /*lang=json,strict*/ """{ "first_name" : "Ada" }""";

    private static readonly JsonSerializerOptions SnakeCaseOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode = HttpStatusCode.OK, string json = SnakeCaseJson)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

    private class FixedSerializer : ISerializer
    {
        public Task<object?> Deserialize(Stream content, Type modelType)
            => Task.FromResult<object?>(new Person { FirstName = "Fixed" });
    }

    #region UsingSerializer

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_without_a_serializer_choice_it_should_use_the_default_options_and_fail()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_using_serializer_options_it_should_succeed()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_response_content_to_be_as_model_using_serializer_options_it_should_succeed()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .BeAs(new Person { FirstName = "Ada" });

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_response_content_to_be_as_model_using_serializer_options_with_different_values_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .BeAs(new Person { FirstName = "Grace" }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>().WithMessage("*FirstName*Grace*Ada*reason*");
    }

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_using_a_custom_serializer_it_should_use_that_serializer()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer(new FixedSerializer())
            .Satisfy<Person>(model => model.FirstName.Should().Be("Fixed"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_chaining_a_status_assertion_after_using_serializer_it_should_keep_using_the_serializer()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .Be200Ok().And.Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_chaining_a_bad_request_assertion_after_using_serializer_it_should_keep_using_the_serializer()
    {
        // Arrange
        using var subject = CreateResponse(HttpStatusCode.BadRequest);

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .Be400BadRequest().And.Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_chaining_a_created_assertion_after_using_serializer_it_should_keep_using_the_serializer()
    {
        // Arrange
        using var subject = CreateResponse(HttpStatusCode.Created);
        subject.Headers.Location = new Uri("http://localhost/people/1");

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .Be201Created().And.Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_chaining_a_header_assertion_after_using_serializer_it_should_keep_using_the_serializer()
    {
        // Arrange
        using var subject = CreateResponse();
        subject.Headers.Add("custom-header", "value");

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .HaveHeader("custom-header").And.Satisfy<Person>(model => model.FirstName.Should().Be("Ada"));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_using_serializer_it_should_not_change_the_global_configuration()
    {
        // Arrange
        using var subject = CreateResponse();
        var globalSerializer = AssertionsWebConfig.Serializer;
        var globalConverters = SystemTextJsonSerializerConfig.Options.Converters.Count;

        // Act
        subject.Should().UsingSerializer(SnakeCaseOptions).BeAs(new Person { FirstName = "Ada" });
        Action act = () => subject.Should().BeAs(new Person { FirstName = "Ada" });

        // Assert
        AssertionsWebConfig.Serializer.Should().BeSameAs(globalSerializer);
        SystemTextJsonSerializerConfig.Options.Converters.Count.Should().Be(globalConverters);
        SystemTextJsonSerializerConfig.Options.PropertyNamingPolicy.Should().BeNull();
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void When_deserialization_fails_using_serializer_options_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = CreateResponse(json: /*lang=json,strict*/ """{ "first_name" : [1] }""");

        // Act
        Action act = () => subject.Should().UsingSerializer(SnakeCaseOptions)
            .BeAs(new Person { FirstName = "Ada" }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*but the JSON representation could not be parsed*Exception while deserializing the model with SystemTextJsonSerializer*reason*");
    }

    [Fact]
    public void When_using_a_null_serializer_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer((ISerializer)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*<null> serializer*");
    }

    [Fact]
    public void When_using_null_serializer_options_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () => subject.Should().UsingSerializer((JsonSerializerOptions)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*<null> serializer options*");
    }

    #endregion
}
#endif
