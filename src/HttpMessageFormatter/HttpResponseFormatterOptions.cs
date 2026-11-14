using HttpMessageFormatter.Internal;

namespace HttpMessageFormatter;

/// <summary>
/// Parameter object with the options used when formatting an <see cref="HttpResponseMessage"/>.
/// </summary>
public class HttpResponseFormatterOptions
{
    private int _maximumReadableBytes = ContentFormatterOptions.MaximumReadableBytes;

    /// <summary>
    /// The maximum number of characters read from the content and printed into the formatted output.
    /// When the content is longer, only a part of it is printed together with a warning message.
    /// Defaults to 10 * 128 * 1024 (1,310,720) characters.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The value is less than one.</exception>
    public int MaximumReadableBytes
    {
        get => _maximumReadableBytes;

        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(MaximumReadableBytes)} must be greater than zero.");
            }

            _maximumReadableBytes = value;
        }
    }

    /// <summary>
    /// When configured, replaces the built-in formatting of the entire output: the HTTP response (status, headers
    /// and content) as well as the originating HTTP request.
    /// The delegate receives the complete <see cref="HttpResponseMessage"/>, which also holds the originating
    /// request through <see cref="HttpResponseMessage.RequestMessage"/>, and returns the text to be appended
    /// to the formatted output.
    /// When it is set, the built-in formatting pipeline, including the content processors and the
    /// <see cref="MaximumReadableBytes"/> limit, is bypassed.
    /// If the delegate throws, the exception is reported into the formatted output, similar to any other
    /// formatting error.
    /// </summary>
    public Func<HttpResponseMessage, string>? ResponseFormatter { get; set; }

    /// <summary>
    /// When configured, transforms the final formatted output of the HTTP response and the originating HTTP
    /// request, no matter if it was produced by the built-in formatting or by <see cref="ResponseFormatter"/>.
    /// It can be used, for example, to make escaped newlines readable, to redact sensitive values or
    /// to normalize volatile ones.
    /// If the delegate throws or returns <see langword="null"/>, the untransformed output is kept and the
    /// exception is reported into the formatted output, similar to any other formatting error.
    /// </summary>
    public Func<string, string>? PostFormat { get; set; }
}
