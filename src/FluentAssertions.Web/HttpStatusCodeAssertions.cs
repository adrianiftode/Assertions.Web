#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

public partial class HttpResponseMessageAssertions
{
    #region Be1XXInformational
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing an informational response.
    /// </summary>
    /// <remarks>The HTTP response was an informational one if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 100-199.</remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public AndConstraint<HttpResponseMessageAssertions> Be1XXInformational(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject!.StatusCode.IsInformational())
            .FailWith("Expected {context:response} to have an HTTP status code representing an informational error, but it was {0}{reason}.{1}",
                Subject!.StatusCode, Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region Be2XXSuccessful
    /// <summary>
    /// Asserts that an HTTP response has a successful HTTP status code.
    /// </summary>
    /// <remarks>The HTTP response was successful if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 200-299.</remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public AndConstraint<HttpResponseMessageAssertions> Be2XXSuccessful(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject!.StatusCode.IsSuccessful())
            .FailWith("Expected {context:response} to have a successful HTTP status code, but it was {0}{reason}.{1}",
                Subject!.StatusCode, Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region Be3XXRedirection
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a redirection response.
    /// </summary>
    /// <remarks>The HTTP response was an informational one if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 300-399.</remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public AndConstraint<LocationAssertions> Be3XXRedirection(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject!.StatusCode.IsRedirection())
            .FailWith("Expected {context:response} to have an HTTP status code representing a redirection, but it was {0}{reason}.{1}",
                Subject!.StatusCode, Subject);

#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }
    #endregion

    #region Be4XXClientError
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a client error.
    /// </summary>
    /// <remarks>The HTTP response was a client error if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 400-499.</remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public AndConstraint<HttpResponseMessageAssertions> Be4XXClientError(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject!.StatusCode.IsClientError())
            .FailWith("Expected {context:response} to have an HTTP status code representing a client error, but it was {0}{reason}.{1}",
                Subject!.StatusCode, Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region Be5XXServerError
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status code representing a server error.
    /// </summary>
    /// <remarks>The HTTP response was a server error if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was above 500.</remarks>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public AndConstraint<HttpResponseMessageAssertions> Be5XXServerError(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject!.StatusCode.IsServerError())
            .FailWith("Expected {context:response} to have an HTTP status code representing a server error, but it was {0}{reason}.{1}",
                Subject!.StatusCode, Subject);

        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region HaveHttpStatus
    /// <summary>
    /// Asserts that an HTTP response has an HTTP status with the specified code.
    /// </summary>
    /// <param name="expected">
    /// The code of the expected HTTP Status.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> HaveHttpStatusCode(HttpStatusCode expected, string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(expected == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , expected, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region NotHaveHttpStatus
    /// <summary>
    /// Asserts that an HTTP response does not have an HTTP status with the specified code.
    /// </summary>
    /// <param name="unexpected">
    /// The code of the unexpected HTTP Status.
    /// </param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> NotHaveHttpStatusCode(HttpStatusCode unexpected, string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(unexpected != Subject!.StatusCode)
            .FailWith("Did not expect {context:response} to have status {0}{reason}.{1}",
                Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
    #endregion

    #region BeXXXHttpStatus
    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 100 Continue
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be100Continue(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Continue == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Continue, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 101 Switching Protocols
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be101SwitchingProtocols(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.SwitchingProtocols == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.SwitchingProtocols, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 200 Ok
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be200Ok(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.OK == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.OK, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 201 Created
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be201Created(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Created == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Created, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 202 Accepted
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be202Accepted(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Accepted == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Accepted, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 203 Non Authoritative Information
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be203NonAuthoritativeInformation(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NonAuthoritativeInformation == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NonAuthoritativeInformation, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 204 No Content
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be204NoContent(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NoContent == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NoContent, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 205 Reset Content
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be205ResetContent(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.ResetContent == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.ResetContent, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 206 Partial Content
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be206PartialContent(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.PartialContent == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.PartialContent, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 300 Multiple Choices
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be300MultipleChoices(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.MultipleChoices == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.MultipleChoices {value: 300}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 300 Ambiguous
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be300Ambiguous(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Ambiguous == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , $"{nameof(HttpStatusCode)}.{nameof(HttpStatusCode.Ambiguous)}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 301 Moved Permanently
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be301MovedPermanently(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.MovedPermanently == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.MovedPermanently {value: 301}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 301 Moved
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be301Moved(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Moved == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Moved, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 302 Found
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be302Found(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Found == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.Found {value: 302}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 302 Redirect
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be302Redirect(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Redirect == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , $"{nameof(HttpStatusCode)}.{nameof(HttpStatusCode.Redirect)}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 303 See Other
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be303SeeOther(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.SeeOther == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.SeeOther {value: 303}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 303 Redirect Method
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be303RedirectMethod(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RedirectMethod == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , $"{nameof(HttpStatusCode)}.{nameof(HttpStatusCode.RedirectMethod)}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 304 Not Modified
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be304NotModified(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NotModified == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NotModified, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 305 Use Proxy
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be305UseProxy(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.UseProxy == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.UseProxy, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 306 Unused
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be306Unused(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Unused == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Unused, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 307 Temporary Redirect
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be307TemporaryRedirect(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.TemporaryRedirect == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , $"{nameof(HttpStatusCode)}.{nameof(HttpStatusCode.TemporaryRedirect)}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 307 Redirect Keep Verb
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be307RedirectKeepVerb(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RedirectKeepVerb == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.RedirectKeepVerb {value: 307}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 308 Permanent Redirect
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be308PermanentRedirect(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(308 == (int)Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.PermanentRedirect {value: 308}", Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 400 BadRequest
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<BadRequestAssertions> Be400BadRequest(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.BadRequest == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.BadRequest, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<BadRequestAssertions>(new BadRequestAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<BadRequestAssertions>(new BadRequestAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 401 Unauthorized
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be401Unauthorized(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Unauthorized == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Unauthorized, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 402 Payment Required
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be402PaymentRequired(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.PaymentRequired == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.PaymentRequired, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 403 Forbidden
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be403Forbidden(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Forbidden == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Forbidden, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 404 Not Found
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be404NotFound(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NotFound == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NotFound, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 405 Method Not Allowed
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be405MethodNotAllowed(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.MethodNotAllowed == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.MethodNotAllowed, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 406 Not Acceptable
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be406NotAcceptable(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NotAcceptable == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NotAcceptable, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 407 Proxy Authentication Required
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be407ProxyAuthenticationRequired(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.ProxyAuthenticationRequired == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.ProxyAuthenticationRequired, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 408 Request Timeout
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be408RequestTimeout(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RequestTimeout == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.RequestTimeout, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 409 Conflict
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be409Conflict(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Conflict == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Conflict, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 410 Gone
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<LocationAssertions> Be410Gone(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.Gone == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.Gone, Subject!.StatusCode, Subject);
#if FAV8
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject, CurrentAssertionChain));
#else
        return new AndConstraint<LocationAssertions>(new LocationAssertions(Subject));
#endif
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 411 Length Required
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be411LengthRequired(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.LengthRequired == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.LengthRequired, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 412 Precondition Failed
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be412PreconditionFailed(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.PreconditionFailed == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.PreconditionFailed, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 413 Request Entity Too Large
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be413RequestEntityTooLarge(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RequestEntityTooLarge == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.RequestEntityTooLarge, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 414 Request Uri Too Long
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be414RequestUriTooLong(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RequestUriTooLong == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.RequestUriTooLong, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 415 Unsupported Media Type
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be415UnsupportedMediaType(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.UnsupportedMediaType == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.UnsupportedMediaType, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 416 Requested Range Not Satisfiable
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be416RequestedRangeNotSatisfiable(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.RequestedRangeNotSatisfiable == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.RequestedRangeNotSatisfiable, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 417 Expectation Failed
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be417ExpectationFailed(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.ExpectationFailed == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.ExpectationFailed, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 418 I'm A Teapot
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be418ImATeapot(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(418 == (int)Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.ImATeapot", Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 422 UnprocessableEntity
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be422UnprocessableEntity(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(422 == (int)Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.UnprocessableEntity", Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 429 TooManyRequests
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be429TooManyRequests(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(429 == (int)Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , "HttpStatusCode.TooManyRequests", Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 426 UpgradeRequired
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be426UpgradeRequired(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.UpgradeRequired == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.UpgradeRequired, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 500 Internal Server Error
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be500InternalServerError(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.InternalServerError == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.InternalServerError, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 501 Not Implemented
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be501NotImplemented(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.NotImplemented == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.NotImplemented, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 502 Bad Gateway
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be502BadGateway(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.BadGateway == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.BadGateway, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 503 Service Unavailable
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be503ServiceUnavailable(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.ServiceUnavailable == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.ServiceUnavailable, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 504 Gateway Timeout
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be504GatewayTimeout(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.GatewayTimeout == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.GatewayTimeout, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 505 Http Version Not Supported
    /// </summary>        
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public AndConstraint<HttpResponseMessageAssertions> Be505HttpVersionNotSupported(string because = "", params object[] becauseArgs)
    {
#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected a {context:response} to assert{reason}, but found <null>.");

#if FAV8
        CurrentAssertionChain
#else
        Execute.Assertion
#endif
            .BecauseOf(because, becauseArgs)
            .ForCondition(HttpStatusCode.HttpVersionNotSupported == Subject!.StatusCode)
            .FailWith("Expected {context:response} to be {0}{reason}, but found {1}.{2}"
                , HttpStatusCode.HttpVersionNotSupported, Subject!.StatusCode, Subject);
        return new AndConstraint<HttpResponseMessageAssertions>(this);
    }
#endregion
}