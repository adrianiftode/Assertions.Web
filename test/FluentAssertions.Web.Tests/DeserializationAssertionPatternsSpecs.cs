#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class DeserializationAssertionPatternsSpecs
{
    private sealed class ExpectedProblemDetails
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
        public string? TraceId { get; set; }
    }

    private sealed class ProblemDetailsWithExtensions
    {
        public int? Status { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        [System.Text.Json.Serialization.JsonExtensionData]
        public Dictionary<string, JsonElement>? Extensions { get; set; }
    }

    private sealed class CartResponse
    {
        public string? Status { get; set; }
        public decimal Total { get; set; }
        public string? DiscountCode { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }

    private sealed class CartItem
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    [Fact]
    public void When_be_as_asserts_a_problem_details_model_then_the_complete_expected_graph_is_compared()
    {
        // Arrange
        using var response = ProblemDetailsResponse();
        var expected = new ExpectedProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more validation errors occurred.",
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                ["Author"] = new[] { "The Author field is required." }
            },
            TraceId = "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
        };

#if SH
        // Act
        Action act = () => response.ShouldBeAs(expected);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () => response.Should().BeAs(expected);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_satisfy_asserts_problem_details_then_selected_fields_and_extension_data_are_available()
    {
        // Arrange
        using var response = ProblemDetailsResponse();

#if SH
        // Act
        Action act = () => response.ShouldSatisfy<ProblemDetailsWithExtensions>(problem =>
        {
            problem.Status.ShouldBe(400);
            var errors = problem.Errors;
            errors.ShouldNotBeNull();
            errors.ShouldContainKey("Author");
            errors["Author"][0].ShouldContain("required");

            var extensions = problem.Extensions;
            extensions.ShouldNotBeNull();
            extensions.ShouldContainKey("traceId");
            var traceId = extensions!["traceId"].GetString();
            traceId.ShouldNotBeNullOrEmpty();
            traceId.ShouldStartWith("00-");
        });

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () => response.Should().Satisfy<ProblemDetailsWithExtensions>(problem =>
        {
            problem.Status.Should().Be(400);
            var errors = problem.Errors;
            errors.Should().NotBeNull();
            errors!.Should().ContainKey("Author");
            errors!["Author"].Should().ContainSingle()
                .Which.Should().Match("*required*");

            var extensions = problem.Extensions;
            extensions.Should().NotBeNull();
            extensions!.Should().ContainKey("traceId");
            var traceId = extensions!["traceId"].GetString();
            traceId.Should().NotBeNullOrWhiteSpace();
            traceId.Should().StartWith("00-");
        });

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_the_body_cannot_be_deserialized_then_be_as_and_satisfy_both_report_a_parsing_failure()
    {
        // Arrange
        using var response = new HttpResponseMessage
        {
            Content = new StringContent("// not JSON", Encoding.UTF8, "application/json")
        };

#if SH
        // Act
        Action beAsAct = () => response.ShouldBeAs(new ExpectedProblemDetails());
        Action satisfyAct = () => response.ShouldSatisfy<ProblemDetailsWithExtensions>(_ => { });

        // Assert
        beAsAct.ShouldFailWith("response.Content", "should be as", "The HTTP response was:");
        satisfyAct.ShouldFailWith("response.Content", "should be as", "The HTTP response was:");
#else
        // Act
        Action beAsAct = () => response.Should().BeAs(new ExpectedProblemDetails());
        Action satisfyAct = () => response.Should().Satisfy<ProblemDetailsWithExtensions>(_ => { });

        // Assert
        beAsAct.Should().Throw<XunitException>()
            .WithMessage("*JSON representation could not be parsed*");
        satisfyAct.Should().Throw<XunitException>()
            .WithMessage("*JSON representation could not be parsed*");
#endif
    }

    [Fact]
    public void When_deserialization_succeeds_but_expectations_fail_then_the_failure_categories_differ()
    {
        // Arrange
        using var response = ProblemDetailsResponse();
        var wrongExpected = new ExpectedProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more validation errors occurred.",
            Status = 500,
            Errors = new Dictionary<string, string[]>
            {
                ["Author"] = new[] { "The Author field is required." }
            },
            TraceId = "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
        };

#if SH
        // Act
        Action beAsAct = () => response.ShouldBeAs(wrongExpected);
        Action satisfyAct = () =>
            response.ShouldSatisfy<ProblemDetailsWithExtensions>(problem => problem.Status.ShouldBe(500));

        // Assert
        beAsAct.ShouldFailContaining("Comparing object equivalence", "response.Content", "Status", "500", "400",
            "The HTTP response was:");
        satisfyAct.ShouldFailWith("response.Content", "should satisfy all the conditions specified, but does not.",
            "Error 1", "The HTTP response was:");
#else
        // Act
        Action beAsAct = () => response.Should().BeAs(wrongExpected);
        Action satisfyAct = () =>
            response.Should().Satisfy<ProblemDetailsWithExtensions>(problem => problem.Status.Should().Be(500));

        // Assert
        beAsAct.Should().Throw<XunitException>()
            .WithMessage("*content equivalent to a model*has differences*");
        satisfyAct.Should().Throw<XunitException>()
            .WithMessage("*satisfy one or more model assertions*Status*");
#endif
    }

    [Fact]
    public void When_satisfy_runs_real_test_logic_then_its_failure_keeps_the_http_conversation()
    {
        // Arrange
        using var response = CartHttpResponse();

#if SH
        // Act
        Action act = () => response.ShouldSatisfy<CartResponse>(cart =>
        {
            var calculatedTotal = cart.Items.Sum(item => item.Price * item.Quantity);
            if (cart.Total != calculatedTotal)
            {
                throw new InvalidOperationException($"The total was {cart.Total}, but the items add up to {calculatedTotal}.");
            }

            if (string.Equals(cart.Status, "Discounted", StringComparison.OrdinalIgnoreCase))
            {
                var discountCode = cart.DiscountCode;
                if (string.IsNullOrWhiteSpace(discountCode) || discountCode.Length != 8)
                {
                    throw new InvalidOperationException("A discounted cart must have an eight-character discount code.");
                }
            }
        });

        // Assert
        act.ShouldFailWith("response.Content",
            "should satisfy all the conditions specified, but does not.",
            "Error 1",
            "A discounted cart must have an eight-character discount code.",
            "The HTTP response was:",
            "SHORT");
#else
        // Act
        Action act = () => response.Should().Satisfy<CartResponse>(cart =>
        {
            var calculatedTotal = cart.Items.Sum(item => item.Price * item.Quantity);
            if (cart.Total != calculatedTotal)
            {
                throw new InvalidOperationException($"The total was {cart.Total}, but the items add up to {calculatedTotal}.");
            }

            if (string.Equals(cart.Status, "Discounted", StringComparison.OrdinalIgnoreCase))
            {
                var discountCode = cart.DiscountCode;
                if (string.IsNullOrWhiteSpace(discountCode) || discountCode.Length != 8)
                {
                    throw new InvalidOperationException("A discounted cart must have an eight-character discount code.");
                }
            }
        });

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*satisfy one or more model assertions*A discounted cart must have an eight-character discount code.*The HTTP response was:*SHORT*");
#endif
    }

    private static HttpResponseMessage CartHttpResponse() =>
        new(HttpStatusCode.OK)
        {
            Content = new StringContent(/*lang=json,strict*/ """
            {
              "status": "Discounted",
              "total": 18.00,
              "discountCode": "SHORT",
              "items": [
                { "price": 9.00, "quantity": 2 }
              ]
            }
            """, Encoding.UTF8, "application/json")
        };

    private static HttpResponseMessage ProblemDetailsResponse() =>
        new(HttpStatusCode.BadRequest)
        {
            Content = new StringContent(/*lang=json,strict*/ """
            {
              "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
              "title": "One or more validation errors occurred.",
              "status": 400,
              "errors": {
                "Author": [ "The Author field is required." ]
              },
              "traceId": "00-aae99e6ddbee1a15d42f336faae193e3-1972a9b2677febe5-00"
            }
            """, Encoding.UTF8, "application/problem+json")
        };
}
