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
public partial class HttpResponseMessageAssertions : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
{
    static HttpResponseMessageAssertions()
    {
        Formatter.AddFormatter(new HttpResponseMessageFormatter());
        Formatter.AddFormatter(new AssertionsFailuresFormatter());
    }

    /// <summary>
    /// Initialized a new instance of the <see cref="HttpResponseMessageAssertions"/>
    /// class.
    /// </summary>
    /// <param name="value">The subject value to be asserted.</param>
#if FAV8
    /// <param name="assertionChain">The assertion chain to build and manage assertions.</param>
    public HttpResponseMessageAssertions(HttpResponseMessage value, AssertionChain assertionChain) : base(value, assertionChain)
#else
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