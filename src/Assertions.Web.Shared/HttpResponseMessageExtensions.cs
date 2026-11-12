using Assertions.Web;

namespace Assertions.Web.Internal;

internal static class HttpResponseMessageExtensions
{
    public static IEnumerable<string> GetHeaderValues(this HttpResponseMessage response, string header)
    {
        var headers = response.GetHeaders();
        return headers
            .FirstOrDefault(c => string.Equals(c.Key, header, StringComparison.OrdinalIgnoreCase))
            .Value
            .Where(c => !string.IsNullOrEmpty(c));
    }

    public static string? GetFirstHeaderValue(this HttpResponseMessage response, string header)
    {
        var values = response.GetHeaderValues(header);

        return values.FirstOrDefault();
    }

    public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetHeaders(this HttpResponseMessage response)
    {
        var responseContentHeaders =
            response.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();
        return response.Headers.Union(responseContentHeaders);
    }

    public static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetHeaders(this HttpRequestMessage request)
    {
        var requestContentHeaders =
            request.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();
        return request.Headers.Union(requestContentHeaders);
    }

    public static async Task<string?> GetStringContent(this HttpResponseMessage response)
        => response.Content != null ? await response.Content.ReadAsStringAsync() : null;

    public static async Task<JsonDocument> GetJsonDocument(this HttpResponseMessage response)
    {
        var content = await response.GetStringContent();

        return JsonDocument.Parse(content!);
    }

    /// <summary>
    /// Synchronously reads the response content as a string, running the read outside
    /// of the current synchronization context to avoid deadlocks in test frameworks.
    /// </summary>
    public static string? ReadContentAsString(this HttpResponseMessage response)
    {
        Func<Task<string?>> content = () => response.GetStringContent();
        return content.ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Tries to deserialize the response content into an instance of <paramref name="modelType"/>.
    /// A JSON object cannot be deserialized into a tuple type; such an attempt fails with an error
    /// explaining that tuple element names exist only at compile time.
    /// </summary>
    /// <param name="response">The response whose content is read.</param>
    /// <param name="modelType">The type to deserialize the content into.</param>
    /// <returns>
    /// <c>(true, null, model)</c> when the content could be read,
    /// or <c>(false, errorMessage, null)</c> when deserialization failed.
    /// </returns>
    public static (bool success, string? errorMessage, object? model) TryReadModel(this HttpResponseMessage response, Type modelType)
    {
        var serializer = AssertionsWebConfig.Serializer;

        Func<Task<object?>> readModel = () => response.Content.ReadAsAsync(modelType, serializer);
        try
        {
            var model = readModel.ExecuteInDefaultSynchronizationContext().GetAwaiter().GetResult();
            return (true, null, model);
        }
        catch (Exception ex) when (ex is DeserializationException or NotSupportedException)
        {
            var message = ex.Message;
            if (ex.InnerException != null)
            {
                message += $": {ex.InnerException.Message}";
            }

            return (false, message, null);
        }
    }
}