#if AAV
using AwesomeAssertions.Formatting;
using AwesomeAssertions.Primitives;
#else
using FluentAssertions.Formatting;
using FluentAssertions.Primitives;
#endif

#if AAV
namespace AwesomeAssertions.Web;
#else
namespace FluentAssertions.Web;
#endif

/// <summary>
/// Contains a number of methods to assert that an <see cref="HttpResponseMessage"/> is in the expected state.
/// </summary>
/// <remarks>
/// <para>
/// This is the assertion group returned by <c>response.Should()</c> in AwesomeAssertions and in FluentAssertions 8
/// and later. It is the starting point for status-code, content, header and <c>Satisfy</c> assertions.
/// </para>
/// <para>
/// Fluent assertion methods return an <c>AndConstraint&lt;TAssertions&gt;</c>, whose <c>And</c> property exposes the
/// assertion group that can be used next. This is what makes chains such as
/// <c>response.Should().Be200Ok().And.HaveHeader("Content-Type").And.Match("application/json*")</c> work.
/// Most status assertions return <see cref="HttpResponseMessageAssertions"/> again, so any general assertion can
/// follow. Some assertions move the chain to a more specialized group:
/// <see cref="Be400BadRequest"/> returns <see cref="BadRequestAssertions"/> for the JSON validation errors.
/// Some status assertions (for example <see cref="Be201Created"/>, <see cref="Be3XXRedirection"/>,
/// <see cref="Be204NoContent"/>, <see cref="Be409Conflict"/> and <see cref="Be410Gone"/>) return
/// <see cref="LocationAssertions"/>, which allows the chain to inspect the <c>Location</c> header; returning that
/// group does not assert that the header is present. <see cref="HaveHeader"/> returns a
/// <see cref="HeadersAssertions"/> bound to the header passed to it.
/// </para>
/// <para>
/// The specialized groups derive from this type, so general assertions remain available from their <c>And</c>
/// property. Start a new <c>response.Should()</c> call when the current chain cannot reach the group you need.
/// For example, <c>HaveLocation()</c> only exists on <see cref="LocationAssertions"/>, so it is not available from
/// a <see cref="BadRequestAssertions"/> chain; assert a location-bearing status first in a new chain. The same
/// applies when an assertion needs the whole <see cref="HttpResponseMessage"/> rather than the current group.
/// </para>
/// <para>
/// The <c>And</c> property and the fluid group transitions are AwesomeAssertions and FluentAssertions conventions.
/// The Shouldly integration exposes independent, flat assertions instead; see the Shouldly package documentation.
/// </para>
/// <example>
/// <code>
/// response.Should()
///     .Be400BadRequest()
///     .And.HaveError("Email", "*required*")
///     .And.HaveHeader("Content-Type").And.Match("application/json*");
/// </code>
/// </example>
/// </remarks>
/// <seealso cref="BadRequestAssertions"/>
/// <seealso cref="HeadersAssertions"/>
/// <seealso cref="LocationAssertions"/>
public partial class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
{
    static HttpResponseMessageAssertions()
    {
        Formatter.AddFormatter(new HttpResponseMessageFormatter());
        Formatter.AddFormatter(new AssertionsFailuresFormatter());
    }

#if FAV8
    /// <summary>
    /// Initialized a new instance of the <see cref="HttpResponseMessageAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
    /// <param name="assertionChain">The assertion chain to build and manage assertions.</param>
    public HttpResponseMessageAssertions(HttpResponseMessage value, AssertionChain assertionChain) : base(value, assertionChain)
#else
    /// <summary>
    /// Initialized a new instance of the <see cref="HttpResponseMessageAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
    public HttpResponseMessageAssertions(HttpResponseMessage value) : base(value)
#endif
    { }

    /// <summary>
    /// Returns the type of the subject the assertion applies on.
    /// </summary>
    protected override string Identifier => $"{nameof(HttpResponseMessage)}";

    private protected string? GetContent()
        => Subject!.ReadContentAsString();

    private protected (bool success, string? errorMessage) TryGetSubjectModel<TModel>(out TModel? model)
    {
        var (success, errorMessage) = TryGetSubjectModel(out var subjectModel, typeof(TModel));
        model = subjectModel is null ? default : (TModel)subjectModel;
        return (success, errorMessage);
    }

    private protected (bool success, string? errorMessage) TryGetSubjectModel(out object? model, Type modelType)
    {
        var (success, errorMessage, readModel) = Subject!.TryReadModel(modelType);
        model = readModel;
        return (success, errorMessage);
    }

    private string[] CollectFailuresFromAssertion<TAsserted>(Action<TAsserted> assertion, TAsserted subject)
    {
        using var collectionScope = new AssertionScope();
        string[] assertionFailures;
        using (var itemScope = new AssertionScope())
        {
            try
            {
                assertion(subject);
                assertionFailures = itemScope.Discard();
            }
            catch (Exception ex)
            {
                assertionFailures = new[] { $"Expected to successfully verify an assertion, but the following exception occurred: { ex }" };
            }

        }

        foreach (var assertionFailure in assertionFailures)
        {
            collectionScope.AddPreFormattedFailure($"{assertionFailure}");
        }

        return collectionScope.Discard();
    }
}