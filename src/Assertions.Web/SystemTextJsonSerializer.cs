namespace Assertions.Web;

/// <summary>
/// System.Text.Json based serializer.
/// </summary>
public class SystemTextJsonSerializer : ISerializer
{
    private readonly JsonSerializerOptions? _options;

    /// <summary>
    /// Initializes a new instance that reads the global <see cref="SystemTextJsonSerializerConfig.Options"/> on every call.
    /// </summary>
    public SystemTextJsonSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance that deserializes with the given <paramref name="options"/>, ignoring the global ones.
    /// </summary>
    /// <remarks>
    /// The assertions rely on the defaults of <see cref="SystemTextJsonSerializerConfig.Options"/>: case-insensitive property names,
    /// enums and numbers read from strings, trailing commas, and an empty string read as <c>null</c> for nullable value types.
    /// Options without them can make an assertion behave unexpectedly, for example leave a property <c>null</c> instead of failing;
    /// to keep them, start from a copy: <c>new JsonSerializerOptions(SystemTextJsonSerializerConfig.Options)</c>.
    /// </remarks>
    /// <param name="options">The options used to deserialize a JSON into a C# object.</param>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
    public SystemTextJsonSerializer(JsonSerializerOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options), "Cannot use <null> serializer options.");
    }

    /// <inheritdoc/>
    public async Task<object?> Deserialize(Stream content, Type modelType)
    {
        try
        {
            var result = await JsonSerializer.DeserializeAsync(content, modelType, _options ?? SystemTextJsonSerializerConfig.Options);
            return result;
        }
        catch (JsonException ex)
        {
            throw new DeserializationException($"Exception while deserializing the model with {nameof(SystemTextJsonSerializer)}", ex);
        }
    }
}
