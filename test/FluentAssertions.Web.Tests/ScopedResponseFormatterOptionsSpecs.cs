#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class ScopedResponseFormatterOptionsSpecs
{
    [Fact]
    public void When_an_assertion_fails_inside_a_response_formatter_options_scope_the_failure_message_should_be_formatted_with_the_scoped_options()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);
        const string scopedMarker = "SCOPED-RESPONSE-FORMATTER-OPTIONS-MARKER";

        // Act + Assert
        using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpMessageFormatter.HttpResponseFormatterOptions
               {
                   PostFormat = formatted => $"{formatted}{Environment.NewLine}{scopedMarker}"
               }))
        {
#if SH
            Action act = () => subject.ShouldBe1XXInformational();

            act.ShouldThrow<Shouldly.ShouldAssertException>().Message.ShouldContain(scopedMarker);
#else
            Action act = () => subject.Should().Be1XXInformational();

            act.Should().Throw<XunitException>().Which.Message.Should().Contain(scopedMarker);
#endif
        }
    }

    [Fact]
    public void When_an_assertion_fails_outside_a_response_formatter_options_scope_the_failure_message_should_not_be_affected_by_it()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);
        const string scopedMarker = "SCOPED-RESPONSE-FORMATTER-OPTIONS-MARKER";

        using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpMessageFormatter.HttpResponseFormatterOptions
               {
                   PostFormat = formatted => $"{formatted}{Environment.NewLine}{scopedMarker}"
               }))
        {
            // The scope ends here; the assertion below must use the global options.
        }

        // Act
        Action act = () =>
#if SH
            subject.ShouldBe1XXInformational();
#else
            subject.Should().Be1XXInformational();
#endif

        // Assert
#if SH
        act.ShouldThrow<Shouldly.ShouldAssertException>().Message.ShouldNotContain(scopedMarker);
#else
        act.Should().Throw<XunitException>().Which.Message.Should().NotContain(scopedMarker);
#endif
    }
}
