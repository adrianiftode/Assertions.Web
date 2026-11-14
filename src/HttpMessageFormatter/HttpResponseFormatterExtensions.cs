using HttpMessageFormatter.Internal;
using HttpMessageFormatter.Internal.ContentProcessors;

namespace HttpMessageFormatter;

/// <summary>
/// Formats HTTP response messages for inspection and debugging.
/// Provides rich, readable output of HTTP responses including headers, content, status codes, and associated requests.
/// </summary>
public static class HttpResponseFormatterExtensions
{
    /// <summary>
    /// Formats an HTTP response message into a readable string representation.
    /// </summary>
    /// <param name="response">The HTTP response message to format.</param>
    /// <returns>A formatted string representation of the HTTP response.</returns>
    public static string Format(this HttpResponseMessage response)
        => Format(response, null);

    /// <summary>
    /// Formats an HTTP response message into a readable string representation using the given <paramref name="options"/>.
    /// </summary>
    /// <param name="response">The HTTP response message to format.</param>
    /// <param name="options">The options controlling the formatting, like the maximum number of content characters to print
    /// or the <see cref="HttpResponseFormatterOptions.ResponseFormatter"/> and <see cref="HttpResponseFormatterOptions.PostFormat"/>
    /// delegates customizing the formatted output.
    /// When <see langword="null"/>, the default options are used.</param>
    /// <returns>A formatted string representation of the HTTP response.</returns>
    public static string Format(this HttpResponseMessage response, HttpResponseFormatterOptions? options)
    {
        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine();
        messageBuilder.AppendLine();
        messageBuilder.AppendLine("The HTTP response was:");

        Func<Task> contentResolver = async () => await AppendHttpResponseMessage(messageBuilder, response, options);
        contentResolver.ExecuteInDefaultSynchronizationContext();

        return messageBuilder.ToString();
    }

    private static async Task AppendHttpResponseMessage(StringBuilder messageBuilder, HttpResponseMessage response, HttpResponseFormatterOptions? options)
    {
        var outputStart = messageBuilder.Length;

        if (options?.ResponseFormatter is { } responseFormatter)
        {
            try
            {
                messageBuilder.Append(responseFormatter(response));
            }
            catch (Exception e)
            {
                messageBuilder.AppendLine();
                messageBuilder.AppendLine($"An exception occurred while trying to format the HTTP response: {e}");
            }
        }
        else
        {
            try
            {
                await AppendResponse(messageBuilder, response, options);
            }
            catch (Exception e)
            {
                messageBuilder.AppendLine();
                messageBuilder.AppendLine($"An exception occurred while trying to output the some of the response details: {e}");
            }
            try
            {
                await AppendRequest(messageBuilder, response, options);
            }
            catch (Exception e)
            {
                messageBuilder.AppendLine();
                messageBuilder.AppendLine($"An exception occurred while trying to output some of the request details: {e}");
            }
        }

        ApplyPostFormat(messageBuilder, outputStart, options);
    }

    private static void ApplyPostFormat(StringBuilder messageBuilder, int outputStart, HttpResponseFormatterOptions? options)
    {
        if (options?.PostFormat is not { } postFormat)
        {
            return;
        }

        var formatted = messageBuilder.ToString(outputStart, messageBuilder.Length - outputStart);
        try
        {
            var transformed = postFormat(formatted);
            if (transformed != null)
            {
                messageBuilder.Length = outputStart;
                messageBuilder.Append(transformed);
            }
        }
        catch (Exception e)
        {
            messageBuilder.AppendLine();
            messageBuilder.AppendLine($"An exception occurred while trying to post-format the response details: {e}");
        }
    }

    private static async Task AppendResponse(StringBuilder messageBuilder, HttpResponseMessage response, HttpResponseFormatterOptions? options)
    {
        AppendProtocolAndStatusCode(messageBuilder, response);
        Appender.AppendHeaders(messageBuilder, response.GetHeaders());
        AppendContentLength(messageBuilder, response.Content);
        await AppendResponseContent(messageBuilder, response, options);
    }

    private static async Task AppendRequest(StringBuilder messageBuilder, HttpResponseMessage response, HttpResponseFormatterOptions? options)
    {
        messageBuilder.AppendLine();
        messageBuilder.AppendLine();
        var request = response.RequestMessage;
        if (request == null)
        {
            messageBuilder.AppendLine("The originating HTTP request was <null>.");
            return;
        }
        messageBuilder.AppendLine("The originating HTTP request was:");
        messageBuilder.AppendLine();

        messageBuilder.AppendLine($"{request.Method.ToString().ToUpper()} {request.RequestUri} HTTP {request.Version}");

        Appender.AppendHeaders(messageBuilder, request.GetHeaders());
        AppendContentLength(messageBuilder, request.Content);

        await AppendRequestContent(messageBuilder, request.Content, options);
    }

    private static async Task AppendResponseContent(StringBuilder messageBuilder, HttpResponseMessage response, HttpResponseFormatterOptions? options)
    {
        var content = response.Content;
        if (content == null)
        {
            return;
        }

        var processors = new List<IContentProcessor>();
        processors.Add(new InternalServerErrorProcessor(response, content));
        processors.AddRange(ProcessorsRunner.CommonProcessors(content, options));

        var contentBuilder = await ProcessorsRunner.RunProcessors(processors);
        messageBuilder.AppendLine();
        messageBuilder.Append(contentBuilder);
    }

    internal static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetHeaders(this HttpResponseMessage response)
    {
        var responseContentHeaders =
            response.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();
        return response.Headers.Union(responseContentHeaders);
    }

    internal static IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetHeaders(this HttpRequestMessage request)
    {
        var requestContentHeaders =
            request.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();
        return request.Headers.Union(requestContentHeaders);
    }

    private static async Task AppendRequestContent(StringBuilder messageBuilder, HttpContent content, HttpResponseFormatterOptions? options)
    {
        await Appender.AppendContent(messageBuilder, content, false, options);
    }

    private static void AppendProtocolAndStatusCode(StringBuilder messageBuilder, HttpResponseMessage response)
    {
        messageBuilder.AppendLine();
        messageBuilder.AppendLine($@"HTTP/{response.Version} {(int)response.StatusCode} {response.StatusCode}");
    }

    private static void AppendContentLength(StringBuilder messageBuilder, HttpContent? content)
    {
        if (content != null && content.Headers.TryGetValues("Content-Length", out var values))
        {
            foreach (var value in values)
            {
                messageBuilder.AppendLine($"Content-Length: {value}");
            }
        }
    }
}