using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

#if SH
namespace Shouldly.Web.Serializers.NewtonsoftJson.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Serializers.NewtonsoftJson.Tests;
#else
namespace FluentAssertions.Web.Serializers.NewtonsoftJson.Tests;
#endif

public class NewtonsoftJsonSerializerSettingsTests
{
    private sealed class Flags
    {
        public bool Accepted { get; set; }
    }

    private static MemoryStream Json(string json) => new(Encoding.UTF8.GetBytes(json));

    [Fact]
    public async Task Given_settings_Then_they_are_used_instead_of_the_global_ones()
    {
        // Arrange
        var settings = new JsonSerializerSettings { Converters = { new YesNoBooleanJsonConverter() } };
        var serializer = new NewtonsoftJsonSerializer(settings);

        // Act
        var model = await serializer.Deserialize(Json(/*lang=json,strict*/ """{ "accepted": "yes" }"""), typeof(Flags));

        // Assert
        Assert.True(Assert.IsType<Flags>(model).Accepted);
    }

    [Fact]
    public async Task Given_settings_without_the_global_converter_Then_the_global_converter_does_not_apply()
    {
        // Arrange
        var serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings());

        // Act
        Func<Task> act = () => serializer.Deserialize(Json(/*lang=json,strict*/ """{ "accepted": "yes" }"""), typeof(Flags));

        // Assert
        var exception = await Assert.ThrowsAsync<DeserializationException>(act);
        Assert.Contains("NewtonsoftJsonSerializer", exception.Message);
    }

    [Fact]
    public async Task Given_no_settings_Then_the_global_settings_apply()
    {
        // Arrange
        var serializer = new NewtonsoftJsonSerializer();

        // Act
        var model = await serializer.Deserialize(Json(/*lang=json,strict*/ """{ "accepted": "yes" }"""), typeof(Flags));

        // Assert
        Assert.True(Assert.IsType<Flags>(model).Accepted);
    }

    [Fact]
    public void Given_null_settings_Then_it_throws_ArgumentNullException()
    {
        // Act
        Action act = () => new NewtonsoftJsonSerializer(null!);

        // Assert
        var exception = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal("settings", exception.ParamName);
    }
}
