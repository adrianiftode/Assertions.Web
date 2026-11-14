using System.Reflection;
using System.Text.Json.Serialization;

namespace Assertions.Web.Internal.Serializers;

//https://stackoverflow.com/a/65025191/782754
internal class NullableConverterFactory : JsonConverterFactory
{
    static readonly byte[] Empty = Array.Empty<byte>();

    public override bool CanConvert(Type typeToConvert) => Nullable.GetUnderlyingType(typeToConvert) != null;

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(
            typeof(NullableConverter<>).MakeGenericType(
                new Type[] { Nullable.GetUnderlyingType(type) }),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: new object[] { options },
            culture: null);

    class NullableConverter<T> : JsonConverter<T?> where T : struct
    {
        // DO NOT CACHE the return of (JsonConverter<T>)options.GetConverter(typeof(T)) as DoubleConverter.Read() and DoubleConverter.Write()
        // DO NOT WORK for nondefault values of JsonSerializerOptions.NumberHandling which was introduced in .NET 5
        public NullableConverter(JsonSerializerOptions options) { }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.ValueTextEquals(Empty))
                    return null;
            }

            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, value != null ? value.Value : default, options);
    }
}
