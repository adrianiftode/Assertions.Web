#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class HeadersAssertionsSpecs
{
    #region HaveHeader
    [Fact]
    public void When_asserting_response_with_header_to_have_header_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_and_no_header_value_to_have_header_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", (string?)null }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_no_headers_to_have_header_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeader("custom-header", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers", "should have header", "\"custom-header\"", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header*custom-header*but no such header was found*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_without_header_to_have_header_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "other-header", "other-header-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeader("custom-header", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers", "should have header", "\"custom-header\"", "[\"other-header\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header*custom-header*but no such header was found*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_against_null_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeader(null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify having a header against a <null> header.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*header*<null>*");
#endif
    }

    #endregion

    #region NotHaveHeader
    [Fact]
    public void When_asserting_response_with_headers_to_to_not_have_a_header_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "a-header", "with-a-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHeader("other-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHeader("other-header");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_no_headers_to_not_to_have_header_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHeader("custom-header");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_a_header_to_not_have_that_header_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "that-header", "with-a-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHeader("that-header", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers", "should not have header", "\"that-header\"", "[\"with-a-value\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHeader("that-header", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to not contain the HTTP header*that-header*but the header was found*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_not_have_header_against_null_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHeader(null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify not having a header against a <null> header.");
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHeader(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*not having*header*<null>*");
#endif
    }

    #endregion

    #region BeEmpty
    [Fact]
    public void When_asserting_response_with_header_and_no_header_value_to_have_header_and_be_empty_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", (string?)null }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveEmptyHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeEmpty();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_and_a_header_value_to_have_that_header_and_be_empty_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "some-non-empty-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveEmptyHeader("custom-header", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should be empty but had", "1", "item and was", "[\"some-non-empty-value\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeEmpty("we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header *custom-header* with no header values*some-non-empty-value*reason*");
#endif
    }
    #endregion

    #region NotBeEmpty
    [Fact]
    public void When_asserting_response_with_header_and_header_value_to_have_header_and_not_be_empty_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "some-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveNonEmptyHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.NotBeEmpty();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_and_header_values_to_have_header_and_not_be_empty_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "some-value" },
                { "custom-header", "another-value" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveNonEmptyHeader("custom-header");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.NotBeEmpty();

        // Assert
        act.Should().NotThrow();
#endif
    }


    [Theory]
    [InlineData("")]
    [InlineData((string?)null)]
    public void When_asserting_response_with_header_and_no_header_value_to_have_that_header_and_not_be_empty_value_it_should_throw_with_descriptive_message(string? headerValue)
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            { 
                { "custom-header", headerValue }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveNonEmptyHeader("custom-header", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should not be empty but was", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.NotBeEmpty("we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header *custom-header* with any header values, but found the header and it has no values in the actual response*reason*");
#endif
    }
    #endregion

    #region Match
    [Fact]
    public void When_asserting_response_with_header_to_have_header_with_a_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderMatching("custom-header", "value*");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.Match("value*");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_without_a_value_to_have_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderMatching("custom-header", "other-than-value1", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should have header matching", "\"other-than-value1\"", "[\"value1\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.Match("other-than-value1", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header*custom-header* having a value matching *other-than-value1*, but there was no match*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_and_be_value_against_null_expected_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderMatching("custom-header", null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify an HTTP header to be a value against a <null> value.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.Match(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*<null>*Use And.BeEmpty to test if the HTTP header has no values.*");
#endif
    }
    #endregion

    #region BeValue
    [Fact]
    public void When_asserting_response_with_header_with_value_to_have_the_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", "value1");

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue("value1");

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_with_multiple_values_to_have_the_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" },
                { "custom-header", "value2" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", "value1", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should have header with value", "\"value1\"", "[\"value1\", \"value2\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue("value1", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the*custom-header*HTTP header*value to be equivalent to*value1*but found the header and has more or no values*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_with_multiple_values_indicated_by_comma_separation_to_have_the_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1,value2" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", "value1", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should have header with value", "\"value1\"", "[\"value1,value2\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue("value1", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the*custom-header*HTTP header*equivalent*value1*value2*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_without_some_value_to_be_different_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", "other-than-value1", "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should have header with value", "\"other-than-value1\"", "[\"value1\"]", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue("other-than-value1", "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*custom-header*HTTP*header*value*equivalent*but*value1*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_and_be_value_against_null_expected_values_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify an HTTP header to be a value against a <null> or empty value.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*<null>*Use And.BeEmpty to test if the HTTP header has no value.*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_and_be_values_against_empty_expected_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValue("custom-header", "");

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify an HTTP header to be a value against a <null> or empty value.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValue("");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*empty*Use And.BeEmpty to test if the HTTP header has no value.*");
#endif
    }
    #endregion

    #region BeValues
    [Fact]
    public void When_asserting_response_with_header_with_values_to_have_those_values_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" },
                { "custom-header", "value2" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValues("custom-header", new[] { "value1", "value2" });

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValues(new[] { "value1", "value2" });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_header_without_some_value_to_be_different_values_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValues("custom-header", new[] {
                "other-than-value1",
                "another-other-than-value1" }, "we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Headers[\"custom-header\"]", "should be", "[\"other-than-value1\", \"another-other-than-value1\"]", "but was (case sensitive comparison)", "[\"value1\"]", "difference", "Additional Info:", "we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValues(new[] {
                "other-than-value1",
                "another-other-than-value1" }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the HTTP header*custom-header*having values*other-than-value1*another-other-than-value1*reason*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_and_be_values_against_null_expected_values_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValues("custom-header", null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify an HTTP header to be a collection of expected values against a <null> collection.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValues(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*<null>*Use And.BeEmpty to test if the HTTP header has no values.*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_have_header_and_be_values_against_empty_expected_values_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Headers =
            {
                { "custom-header", "value1" }
            }
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHeaderWithValues("custom-header", Enumerable.Empty<string>());

        // Assert
        act.ShouldThrow<ArgumentException>()
            .Message.ShouldStartWith("Cannot verify an HTTP header to be a collection of expected values against an empty collection.");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHeader("custom-header").And.BeValues(Enumerable.Empty<string>());

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*empty*Use And.BeEmpty to test if the HTTP header has no values.*");
#endif
    }

    [Fact]
    public void When_asserting_an_allow_header_with_two_values_to_have_them_in_any_order_it_should_succeed()
    {
        // Arrange. .NET exposes these as two separate values, not one comma-separated string.
        using var subject = AllowResponse("POST", "GET");

        // Act
#if SH
        Action act = () => subject.ShouldHaveHeaderWithValues("Allow", new[] { "GET", "POST" });

        // Assert
        act.ShouldNotThrow();
#else
        Action act = () => subject.Should().HaveHeader("Allow").And.BeValues(new[] { "GET", "POST" });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_a_header_is_exposed_as_one_comma_separated_value_then_no_splitting_should_occur()
    {
        // Arrange
        using var subject = new HttpResponseMessage();
        subject.Headers.TryAddWithoutValidation("X-Raw-Allow", "GET, POST");

        // Act
#if SH
        Action singleValueAct = () => subject.ShouldHaveHeaderWithValue("X-Raw-Allow", "GET, POST");
        Action multipleValuesAct = () => subject.ShouldHaveHeaderWithValues("X-Raw-Allow", new[] { "GET", "POST" });

        // Assert
        singleValueAct.ShouldNotThrow();
        multipleValuesAct.ShouldFailWith("subject.Headers[\"X-Raw-Allow\"]", "should be", "[\"GET\", \"POST\"]", "[\"GET, POST\"]");
#else
        Action singleValueAct = () => subject.Should().HaveHeader("X-Raw-Allow").And.BeValue("GET, POST");
        Action multipleValuesAct = () => subject.Should().HaveHeader("X-Raw-Allow").And.BeValues(new[] { "GET", "POST" });

        // Assert
        singleValueAct.Should().NotThrow();
        multipleValuesAct.Should().Throw<XunitException>()
            .WithMessage("*X-Raw-Allow*having values*GET*POST*differences*");
#endif
    }

#if !SH
    [Fact]
    public void When_a_location_group_has_no_location_header_then_be_value_should_fail_with_an_assertion_not_a_null_reference()
    {
        // Arrange. Be201Created returns LocationAssertions without asserting the header is present.
        using var subject = new HttpResponseMessage(HttpStatusCode.Created);

        // Act
        Action act = () => subject.Should().Be201Created().And.BeValue("http://localhost/api/comments/1");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*Location*HTTP header*more or no values*");
    }
#endif
    #endregion

    private static HttpResponseMessage AllowResponse(params string[] values)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        };
        response.Content.Headers.TryAddWithoutValidation("Allow", values);
        return response;
    }
}