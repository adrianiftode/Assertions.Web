// ReSharper disable once CheckNamespace
namespace Assertions.Web;

/// <summary>
/// Newtonsoft.Json based serializer
/// </summary>
public class NewtonsoftJsonSerializer : ISerializer
{
    private readonly JsonSerializerSettings? _settings;

    /// <summary>
    /// Initializes a new instance that reads the global <see cref="NewtonsoftJsonSerializerConfig.Options"/> on every call.
    /// </summary>
    public NewtonsoftJsonSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance that deserializes with the given <paramref name="settings"/>, ignoring the global ones.
    /// </summary>
    /// <param name="settings">The settings used to deserialize a JSON into a C# object.</param>
    /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <c>null</c>.</exception>
    public NewtonsoftJsonSerializer(JsonSerializerSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings), "Cannot use <null> serializer settings.");
    }

    /// <inheritdoc/>
    public Task<object?> Deserialize(Stream content, Type modelType)
    {
        try
        {
            var serializer = JsonSerializer.Create(_settings ?? NewtonsoftJsonSerializerConfig.Options);

            using var sr = new StreamReader(content, Encoding.UTF8, true, 1024, leaveOpen: true);
            using var reader = new JsonTextReader(sr);
            var result = serializer.Deserialize(reader, modelType);
            return Task.FromResult((object?)result);
        }

        catch (JsonException ex)
        {
            throw new DeserializationException($"Exception while deserializing the model with {nameof(NewtonsoftJsonSerializer)}", ex);
        }
    }
}
