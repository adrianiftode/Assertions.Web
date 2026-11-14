#if SH
using Shouldly;
#elif AAV
using AwesomeAssertions;
#else
using FluentAssertions;
#endif
using Assertions.Web;
using HttpMessageFormatter;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Sample.Api.Tests
{
    public class ResponseFormatterConfigurationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ResponseFormatterConfigurationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostFormat_CanRemoveTheRequestAuthorizationHeaderFromTheFailureMessage()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "my-secret-token");
            using var response = await client.GetAsync("/api/values");

            // Act
            using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpResponseFormatterOptions
            {
                PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
            }))
            {
                // Assert
                var exception = CaptureFailure(() => FailOnStatusCode(response));

                exception.Message.ShouldContainTheText("The originating HTTP request was:");
                exception.Message.ShouldNotContainTheText("my-secret-token");
            }
        }

        [Fact]
        public async Task PostFormat_CanPrintTheResponseAndRequestDetailsOnASingleLine()
        {
            // Arrange
            var client = _factory.CreateClient();
            using var response = await client.GetAsync("/api/values");

            // Act
            using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpResponseFormatterOptions
            {
                PostFormat = formatted => formatted.Replace("\r\n", " ").Replace('\n', ' ')
            }))
            {
                // Assert
                var exception = CaptureFailure(() => FailOnStatusCode(response));

                exception.Message.ShouldContainTheText("The HTTP response was:");
                var afterHeader = exception.Message.Substring(exception.Message.IndexOf("was:", StringComparison.Ordinal) + 4);
                afterHeader = afterHeader.Substring(afterHeader.IndexOf('\n') + 1);
                afterHeader.ShouldNotContainTheText("\n");
            }
        }

        [Fact]
        public async Task ResponseFormatter_CanReplaceTheBuiltInFormattingOfTheWholeOutput()
        {
            // Arrange
            var client = _factory.CreateClient();
            using var response = await client.GetAsync("/api/values");

            // Act
            using (AssertionsWebConfig.UseResponseFormatterOptions(new HttpResponseFormatterOptions
            {
                ResponseFormatter = formatted => $"CUSTOM {formatted.StatusCode} for {formatted.RequestMessage!.RequestUri}"
            }))
            {
                // Assert
                var exception = CaptureFailure(() => FailOnStatusCode(response));

                exception.Message.ShouldContainTheText("CUSTOM OK for http://localhost/api/values");
                exception.Message.ShouldNotContainTheText("HTTP/1.1 200 OK");
            }
        }

        [Fact]
        public async Task PostFormat_CanBeConfiguredGlobally()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "my-other-secret-token");
            using var response = await client.GetAsync("/api/values");

            var global = AssertionsWebConfig.ResponseFormatterOptions;
            try
            {
                AssertionsWebConfig.ResponseFormatterOptions = new HttpResponseFormatterOptions
                {
                    PostFormat = formatted => Regex.Replace(formatted, "^Authorization: .*(\r?\n|$)", string.Empty, RegexOptions.Multiline)
                };

                // Act + Assert
                var exception = CaptureFailure(() => FailOnStatusCode(response));

                exception.Message.ShouldContainTheText("The originating HTTP request was:");
                exception.Message.ShouldNotContainTheText("my-other-secret-token");
            }
            finally
            {
                AssertionsWebConfig.ResponseFormatterOptions = global;
            }
        }

        private static void FailOnStatusCode(HttpResponseMessage response)
        {
#if SH
            response.ShouldBe500InternalServerError();
#else
            response.Should().Be500InternalServerError();
#endif
        }

        private static Exception CaptureFailure(Action failingAssertion)
        {
#if SH
            return Should.Throw<Shouldly.ShouldAssertException>(failingAssertion);
#else
            return failingAssertion.Should().Throw<XunitException>().Which;
#endif
        }
    }

    internal static class ResponseFormatterConfigurationTestExtensions
    {
        public static void ShouldContainTheText(this string actual, string expected)
        {
#if SH
            actual.ShouldContain(expected);
#else
            actual.Should().Contain(expected);
#endif
        }

        public static void ShouldNotContainTheText(this string actual, string notExpected)
        {
#if SH
            actual.ShouldNotContain(notExpected);
#else
            actual.Should().NotContain(notExpected);
#endif
        }
    }
}
