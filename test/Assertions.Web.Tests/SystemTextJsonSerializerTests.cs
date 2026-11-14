namespace Assertions.Web.Tests;

[Collection("Assertions Web Config Tests")]
public class SystemTextJsonSerializerTests
{
    private sealed class Person
    {
        public string? FirstName { get; set; }
    }

    private static MemoryStream Json(string json) => new(System.Text.Encoding.UTF8.GetBytes(json));

    [Fact]
    public async Task Given_options_Then_they_are_used_instead_of_the_global_ones()
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        var serializer = new SystemTextJsonSerializer(options);

        var model = await serializer.Deserialize(Json("""{ "first_name" : "Ada" }"""), typeof(Person));

        model.Should().BeOfType<Person>().Which.FirstName.Should().Be("Ada");
    }

    [Fact]
    public async Task Given_options_Then_the_global_defaults_do_not_apply()
    {
        var serializer = new SystemTextJsonSerializer(new JsonSerializerOptions());

        var model = await serializer.Deserialize(Json("""{ "firstname" : "Ada" }"""), typeof(Person));

        model.Should().BeOfType<Person>().Which.FirstName.Should().BeNull();
    }

    [Fact]
    public async Task Given_no_options_Then_the_global_options_apply()
    {
        var serializer = new SystemTextJsonSerializer();

        var model = await serializer.Deserialize(Json("""{ "firstname" : "Ada" }"""), typeof(Person));

        model.Should().BeOfType<Person>().Which.FirstName.Should().Be("Ada");
    }

    [Fact]
    public void Given_null_options_Then_it_throws_ArgumentNullException()
    {
        Action act = () => new SystemTextJsonSerializer(null!);

        act.Should().Throw<ArgumentNullException>().WithParameterName("options");
    }

    [Fact]
    public async Task Given_invalid_json_Then_it_throws_DeserializationException()
    {
        var serializer = new SystemTextJsonSerializer(new JsonSerializerOptions());

        Func<Task> act = () => serializer.Deserialize(Json("not json"), typeof(Person));

        (await act.Should().ThrowAsync<DeserializationException>())
            .WithMessage("Exception while deserializing the model with SystemTextJsonSerializer");
    }
}
