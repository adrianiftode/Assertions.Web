#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class HttpResponseContentAssertionsSpecs
{
    #region BeEmpty

    [Fact]
    public void When_asserting_response_with_no_content_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeEmpty();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () => subject.Should().BeEmpty();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_empty_content_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage()
        {
            Content = new StringContent("")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeEmpty();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () => subject.Should().BeEmpty();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_content_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
                                                     {
                                                       "comment": "Hey",
                                                       "author": "John"
                                                     }
                                                     """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeEmpty("because we want to test the reason");

        // Assert
        act.ShouldFailContaining("subject.Content", "should be null or empty", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () => subject.Should().BeEmpty("we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have no content*");
#endif
    }
    #endregion

    #region BeAs
    [Fact]
    public void When_asserting_response_with_content_to_be_as_model_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey",
              "author": "John"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Hey",
                Author = "John"
            });

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Hey",
                Author = "John"
            });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_content_with_more_JSON_properties_than_a_model_to_be_as_model_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "author": "John",
              "comment": "Hey"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Hey",
            });

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Hey",
            });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_content_with_differences_to_be_as_model_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey",
              "author": "John"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Not Hey",
                Author = "John"
            }, "because we want to test the reason");

        // Assert
        act.ShouldFailContaining("Comparing object equivalence", "Comment", "\"Not Hey\"", "but was", "\"Hey\"", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Not Hey",
                Author = "John"
            }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
#if AAV
            .WithMessage("*expected property subject.Comment*Not Hey*reason*");
#else
            .WithMessage("*Not Hey*with a length*reason*");
#endif
#endif
    }

    public static IEnumerable<object[]> Data =>
       new List<object[]>
       {
            new object[] { 1 },
            new object[] { "" },
            new object[] { new DateTime(2019, 10, 11) }
       };
    [Theory]
    [MemberData(nameof(Data))]
    public void When_asserting_response_with_content_as_primitives_types_to_be_as_the_primitive_value_it_should_succeed(object expectedModel)
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(expectedModel.ToJson(), Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(expectedModel);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(expectedModel);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_content_as_int_to_be_as_the_primitive_value_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(1.ToJson(), Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(1);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(1);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_fewer_JSON_properties_than_the_model_to_be_as_model_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Hey",
                Author = "John"
            }, "because we want to test the reason");

        // Assert
        act.ShouldFailContaining("Comparing object equivalence", "Author", "\"John\"", "but was", "null", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Hey",
                Author = "John"
            }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("""*Author to be "John"*reason*""");
#endif
    }

    [Fact]
    public void When_asserting_response_with_not_a_proper_JSON_to_be_as_model_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent("some text:that doesn't look like a json {", Encoding.UTF8, "text/plain")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Hey"
            }, "because we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Content", "should be as", "but was", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Hey"
            }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have a content equivalent to a model of type*, but the JSON representation could not be parsed*");
#endif
    }

    [Fact]
    public void When_asserting_response_with_not_a_proper_JSON_to_be_as_model_it_should_throw_with_descriptive_message_which_includes_the_parsing_error_details()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json,strict*/ """{ "price" : 0.0}""", Encoding.UTF8, "text/plain")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                price = 0
            }, "because we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Content", "should be as", "but was", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                price = 0
            }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("""*to have a content equivalent to a model of type*, but the JSON representation could not be parsed, as the operation failed with the following message: "Exception while deserializing the model with SystemTextJsonSerializer: The JSON value could not be converted to * Path: $.price | LineNumber: 0 | BytePositionInLine: 15.*""");
#endif
    }

    [Fact]
    public void When_asserting_response_with_a_syntactically_correct_JSON_array_to_be_as_a_single_object_model_it_should_throw_with_descriptive_message_which_includes_the_serialization_error_details_but_keep_the_original_content()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json,strict*/ """[{ "price" : 0.0}, { "price" : 1.0}]""", Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                price = 0m
            }, "because we want to test the reason");

        // Assert
        act.ShouldFailWith("subject.Content", "should be as", "but was", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                price = 0m
            }, "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("""
            *to have a content equivalent to a model of type*, but the JSON representation could not be parsed, as the operation failed with the following message: "Exception while deserializing the model with SystemTextJsonSerializer: The JSON value could not be converted to * Path: $ | LineNumber: 0 | BytePositionInLine: 1.*
            [
              {
                "price": 0.0
              },
              {
                "price": 1.0
              }
            ]*
            """);
#endif
    }

    [Fact]
    public void When_asserting_with_equivalency_assertion_options_to_be_as_model_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json,strict*/ """
            {
              "author": "John",
              "comment": "Hey",
              "version": "version 1"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                author = "John",
                comment = "Hey",
                version = "version 2"
            }, new EquivalencyOptions { MembersToIgnore = { "version" } });

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                author = "John",
                comment = "Hey",
                version = "version 2"
            }, options => options.Excluding(model => model.version));

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_equivalency_assertion_options_and_with_differences_to_be_as_model_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey",
              "author": "John"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new
            {
                Comment = "Not Hey",
                Author = "John"
            }, new EquivalencyOptions { MembersToIgnore = { "Author" } }, "because we want to test the reason");

        // Assert
        act.ShouldFailContaining("Comparing object equivalence", "Comment", "\"Not Hey\"", "but was", "\"Hey\"", "Additional Info:", "because we want to test the reason", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs(new
            {
                Comment = "Not Hey",
                Author = "John"
            }, options => options.Excluding(model => model.Author), "we want to test the {0}", "reason");

        // Assert
        act.Should().Throw<XunitException>()
#if AAV
            .WithMessage("*expected property subject.Comment*Not Hey*reason*");
#else
            .WithMessage("*Not Hey*with a length*reason*");
#endif
#endif
    }

    [Fact]
    public void When_asserting_response_to_be_as_model_with_null_equivalency_assertion_options_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs(new { }, (EquivalencyOptions)null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Value cannot be null");
#else
        // Act
        Action act = () =>
#if FAV8
            subject.Should().BeAs(new { }, options: (Func<EquivalencyOptions<object>, EquivalencyOptions<object>>)(null!));
#else
            subject.Should().BeAs(new { }, options: (Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>)(null!));
#endif

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null*options*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_be_as_against_null_value_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs((object?)null);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify having a content equivalent to a model against a <null> model.");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs((object?)null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*content*<null>*");
#endif
    }

    [Fact]
    public void When_asserting_null_response_to_be_as_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBeAs((object?)null, "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().BeAs((object?)null, "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
#endregion

    #region MatchInContent
    [Theory]
    [InlineData("*comment*author*")]
    [InlineData("*co?ment*a?thor*")]
    public void When_asserting_response_with_keywords_in_content_to_match_in_content_it_should_succeed(string wildcardText)
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey",
              "author": "John"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldMatchInContent(wildcardText);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().MatchInContent(wildcardText);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_without_keywords_in_content_to_match_in_content_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage
        {
            Content = new StringContent(/*lang=json*/"""
            {
              "comment": "Hey",
              "author": "John"
            }
            """, Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action act = () =>
            subject.ShouldMatchInContent("*notes*", "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.Content", "should match in content", "\"*notes*\"", "but was", "Additional Info:", "because we want to test the failure message", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().MatchInContent("*notes*", "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*notes*message*");
#endif
    }

    [Fact]
    public void When_asserting_response_with_no_string_content_to_match_in_content_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldMatchInContent("*notes*", "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.Content", "should match in content", "\"*notes*\"", "but was", "\"\"", "Additional Info:", "because we want to test the failure message", "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().MatchInContent("*notes*", "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*the wildcard pattern*notes*content was <null>*message*");
#endif
    }

    [Fact]
    public void When_asserting_response_to_match_in_content_against_null_wildcard_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage();

#if SH
        // Act
        Action act = () =>
            subject.ShouldMatchInContent(null!);

        // Assert
        act.ShouldThrow<ArgumentNullException>()
            .Message.ShouldStartWith("Cannot verify an HTTP response content match a <null> wildcard pattern.");
#else
        // Act
        Action act = () =>
            subject.Should().MatchInContent(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*<null>*wildcard*");
#endif
    }

    [Fact]
    public void When_asserting_null_response_to_match_in_content_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldMatchInContent("wildcard", "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().MatchInContent("wildcard", "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion
}
