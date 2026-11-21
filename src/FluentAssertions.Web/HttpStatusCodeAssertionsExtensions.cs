#if !FAV8
// ReSharper disable once CheckNamespace
namespace FluentAssertions;

/// <summary>
/// Contains extension methods for custom assertions in unit tests related to the HTTP status code of an
/// <see cref="HttpResponseMessage"/>.
/// </summary>
/// <remarks>
/// <para>
/// In FluentAssertions versions before 8, <c>response.Should()</c> returns FluentAssertions' own response assertion
/// type, so these extension methods provide the Assertions.Web status assertions. Each one returns an
/// <c>AndConstraint&lt;TAssertions&gt;</c> for an Assertions.Web assertion group, and the chain continues from its
/// <c>And</c> property exactly as it does in AwesomeAssertions and FluentAssertions 8.
/// </para>
/// <para>
/// Most status assertions return <see cref="HttpResponseMessageAssertions"/>. <c>Be400BadRequest</c> returns
/// <see cref="BadRequestAssertions"/>. Some status assertions (for example <c>Be201Created</c>,
/// <c>Be3XXRedirection</c>, <c>Be204NoContent</c>, <c>Be409Conflict</c> and <c>Be410Gone</c>) return
/// <see cref="LocationAssertions"/>, which allows the chain to inspect the <c>Location</c> header; returning that
/// group does not assert that the header is present.
/// </para>
/// </remarks>
/// <seealso cref="BadRequestAssertions"/>
/// <seealso cref="HeadersAssertions"/>
/// <seealso cref="LocationAssertions"/>
public static class HttpStatusCodeAssertionsExtensions
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
    public static AndConstraint<HttpResponseMessageAssertions> Be1XXInformational(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be1XXInformational(because, becauseArgs);
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
    public static AndConstraint<HttpResponseMessageAssertions> Be2XXSuccessful(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be2XXSuccessful(because, becauseArgs);
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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    // ReSharper disable once InconsistentNaming
    public static AndConstraint<LocationAssertions> Be3XXRedirection(
#pragma warning disable 1573
    this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
    string because = "", params object[] becauseArgs)
    => new LocationAssertions(parent.Subject).Be3XXRedirection(because, becauseArgs);
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
    public static AndConstraint<HttpResponseMessageAssertions> Be4XXClientError(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be4XXClientError(because, becauseArgs);
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
    public static AndConstraint<HttpResponseMessageAssertions> Be5XXServerError(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be5XXServerError(because, becauseArgs);
    #endregion

    #region HaveHtppStatus
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
    public static AndConstraint<HttpResponseMessageAssertions> HaveHttpStatusCode(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        HttpStatusCode expected, string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).HaveHttpStatusCode(expected, because, becauseArgs);
    #endregion

    #region NotHaveHtppStatus
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
    public static AndConstraint<HttpResponseMessageAssertions> NotHaveHttpStatusCode(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        HttpStatusCode unexpected, string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).NotHaveHttpStatusCode(unexpected, because, becauseArgs);
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
    public static AndConstraint<HttpResponseMessageAssertions> Be100Continue(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be100Continue(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be101SwitchingProtocols(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be101SwitchingProtocols(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be200Ok(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be200Ok(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be201Created(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be201Created(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be202Accepted(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be202Accepted(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be203NonAuthoritativeInformation(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be203NonAuthoritativeInformation(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be204NoContent(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be204NoContent(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be205ResetContent(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be205ResetContent(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be206PartialContent(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be206PartialContent(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be300MultipleChoices(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be300MultipleChoices(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be300Ambiguous(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be300Ambiguous(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be301MovedPermanently(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be301MovedPermanently(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be301Moved(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be301Moved(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be302Found(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be302Found(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be302Redirect(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be302Redirect(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be303SeeOther(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be303SeeOther(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be303RedirectMethod(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be303RedirectMethod(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be304NotModified(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be304NotModified(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be305UseProxy(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be305UseProxy(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be306Unused(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be306Unused(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be307TemporaryRedirect(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be307TemporaryRedirect(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be307RedirectKeepVerb(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be307RedirectKeepVerb(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be308PermanentRedirect(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be308PermanentRedirect(because, becauseArgs);

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
    /// <returns>
    /// A <see cref="BadRequestAssertions"/> for the response, so validation-error assertions such as
    /// <c>HaveError</c> can be chained from <c>And</c>.
    /// </returns>
    [CustomAssertion]
    public static AndConstraint<BadRequestAssertions> Be400BadRequest(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new BadRequestAssertions(parent.Subject).Be400BadRequest(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be401Unauthorized(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be401Unauthorized(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be402PaymentRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be402PaymentRequired(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be403Forbidden(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be403Forbidden(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be404NotFound(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be404NotFound(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be405MethodNotAllowed(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be405MethodNotAllowed(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be406NotAcceptable(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be406NotAcceptable(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be407ProxyAuthenticationRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be407ProxyAuthenticationRequired(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be408RequestTimeout(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be408RequestTimeout(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be409Conflict(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be409Conflict(because, becauseArgs);

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
    /// <returns>The <see cref="LocationAssertions"/> group for the response.</returns>
    [CustomAssertion]
    public static AndConstraint<LocationAssertions> Be410Gone(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new LocationAssertions(parent.Subject).Be410Gone(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be411LengthRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be411LengthRequired(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be412PreconditionFailed(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be412PreconditionFailed(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be413RequestEntityTooLarge(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be413RequestEntityTooLarge(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be414RequestUriTooLong(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be414RequestUriTooLong(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be415UnsupportedMediaType(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be415UnsupportedMediaType(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be416RequestedRangeNotSatisfiable(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be416RequestedRangeNotSatisfiable(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be417ExpectationFailed(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be417ExpectationFailed(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be418ImATeapot(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be418ImATeapot(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be422UnprocessableEntity(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be422UnprocessableEntity(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be429TooManyRequests(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be429TooManyRequests(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be426UpgradeRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be426UpgradeRequired(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be500InternalServerError(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be500InternalServerError(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be501NotImplemented(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be501NotImplemented(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be502BadGateway(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be502BadGateway(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be503ServiceUnavailable(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be503ServiceUnavailable(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be504GatewayTimeout(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be504GatewayTimeout(because, becauseArgs);

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
    public static AndConstraint<HttpResponseMessageAssertions> Be505HttpVersionNotSupported(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be505HttpVersionNotSupported(because, becauseArgs);
    #endregion

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 102 Processing
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be102Processing(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be102Processing(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 103 Early Hints
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be103EarlyHints(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be103EarlyHints(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 207 Multi-Status
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be207MultiStatus(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be207MultiStatus(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 208 Already Reported
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be208AlreadyReported(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be208AlreadyReported(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 226 IM Used
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be226IMUsed(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be226IMUsed(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 421 Misdirected Request
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be421MisdirectedRequest(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be421MisdirectedRequest(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 423 Locked
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be423Locked(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be423Locked(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 424 Failed Dependency
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be424FailedDependency(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be424FailedDependency(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 425 Too Early
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be425TooEarly(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be425TooEarly(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 428 Precondition Required
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be428PreconditionRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be428PreconditionRequired(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 431 Request Header Fields Too Large
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be431RequestHeaderFieldsTooLarge(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be431RequestHeaderFieldsTooLarge(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 451 Unavailable For Legal Reasons
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be451UnavailableForLegalReasons(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be451UnavailableForLegalReasons(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 506 Variant Also Negotiates
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be506VariantAlsoNegotiates(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be506VariantAlsoNegotiates(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 507 Insufficient Storage
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be507InsufficientStorage(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be507InsufficientStorage(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 508 Loop Detected
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be508LoopDetected(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be508LoopDetected(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 510 Not Extended
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be510NotExtended(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be510NotExtended(because, becauseArgs);

    /// <summary>
    /// Asserts that an HTTP response has the HTTP status 511 Network Authentication Required
    /// </summary>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <see paramref="because" />.
    /// </param>
    [CustomAssertion]
    public static AndConstraint<HttpResponseMessageAssertions> Be511NetworkAuthenticationRequired(
#pragma warning disable 1573
        this Primitives.HttpResponseMessageAssertions<Primitives.HttpResponseMessageAssertions> parent,
#pragma warning restore 1573,
        string because = "", params object[] becauseArgs)
        => new HttpResponseMessageAssertions(parent.Subject).Be511NetworkAuthenticationRequired(because, becauseArgs);
}
#endif