using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#if SH
namespace Shouldly.Web.Serializers.NewtonsoftJson.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Serializers.NewtonsoftJson.Tests;
#else
namespace FluentAssertions.Web.Serializers.NewtonsoftJson.Tests;
#endif

public class NewtonsoftScopedSerializerTests
{
    private sealed class Person
    {
        public string? FirstName { get; set; }
    }

    private static readonly JsonSerializerSettings SnakeCaseSettings = new()
    {
        ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
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
            using (AssertionsWebConfig.UseSerializer(new NewtonsoftJsonSerializer(SnakeCaseSettings)))
            {
#if SH
                subject.ShouldSatisfy<Person>(model => Assert.Equal("Ada", model.FirstName));
#else
                subject.Should().Satisfy<Person>(model => Assert.Equal("Ada", model.FirstName));
#endif
            }
        };

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void When_asserting_response_content_to_satisfy_model_without_a_serializer_scope_it_should_use_the_global_serializer()
    {
        // Arrange
        using var subject = CreateResponse();

        // Act
        Action act = () =>
        {
#if SH
            subject.ShouldSatisfy<Person>(model => Assert.Equal("Ada", model.FirstName));
#else
            subject.Should().Satisfy<Person>(model => Assert.Equal("Ada", model.FirstName));
#endif
        };

        // Assert
        Assert.NotNull(Record.Exception(act));
    }
}
