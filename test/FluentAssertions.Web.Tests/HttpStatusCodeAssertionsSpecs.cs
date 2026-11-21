#if SH
namespace Shouldly.Web.Tests;
#elif AAV
namespace AwesomeAssertions.Web.Tests;
#else
namespace FluentAssertions.Web.Tests;
#endif

public class HttpStatusCodeAssertionsSpecs
{
    #region Be1XXInformational
    [Theory]
    [InlineData(HttpStatusCode.Continue)]
    [InlineData(HttpStatusCode.SwitchingProtocols)]
    public void When_asserting_response_with_an_informational_status_to_Be1XXInformational_it_should_succeed(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe1XXInformational();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be1XXInformational();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public void When_asserting_other_than_an_informational_status_code_response_to_Be1XXInformational_it_should_throw_with_descriptive_message(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe1XXInformational("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "a 1XX informational status code", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be1XXInformational("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have an HTTP status code representing an informational error*message*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_Be1XXInformational_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe1XXInformational("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be1XXInformational("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region Be2XXSuccessful
    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.PartialContent)]
    public void When_asserting_response_with_a_successful_status_to_Be2XXSuccessful_it_should_succeed(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe2XXSuccessful();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be2XXSuccessful();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Theory]
    [InlineData(HttpStatusCode.SwitchingProtocols)]
    [InlineData(HttpStatusCode.PermanentRedirect)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public void When_asserting_other_than_a_successful_status_code_response_to_Be2XXSuccessful_it_should_throw_with_descriptive_message(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe2XXSuccessful("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "a 2XX successful status code", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be2XXSuccessful("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*have a successful HTTP status code, but it was*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_Be2XXSuccessful_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe2XXSuccessful("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be2XXSuccessful("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region Be3XXRedirection
    [Theory]
    [InlineData(HttpStatusCode.Moved)]
    [InlineData(HttpStatusCode.TemporaryRedirect)]
    public void When_asserting_response_with_a_redirection_status_code_to_Be3XXRedirection_it_should_succeed(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe3XXRedirection();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be3XXRedirection();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Theory]
    [InlineData(HttpStatusCode.Continue)]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public void When_asserting_other_than_a_client_redirection_status_code_response_to_Be3XXRedirection_it_should_throw_with_descriptive_message(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe3XXRedirection("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "a 3XX redirection status code (301\u2013399)", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be3XXRedirection("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have an HTTP status code representing a redirection*message*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_Be3XXRedirection_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe3XXRedirection("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be3XXRedirection("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region Be4XXClientError
    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.UpgradeRequired)]
    public void When_asserting_response_with_a_client_error_status_code_to_Be4XXClientError_it_should_succeed(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe4XXClientError();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be4XXClientError();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Theory]
    [InlineData(HttpStatusCode.Continue)]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public void When_asserting_other_than_a_client_error_status_code_response_to_Be4XXClientError_it_should_throw_with_descriptive_message(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe4XXClientError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "a 4XX client error status code", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be4XXClientError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have an HTTP status code representing a client error*message*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_Be4XXClientError_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe4XXClientError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be4XXClientError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region Be5XXServerError
    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.HttpVersionNotSupported)]
    public void When_asserting_response_with_a_server_error_status_code_to_Be5XXServerError_it_should_succeed(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe5XXServerError();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be5XXServerError();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Theory]
    [InlineData(HttpStatusCode.Continue)]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.UpgradeRequired)]
    public void When_asserting_other_than_a_server_error_status_code_response_to_Be5XXServerError_it_should_throw_with_descriptive_message(HttpStatusCode status)
    {
        // Arrange
        using var subject = new HttpResponseMessage(status);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe5XXServerError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "a 5XX server error status code", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be5XXServerError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*to have an HTTP status code representing a server error*message*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_Be5XXServerError_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe5XXServerError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be5XXServerError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region HaveHttpStatusCode
    [Fact]
    public void When_asserting_response_with_a_status_to_HaveHttpStatusCode_with_that_status_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Continue);

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHttpStatusCode(HttpStatusCode.Continue);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().HaveHttpStatusCode(HttpStatusCode.Continue);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_the_expected_status_response_to_HaveHttpStatusCode_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHttpStatusCode(HttpStatusCode.BadRequest, "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.BadRequest", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHttpStatusCode(HttpStatusCode.BadRequest, "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.BadRequest*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_HaveHttpStatusCode_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldHaveHttpStatusCode(HttpStatusCode.InternalServerError, "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().HaveHttpStatusCode(HttpStatusCode.InternalServerError, "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region NotHaveHttpStatusCode
    [Fact]
    public void When_asserting_response_with_a_status_to_NotHaveHttpStatusCode_with_that_other_status_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Continue);

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHttpStatusCode(HttpStatusCode.InsufficientStorage);

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHttpStatusCode(HttpStatusCode.InsufficientStorage);

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_response_with_the_unexpected_status_to_NotHaveHttpStatusCode_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHttpStatusCode(HttpStatusCode.OK, "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should not be", "HttpStatusCode.OK", "but was",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHttpStatusCode(HttpStatusCode.OK, "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*Did not expect*to have status HttpStatusCode.OK {value: 200}*message*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_NotHaveHttpStatusCode_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldNotHaveHttpStatusCode(HttpStatusCode.InternalServerError, "because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().NotHaveHttpStatusCode(HttpStatusCode.InternalServerError, "because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 100 Continue
    [Fact]
    public void When_asserting_100_Continue_response_to_be_100Continue_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Continue);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe100Continue();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be100Continue();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_100_Continue_response_to_be_100Continue_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe100Continue("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Continue", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be100Continue("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Continue*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_100Continue_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe100Continue("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be100Continue("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 101 Switching Protocols
    [Fact]
    public void When_asserting_101_Switching_Protocols_response_to_be_101SwitchingProtocols_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe101SwitchingProtocols();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be101SwitchingProtocols();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_101_Switching_Protocols_response_to_be_101SwitchingProtocols_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe101SwitchingProtocols("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.SwitchingProtocols", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be101SwitchingProtocols("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.SwitchingProtocols*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_101SwitchingProtocols_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe101SwitchingProtocols("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be101SwitchingProtocols("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion 

    #region 200 Ok
    [Fact]
    public void When_asserting_200_Ok_response_to_be_200_Ok_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe200Ok();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be200Ok();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_200_Ok_response_to_be_200_Ok_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.BadRequest);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe200Ok("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.OK", "but was", "HttpStatusCode.BadRequest",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be200Ok("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.OK*because we want to test the failure message, but found HttpStatusCode.BadRequest {value: 400}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_200_Ok_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe200Ok("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be200Ok("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 201 Created
    [Fact]
    public void When_asserting_201_Created_response_to_be_201Created_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Created);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe201Created();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be201Created();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_201_Created_response_to_be_201Created_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe201Created("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Created", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be201Created("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Created*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_201Created_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe201Created("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be201Created("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 202 Accepted
    [Fact]
    public void When_asserting_202_Accepted_response_to_be_202Accepted_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Accepted);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe202Accepted();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be202Accepted();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_202_Accepted_response_to_be_202Accepted_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe202Accepted("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Accepted", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be202Accepted("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Accepted*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_202Accepted_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe202Accepted("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be202Accepted("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 203 Non Authoritative Information
    [Fact]
    public void When_asserting_203_Non_Authoritative_Information_response_to_be_203NonAuthoritativeInformation_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NonAuthoritativeInformation);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe203NonAuthoritativeInformation();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be203NonAuthoritativeInformation();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_203_Non_Authoritative_Information_response_to_be_203NonAuthoritativeInformation_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe203NonAuthoritativeInformation("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NonAuthoritativeInformation", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be203NonAuthoritativeInformation("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NonAuthoritativeInformation*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_203NonAuthoritativeInformation_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe203NonAuthoritativeInformation("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be203NonAuthoritativeInformation("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 204 No Content
    [Fact]
    public void When_asserting_204_No_Content_response_to_be_204NoContent_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NoContent);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe204NoContent();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be204NoContent();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_204_No_Content_response_to_be_204NoContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe204NoContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NoContent", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be204NoContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NoContent*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_204NoContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe204NoContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be204NoContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 205 Reset Content
    [Fact]
    public void When_asserting_205_Reset_Content_response_to_be_205ResetContent_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.ResetContent);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe205ResetContent();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be205ResetContent();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_205_Reset_Content_response_to_be_205ResetContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe205ResetContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.ResetContent", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be205ResetContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.ResetContent*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_205ResetContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe205ResetContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be205ResetContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 206 Partial Content
    [Fact]
    public void When_asserting_206_Partial_Content_response_to_be_206PartialContent_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.PartialContent);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe206PartialContent();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be206PartialContent();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_206_Partial_Content_response_to_be_206PartialContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe206PartialContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.PartialContent", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be206PartialContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.PartialContent*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_206PartialContent_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe206PartialContent("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be206PartialContent("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 300 Multiple Choices
    [Fact]
    public void When_asserting_300_Multiple_Choices_response_to_be_300MultipleChoices_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.MultipleChoices);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300MultipleChoices();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be300MultipleChoices();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_300_Multiple_Choices_response_to_be_300MultipleChoices_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300MultipleChoices("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.MultipleChoices", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be300MultipleChoices("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.MultipleChoices*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_300MultipleChoices_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300MultipleChoices("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be300MultipleChoices("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 300 Ambiguous
    [Fact]
    public void When_asserting_300_Ambiguous_response_to_be_300Ambiguous_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Ambiguous);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300Ambiguous();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be300Ambiguous();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_300_Ambiguous_response_to_be_300Ambiguous_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300Ambiguous("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Ambiguous", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be300Ambiguous("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Ambiguous*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_300Ambiguous_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe300Ambiguous("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be300Ambiguous("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 301 Moved Permanently
    [Fact]
    public void When_asserting_301_Moved_Permanently_response_to_be_301MovedPermanently_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.MovedPermanently);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301MovedPermanently();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be301MovedPermanently();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_301_Moved_Permanently_response_to_be_301MovedPermanently_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301MovedPermanently("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.MovedPermanently", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be301MovedPermanently("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.MovedPermanently*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_301MovedPermanently_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301MovedPermanently("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be301MovedPermanently("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 301 Moved
    [Fact]
    public void When_asserting_301_Moved_response_to_be_301Moved_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Moved);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301Moved();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be301Moved();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_301_Moved_response_to_be_301Moved_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301Moved("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Moved", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be301Moved("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Moved*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_301Moved_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe301Moved("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be301Moved("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 302 Found
    [Fact]
    public void When_asserting_302_Found_response_to_be_302Found_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Found);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Found();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be302Found();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_302_Found_response_to_be_302Found_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Found("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Found", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be302Found("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Found*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_302Found_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Found("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be302Found("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 302 Redirect
    [Fact]
    public void When_asserting_302_Redirect_response_to_be_302Redirect_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Redirect);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Redirect();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be302Redirect();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_302_Redirect_response_to_be_302Redirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Redirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Redirect", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be302Redirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Redirect*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_302Redirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe302Redirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be302Redirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 303 See Other
    [Fact]
    public void When_asserting_303_See_Other_response_to_be_303SeeOther_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.SeeOther);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303SeeOther();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be303SeeOther();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_303_See_Other_response_to_be_303SeeOther_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303SeeOther("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.SeeOther", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be303SeeOther("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.SeeOther*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_303SeeOther_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303SeeOther("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be303SeeOther("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 303 Redirect Method
    [Fact]
    public void When_asserting_303_Redirect_Method_response_to_be_303RedirectMethod_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RedirectMethod);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303RedirectMethod();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be303RedirectMethod();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_303_Redirect_Method_response_to_be_303RedirectMethod_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303RedirectMethod("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RedirectMethod", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be303RedirectMethod("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RedirectMethod*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_303RedirectMethod_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe303RedirectMethod("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be303RedirectMethod("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 304 Not Modified
    [Fact]
    public void When_asserting_304_Not_Modified_response_to_be_304NotModified_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NotModified);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe304NotModified();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be304NotModified();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_304_Not_Modified_response_to_be_304NotModified_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe304NotModified("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NotModified", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be304NotModified("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NotModified*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_304NotModified_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe304NotModified("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be304NotModified("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 305 Use Proxy
    [Fact]
    public void When_asserting_305_Use_Proxy_response_to_be_305UseProxy_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.UseProxy);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe305UseProxy();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be305UseProxy();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_305_Use_Proxy_response_to_be_305UseProxy_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe305UseProxy("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.UseProxy", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be305UseProxy("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.UseProxy*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_305UseProxy_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe305UseProxy("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be305UseProxy("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 306 Unused
    [Fact]
    public void When_asserting_306_Unused_response_to_be_306Unused_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Unused);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe306Unused();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be306Unused();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_306_Unused_response_to_be_306Unused_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe306Unused("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Unused", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be306Unused("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Unused*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_306Unused_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe306Unused("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be306Unused("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 307 Temporary Redirect
    [Fact]
    public void When_asserting_307_Temporary_Redirect_response_to_be_307TemporaryRedirect_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.TemporaryRedirect);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307TemporaryRedirect();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be307TemporaryRedirect();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_307_Temporary_Redirect_response_to_be_307TemporaryRedirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307TemporaryRedirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.TemporaryRedirect", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be307TemporaryRedirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.TemporaryRedirect*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_307TemporaryRedirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307TemporaryRedirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be307TemporaryRedirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 307 Redirect Keep Verb
    [Fact]
    public void When_asserting_307_Redirect_Keep_Verb_response_to_be_307RedirectKeepVerb_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RedirectKeepVerb);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307RedirectKeepVerb();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be307RedirectKeepVerb();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_307_Redirect_Keep_Verb_response_to_be_307RedirectKeepVerb_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307RedirectKeepVerb("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RedirectKeepVerb", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be307RedirectKeepVerb("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RedirectKeepVerb*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_307RedirectKeepVerb_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe307RedirectKeepVerb("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be307RedirectKeepVerb("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion 

    #region 308 Permanent Redirect
    [Fact]
    public void When_asserting_308_Permanent_Redirect_response_to_be_308PermanentRedirect_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)308);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe308PermanentRedirect();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be308PermanentRedirect();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_308_Permanent_Redirect_response_to_be_308PermanentRedirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe308PermanentRedirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.PermanentRedirect", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be308PermanentRedirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.PermanentRedirect*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_308PermanentRedirect_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe308PermanentRedirect("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be308PermanentRedirect("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion 

    #region 400 BadRequest
    [Fact]
    public void When_asserting_400_BadRequest_response_to_be_400_BadRequest_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.BadRequest);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe400BadRequest();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be400BadRequest();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_400_BadRequest_response_to_be_400_BadRequest_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe400BadRequest("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.BadRequest", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be400BadRequest("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.BadRequest*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_400_BadRequest_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe400BadRequest("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be400BadRequest("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 401 Unauthorized
    [Fact]
    public void When_asserting_401_Unauthorized_response_to_be_401_Unauthorized_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Unauthorized);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe401Unauthorized();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be401Unauthorized();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_401_Unauthorized_response_to_be_401_Unauthorized_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe401Unauthorized("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Unauthorized", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be401Unauthorized("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Unauthorized*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_401_Unauthorized_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe401Unauthorized("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be401Unauthorized("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 402 Payment Required
    [Fact]
    public void When_asserting_402_Payment_Required_response_to_be_402PaymentRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.PaymentRequired);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe402PaymentRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be402PaymentRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_402_Payment_Required_response_to_be_402PaymentRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe402PaymentRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.PaymentRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be402PaymentRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.PaymentRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_402PaymentRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe402PaymentRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be402PaymentRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 403 Forbidden
    [Fact]
    public void When_asserting_403_Forbidden_response_to_be_403Forbidden_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Forbidden);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe403Forbidden();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be403Forbidden();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_403_Forbidden_response_to_be_403Forbidden_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe403Forbidden("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Forbidden", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be403Forbidden("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Forbidden*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_403Forbidden_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe403Forbidden("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be403Forbidden("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 404 Not Found
    [Fact]
    public void When_asserting_404_Not_Found_response_to_be_404NotFound_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NotFound);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe404NotFound();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be404NotFound();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_404_Not_Found_response_to_be_404NotFound_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe404NotFound("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NotFound", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be404NotFound("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NotFound*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_404NotFound_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe404NotFound("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be404NotFound("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 405 Method Not Allowed
    [Fact]
    public void When_asserting_405_Method_Not_Allowed_response_to_be_405MethodNotAllowed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe405MethodNotAllowed();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be405MethodNotAllowed();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_405_Method_Not_Allowed_response_to_be_405MethodNotAllowed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe405MethodNotAllowed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.MethodNotAllowed", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be405MethodNotAllowed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.MethodNotAllowed*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_405MethodNotAllowed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe405MethodNotAllowed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be405MethodNotAllowed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 406 Not Acceptable
    [Fact]
    public void When_asserting_406_Not_Acceptable_response_to_be_406NotAcceptable_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NotAcceptable);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe406NotAcceptable();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be406NotAcceptable();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_406_Not_Acceptable_response_to_be_406NotAcceptable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe406NotAcceptable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NotAcceptable", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be406NotAcceptable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NotAcceptable*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_406NotAcceptable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe406NotAcceptable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be406NotAcceptable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 407 Proxy Authentication Required
    [Fact]
    public void When_asserting_407_Proxy_Authentication_Required_response_to_be_407ProxyAuthenticationRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.ProxyAuthenticationRequired);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe407ProxyAuthenticationRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be407ProxyAuthenticationRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_407_Proxy_Authentication_Required_response_to_be_407ProxyAuthenticationRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe407ProxyAuthenticationRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.ProxyAuthenticationRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be407ProxyAuthenticationRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.ProxyAuthenticationRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_407ProxyAuthenticationRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe407ProxyAuthenticationRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be407ProxyAuthenticationRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 408 Request Timeout
    [Fact]
    public void When_asserting_408_Request_Timeout_response_to_be_408RequestTimeout_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RequestTimeout);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe408RequestTimeout();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be408RequestTimeout();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_408_Request_Timeout_response_to_be_408RequestTimeout_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe408RequestTimeout("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RequestTimeout", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be408RequestTimeout("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RequestTimeout*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_408RequestTimeout_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe408RequestTimeout("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be408RequestTimeout("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 409 Conflict
    [Fact]
    public void When_asserting_409_Conflict_response_to_be_409Conflict_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Conflict);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe409Conflict();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be409Conflict();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_409_Conflict_response_to_be_409Conflict_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe409Conflict("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Conflict", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be409Conflict("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Conflict*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_409Conflict_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe409Conflict("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be409Conflict("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 410 Gone
    [Fact]
    public void When_asserting_410_Gone_response_to_be_410Gone_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.Gone);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe410Gone();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be410Gone();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_410_Gone_response_to_be_410Gone_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe410Gone("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Gone", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be410Gone("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Gone*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_410Gone_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe410Gone("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be410Gone("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 411 Length Required
    [Fact]
    public void When_asserting_411_Length_Required_response_to_be_411LengthRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.LengthRequired);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe411LengthRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be411LengthRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_411_Length_Required_response_to_be_411LengthRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe411LengthRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.LengthRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be411LengthRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.LengthRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_411LengthRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe411LengthRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be411LengthRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 412 Precondition Failed
    [Fact]
    public void When_asserting_412_Precondition_Failed_response_to_be_412PreconditionFailed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe412PreconditionFailed();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be412PreconditionFailed();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_412_Precondition_Failed_response_to_be_412PreconditionFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe412PreconditionFailed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.PreconditionFailed", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be412PreconditionFailed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.PreconditionFailed*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_412PreconditionFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe412PreconditionFailed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be412PreconditionFailed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 413 Request Entity Too Large
    [Fact]
    public void When_asserting_413_Request_Entity_Too_Large_response_to_be_413RequestEntityTooLarge_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RequestEntityTooLarge);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe413RequestEntityTooLarge();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be413RequestEntityTooLarge();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_413_Request_Entity_Too_Large_response_to_be_413RequestEntityTooLarge_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe413RequestEntityTooLarge("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RequestEntityTooLarge", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be413RequestEntityTooLarge("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RequestEntityTooLarge*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_413RequestEntityTooLarge_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe413RequestEntityTooLarge("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be413RequestEntityTooLarge("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 414 Request Uri Too Long
    [Fact]
    public void When_asserting_414_Request_Uri_Too_Long_response_to_be_414RequestUriTooLong_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RequestUriTooLong);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe414RequestUriTooLong();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be414RequestUriTooLong();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_414_Request_Uri_Too_Long_response_to_be_414RequestUriTooLong_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe414RequestUriTooLong("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RequestUriTooLong", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be414RequestUriTooLong("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RequestUriTooLong*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_414RequestUriTooLong_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe414RequestUriTooLong("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be414RequestUriTooLong("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 415 Unsupported Media Type
    [Fact]
    public void When_asserting_415_Unsupported_Media_Type_response_to_be_415UnsupportedMediaType_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe415UnsupportedMediaType();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be415UnsupportedMediaType();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_415_Unsupported_Media_Type_response_to_be_415UnsupportedMediaType_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe415UnsupportedMediaType("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.UnsupportedMediaType", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be415UnsupportedMediaType("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.UnsupportedMediaType*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_415UnsupportedMediaType_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe415UnsupportedMediaType("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be415UnsupportedMediaType("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 416 Requested Range Not Satisfiable
    [Fact]
    public void When_asserting_416_Requested_Range_Not_Satisfiable_response_to_be_416RequestedRangeNotSatisfiable_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.RequestedRangeNotSatisfiable);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe416RequestedRangeNotSatisfiable();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be416RequestedRangeNotSatisfiable();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_416_Requested_Range_Not_Satisfiable_response_to_be_416RequestedRangeNotSatisfiable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe416RequestedRangeNotSatisfiable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RequestedRangeNotSatisfiable", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be416RequestedRangeNotSatisfiable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RequestedRangeNotSatisfiable*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_416RequestedRangeNotSatisfiable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe416RequestedRangeNotSatisfiable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be416RequestedRangeNotSatisfiable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 417 Expectation Failed
    [Fact]
    public void When_asserting_417_Expectation_Failed_response_to_be_417ExpectationFailed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe417ExpectationFailed();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be417ExpectationFailed();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_417_Expectation_Failed_response_to_be_417ExpectationFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe417ExpectationFailed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.ExpectationFailed", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be417ExpectationFailed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.ExpectationFailed*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_417ExpectationFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe417ExpectationFailed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be417ExpectationFailed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 418 I'm A Teapot
    [Fact]
    public void When_asserting_418_Im_A_Teapot_response_to_be_418ImATeapot_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)418);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe418ImATeapot();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be418ImATeapot();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_418_Im_A_Teapot_response_to_be_418ImATeapot_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe418ImATeapot("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.ImATeapot", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be418ImATeapot("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.ImATeapot*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_418_Im_A_Teapot_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe418ImATeapot("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be418ImATeapot("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 422 Unprocessable Entity
    [Fact]
    public void When_asserting_422_Unprocessable_Entity_response_to_be_417ExpectationFailed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)422);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe422UnprocessableEntity();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be422UnprocessableEntity();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_422_Unprocessable_Entity_response_to_be_417ExpectationFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe422UnprocessableEntity("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.UnprocessableEntity", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be422UnprocessableEntity("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.UnprocessableEntity*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_422_Unprocessable_Entity_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe422UnprocessableEntity("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be422UnprocessableEntity("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 429 Too Many Requests
    [Fact]
    public void When_asserting_429_Too_Many_Requests_response_to_be_417ExpectationFailed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)429);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe429TooManyRequests();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be429TooManyRequests();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_429_Too_Many_Requests_response_to_be_417ExpectationFailed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe429TooManyRequests("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.TooManyRequests", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be429TooManyRequests("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.TooManyRequests*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_429_Too_Many_Requests_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe429TooManyRequests("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be429TooManyRequests("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 426 Upgrade Required
    [Fact]
    public void When_asserting_426_Upgrade_Required_response_to_be_426UpgradeRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.UpgradeRequired);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe426UpgradeRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be426UpgradeRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_426_Upgrade_Required_response_to_be_426UpgradeRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe426UpgradeRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.UpgradeRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be426UpgradeRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.UpgradeRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_426UpgradeRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe426UpgradeRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be426UpgradeRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 500 Internal Server Error
    [Fact]
    public void When_asserting_500_Internal_Server_Error_response_to_be_500InternalServerError_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.InternalServerError);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe500InternalServerError();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be500InternalServerError();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_500_Internal_Server_Error_response_to_be_500InternalServerError_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe500InternalServerError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.InternalServerError", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be500InternalServerError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.InternalServerError*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_500InternalServerError_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe500InternalServerError("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be500InternalServerError("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 501 Not Implemented
    [Fact]
    public void When_asserting_501_Not_Implemented_response_to_be_501NotImplemented_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.NotImplemented);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe501NotImplemented();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be501NotImplemented();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_501_Not_Implemented_response_to_be_501NotImplemented_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe501NotImplemented("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NotImplemented", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be501NotImplemented("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NotImplemented*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_501NotImplemented_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe501NotImplemented("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be501NotImplemented("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 502 Bad Gateway
    [Fact]
    public void When_asserting_502_Bad_Gateway_response_to_be_502BadGateway_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.BadGateway);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe502BadGateway();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be502BadGateway();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_502_Bad_Gateway_response_to_be_502BadGateway_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe502BadGateway("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.BadGateway", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be502BadGateway("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.BadGateway*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_502BadGateway_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe502BadGateway("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be502BadGateway("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 503 Service Unavailable
    [Fact]
    public void When_asserting_503_Service_Unavailable_response_to_be_503ServiceUnavailable_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe503ServiceUnavailable();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be503ServiceUnavailable();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_503_Service_Unavailable_response_to_be_503ServiceUnavailable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe503ServiceUnavailable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.ServiceUnavailable", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be503ServiceUnavailable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.ServiceUnavailable*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_503ServiceUnavailable_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe503ServiceUnavailable("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be503ServiceUnavailable("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 504 Gateway Timeout
    [Fact]
    public void When_asserting_504_Gateway_Timeout_response_to_be_504GatewayTimeout_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.GatewayTimeout);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe504GatewayTimeout();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be504GatewayTimeout();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_504_Gateway_Timeout_response_to_be_504GatewayTimeout_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe504GatewayTimeout("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.GatewayTimeout", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be504GatewayTimeout("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.GatewayTimeout*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_504GatewayTimeout_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe504GatewayTimeout("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be504GatewayTimeout("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 505 Http Version Not Supported
    [Fact]
    public void When_asserting_505_Http_Version_Not_Supported_response_to_be_505HttpVersionNotSupported_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.HttpVersionNotSupported);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe505HttpVersionNotSupported();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be505HttpVersionNotSupported();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_505_Http_Version_Not_Supported_response_to_be_505HttpVersionNotSupported_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe505HttpVersionNotSupported("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.HttpVersionNotSupported", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be505HttpVersionNotSupported("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.HttpVersionNotSupported*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_be_505HttpVersionNotSupported_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe505HttpVersionNotSupported("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be505HttpVersionNotSupported("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion 

    #region 102 Processing
    [Fact]
    public void When_asserting_102_Processing_response_to_be_102Processing_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)102);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe102Processing();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be102Processing();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_102_Processing_response_to_be_102Processing_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe102Processing("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Processing", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be102Processing("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Processing*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_102_Processing_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe102Processing("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be102Processing("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 103 Early Hints
    [Fact]
    public void When_asserting_103_Early_Hints_response_to_be_103EarlyHints_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)103);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe103EarlyHints();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be103EarlyHints();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_103_Early_Hints_response_to_be_103EarlyHints_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe103EarlyHints("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.EarlyHints", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be103EarlyHints("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.EarlyHints*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_103_Early_Hints_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe103EarlyHints("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be103EarlyHints("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 207 Multi-Status
    [Fact]
    public void When_asserting_207_Multi_Status_response_to_be_207MultiStatus_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)207);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe207MultiStatus();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be207MultiStatus();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_207_Multi_Status_response_to_be_207MultiStatus_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe207MultiStatus("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.MultiStatus", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be207MultiStatus("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.MultiStatus*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_207_Multi_Status_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe207MultiStatus("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be207MultiStatus("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 208 Already Reported
    [Fact]
    public void When_asserting_208_Already_Reported_response_to_be_208AlreadyReported_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)208);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe208AlreadyReported();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be208AlreadyReported();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_208_Already_Reported_response_to_be_208AlreadyReported_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe208AlreadyReported("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.AlreadyReported", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be208AlreadyReported("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.AlreadyReported*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_208_Already_Reported_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe208AlreadyReported("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be208AlreadyReported("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 226 IM Used
    [Fact]
    public void When_asserting_226_IM_Used_response_to_be_226IMUsed_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)226);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe226IMUsed();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be226IMUsed();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_226_IM_Used_response_to_be_226IMUsed_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe226IMUsed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.IMUsed", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be226IMUsed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.IMUsed*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_226_IM_Used_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe226IMUsed("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be226IMUsed("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 421 Misdirected Request
    [Fact]
    public void When_asserting_421_Misdirected_Request_response_to_be_421MisdirectedRequest_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)421);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe421MisdirectedRequest();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be421MisdirectedRequest();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_421_Misdirected_Request_response_to_be_421MisdirectedRequest_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe421MisdirectedRequest("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.MisdirectedRequest", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be421MisdirectedRequest("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.MisdirectedRequest*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_421_Misdirected_Request_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe421MisdirectedRequest("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be421MisdirectedRequest("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 423 Locked
    [Fact]
    public void When_asserting_423_Locked_response_to_be_423Locked_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)423);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe423Locked();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be423Locked();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_423_Locked_response_to_be_423Locked_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe423Locked("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.Locked", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be423Locked("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.Locked*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_423_Locked_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe423Locked("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be423Locked("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 424 Failed Dependency
    [Fact]
    public void When_asserting_424_Failed_Dependency_response_to_be_424FailedDependency_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)424);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe424FailedDependency();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be424FailedDependency();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_424_Failed_Dependency_response_to_be_424FailedDependency_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe424FailedDependency("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.FailedDependency", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be424FailedDependency("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.FailedDependency*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_424_Failed_Dependency_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe424FailedDependency("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be424FailedDependency("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 425 Too Early
    [Fact]
    public void When_asserting_425_Too_Early_response_to_be_425TooEarly_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)425);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe425TooEarly();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be425TooEarly();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_425_Too_Early_response_to_be_425TooEarly_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe425TooEarly("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.TooEarly", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be425TooEarly("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.TooEarly*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_425_Too_Early_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe425TooEarly("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be425TooEarly("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 428 Precondition Required
    [Fact]
    public void When_asserting_428_Precondition_Required_response_to_be_428PreconditionRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)428);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe428PreconditionRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be428PreconditionRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_428_Precondition_Required_response_to_be_428PreconditionRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe428PreconditionRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.PreconditionRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be428PreconditionRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.PreconditionRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_428_Precondition_Required_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe428PreconditionRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be428PreconditionRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 431 Request Header Fields Too Large
    [Fact]
    public void When_asserting_431_Request_Header_Fields_Too_Large_response_to_be_431RequestHeaderFieldsTooLarge_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)431);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe431RequestHeaderFieldsTooLarge();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be431RequestHeaderFieldsTooLarge();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_431_Request_Header_Fields_Too_Large_response_to_be_431RequestHeaderFieldsTooLarge_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe431RequestHeaderFieldsTooLarge("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.RequestHeaderFieldsTooLarge", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be431RequestHeaderFieldsTooLarge("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.RequestHeaderFieldsTooLarge*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_431_Request_Header_Fields_Too_Large_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe431RequestHeaderFieldsTooLarge("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be431RequestHeaderFieldsTooLarge("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 451 Unavailable For Legal Reasons
    [Fact]
    public void When_asserting_451_Unavailable_For_Legal_Reasons_response_to_be_451UnavailableForLegalReasons_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)451);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe451UnavailableForLegalReasons();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be451UnavailableForLegalReasons();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_451_Unavailable_For_Legal_Reasons_response_to_be_451UnavailableForLegalReasons_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe451UnavailableForLegalReasons("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.UnavailableForLegalReasons", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be451UnavailableForLegalReasons("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.UnavailableForLegalReasons*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_451_Unavailable_For_Legal_Reasons_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe451UnavailableForLegalReasons("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be451UnavailableForLegalReasons("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 506 Variant Also Negotiates
    [Fact]
    public void When_asserting_506_Variant_Also_Negotiates_response_to_be_506VariantAlsoNegotiates_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)506);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe506VariantAlsoNegotiates();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be506VariantAlsoNegotiates();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_506_Variant_Also_Negotiates_response_to_be_506VariantAlsoNegotiates_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe506VariantAlsoNegotiates("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.VariantAlsoNegotiates", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be506VariantAlsoNegotiates("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.VariantAlsoNegotiates*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_506_Variant_Also_Negotiates_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe506VariantAlsoNegotiates("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be506VariantAlsoNegotiates("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 507 Insufficient Storage
    [Fact]
    public void When_asserting_507_Insufficient_Storage_response_to_be_507InsufficientStorage_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)507);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe507InsufficientStorage();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be507InsufficientStorage();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_507_Insufficient_Storage_response_to_be_507InsufficientStorage_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe507InsufficientStorage("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.InsufficientStorage", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be507InsufficientStorage("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.InsufficientStorage*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_507_Insufficient_Storage_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe507InsufficientStorage("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be507InsufficientStorage("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 508 Loop Detected
    [Fact]
    public void When_asserting_508_Loop_Detected_response_to_be_508LoopDetected_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)508);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe508LoopDetected();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be508LoopDetected();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_508_Loop_Detected_response_to_be_508LoopDetected_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe508LoopDetected("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.LoopDetected", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be508LoopDetected("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.LoopDetected*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_508_Loop_Detected_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe508LoopDetected("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be508LoopDetected("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 510 Not Extended
    [Fact]
    public void When_asserting_510_Not_Extended_response_to_be_510NotExtended_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)510);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe510NotExtended();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be510NotExtended();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_510_Not_Extended_response_to_be_510NotExtended_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe510NotExtended("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NotExtended", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be510NotExtended("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NotExtended*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_510_Not_Extended_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe510NotExtended("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be510NotExtended("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion

    #region 511 Network Authentication Required
    [Fact]
    public void When_asserting_511_Network_Authentication_Required_response_to_be_511NetworkAuthenticationRequired_it_should_succeed()
    {
        // Arrange
        using var subject = new HttpResponseMessage((HttpStatusCode)511);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe511NetworkAuthenticationRequired();

        // Assert
        act.ShouldNotThrow();
#else
        // Act
        Action act = () =>
            subject.Should().Be511NetworkAuthenticationRequired();

        // Assert
        act.Should().NotThrow();
#endif
    }

    [Fact]
    public void When_asserting_other_than_511_Network_Authentication_Required_response_to_be_511NetworkAuthenticationRequired_it_should_throw_with_descriptive_message()
    {
        // Arrange
        using var subject = new HttpResponseMessage(HttpStatusCode.OK);

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe511NetworkAuthenticationRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject.StatusCode",
            "should be", "HttpStatusCode.NetworkAuthenticationRequired", "but was", "HttpStatusCode.OK",
            "Additional Info:", "because we want to test the failure message",
            "The HTTP response was:");
#else
        // Act
        Action act = () =>
            subject.Should().Be511NetworkAuthenticationRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("*HttpStatusCode.NetworkAuthenticationRequired*because we want to test the failure message, but found HttpStatusCode.OK {value: 200}*");
#endif
    }

    [Fact]
    public void When_asserting_null_HttpResponse_to_511_Network_Authentication_Required_it_should_throw_with_descriptive_message()
    {
        // Arrange
        HttpResponseMessage? subject = null;

#if SH
        // Act
        Action act = () =>
            subject.ShouldBe511NetworkAuthenticationRequired("because we want to test the failure message");

        // Assert
        act.ShouldFailWith("subject", "should not be null", "Additional Info:", "because we want to test the failure message");
#else
        // Act
        Action act = () =>
            subject.Should().Be511NetworkAuthenticationRequired("because we want to test the failure {0}", "message");

        // Assert
        act.Should().Throw<XunitException>()
            .WithMessage("Expected a * to assert because we want to test the failure message, but found <null>.");
#endif
    }
    #endregion
}