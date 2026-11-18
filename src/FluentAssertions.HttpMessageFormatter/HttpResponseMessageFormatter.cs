
using HttpMessageFormatter;

#if AAV
using AwesomeAssertions.Formatting;
#else
using FluentAssertions.Formatting;
#endif

#if AAV
namespace AwesomeAssertions.Formatting;
#else
namespace FluentAssertions.Formatting;
#endif

/// <summary>
/// FluentAssertions formatter for HTTP response messages using HttpMessageFormatter.
/// </summary>
public class HttpResponseMessageFormatter : IValueFormatter
{
    /// <summary>
    /// Determines whether this formatter can render <paramref name="value"/> as an HTTP response message.
    /// </summary>
    /// <param name="value">The object being formatted by the assertion framework.</param>
    /// <returns>
    /// <c>true</c> when <paramref name="value"/> is an <see cref="HttpResponseMessage"/>; otherwise <c>false</c>.
    /// </returns>
    public bool CanHandle(object value) => value is HttpResponseMessage;

    /// <summary>
    /// Appends the formatted HTTP request and response conversation to <paramref name="formattedGraph"/>.
    /// </summary>
    /// <param name="value">The <see cref="HttpResponseMessage"/> to format.</param>
    /// <param name="formattedGraph">The graph receiving the formatted fragments.</param>
    /// <param name="context">The formatting context supplied by the assertion framework.</param>
    /// <param name="formatChild">The callback used by the assertion framework for nested values.</param>
    public void Format(object value,
        FormattedObjectGraph formattedGraph,
        FormattingContext context,
        FormatChild formatChild)
    {
        var response = (HttpResponseMessage)value;
        var options = AssertionsWebConfig.ResponseFormatterOptions;
        var formatted = response.Format(options);

        formattedGraph.AddFragment(formatted);
    }
}