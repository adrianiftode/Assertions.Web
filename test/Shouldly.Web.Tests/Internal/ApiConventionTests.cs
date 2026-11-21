using System.Reflection;

namespace Shouldly.Web.Tests.Internal;

/// <summary>
/// Conventions §6: the shape of everything this assembly exports. The expected-name list is
/// checked in: a new master assertion must be added here deliberately. Later tasks append
/// their own names.
/// </summary>
public class ApiConventionTests
{
    private static readonly Assembly ShouldlyWebAssembly = typeof(HttpStatusCodeShouldlyExtensions).Assembly;

    private static readonly string[] ExpectedNames =
    {
        "ShouldBe1XXInformational",
        "ShouldBe2XXSuccessful",
        "ShouldBe3XXRedirection",
        "ShouldBe4XXClientError",
        "ShouldBe5XXServerError",
        "ShouldHaveHttpStatusCode",
        "ShouldNotHaveHttpStatusCode",
        "ShouldBe100Continue",
        "ShouldBe101SwitchingProtocols",
        "ShouldBe200Ok",
        "ShouldBe201Created",
        "ShouldBe202Accepted",
        "ShouldBe203NonAuthoritativeInformation",
        "ShouldBe204NoContent",
        "ShouldBe205ResetContent",
        "ShouldBe206PartialContent",
        "ShouldBe300MultipleChoices",
        "ShouldBe300Ambiguous",
        "ShouldBe301MovedPermanently",
        "ShouldBe301Moved",
        "ShouldBe302Found",
        "ShouldBe302Redirect",
        "ShouldBe303SeeOther",
        "ShouldBe303RedirectMethod",
        "ShouldBe304NotModified",
        "ShouldBe305UseProxy",
        "ShouldBe306Unused",
        "ShouldBe307TemporaryRedirect",
        "ShouldBe307RedirectKeepVerb",
        "ShouldBe308PermanentRedirect",
        "ShouldBe400BadRequest",
        "ShouldBe401Unauthorized",
        "ShouldBe402PaymentRequired",
        "ShouldBe403Forbidden",
        "ShouldBe404NotFound",
        "ShouldBe405MethodNotAllowed",
        "ShouldBe406NotAcceptable",
        "ShouldBe407ProxyAuthenticationRequired",
        "ShouldBe408RequestTimeout",
        "ShouldBe409Conflict",
        "ShouldBe410Gone",
        "ShouldBe411LengthRequired",
        "ShouldBe412PreconditionFailed",
        "ShouldBe413RequestEntityTooLarge",
        "ShouldBe414RequestUriTooLong",
        "ShouldBe415UnsupportedMediaType",
        "ShouldBe416RequestedRangeNotSatisfiable",
        "ShouldBe417ExpectationFailed",
        "ShouldBe418ImATeapot",
        "ShouldBe422UnprocessableEntity",
        "ShouldBe429TooManyRequests",
        "ShouldBe426UpgradeRequired",
        "ShouldBe500InternalServerError",
        "ShouldBe501NotImplemented",
        "ShouldBe502BadGateway",
        "ShouldBe503ServiceUnavailable",
        "ShouldBe504GatewayTimeout",
        "ShouldBe505HttpVersionNotSupported",
        "ShouldBe102Processing",
        "ShouldBe103EarlyHints",
        "ShouldBe207MultiStatus",
        "ShouldBe208AlreadyReported",
        "ShouldBe226IMUsed",
        "ShouldBe421MisdirectedRequest",
        "ShouldBe423Locked",
        "ShouldBe424FailedDependency",
        "ShouldBe425TooEarly",
        "ShouldBe428PreconditionRequired",
        "ShouldBe431RequestHeaderFieldsTooLarge",
        "ShouldBe451UnavailableForLegalReasons",
        "ShouldBe506VariantAlsoNegotiates",
        "ShouldBe507InsufficientStorage",
        "ShouldBe508LoopDetected",
        "ShouldBe510NotExtended",
        "ShouldBe511NetworkAuthenticationRequired",
        "ShouldBeAs",
        "ShouldBeAs",
        "ShouldBeEmpty",
        "ShouldHaveEmptyHeader",
        "ShouldHaveError",
        "ShouldHaveErrorMessage",
        "ShouldHaveHeader",
        "ShouldHaveHeaderMatching",
        "ShouldHaveHeaderWithValue",
        "ShouldHaveHeaderWithValues",
        "ShouldHaveLocation",
        "ShouldHaveLocationMatching",
        "ShouldHaveLocationWithValue",
        "ShouldHaveLocationWithValues",
        "ShouldHaveNonEmptyHeader",
        "ShouldMatchInContent",
        "ShouldNotHaveError",
        "ShouldNotHaveHeader",
        "ShouldNotHaveLocation",
        "ShouldOnlyHaveError",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy",
        "ShouldSatisfy"
    };

    private static IEnumerable<Type> ExportedTypes =>
        ShouldlyWebAssembly.GetExportedTypes();

    private static IEnumerable<MethodInfo> PublicMethods =>
        ExportedTypes.SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly));

    [Fact]
    public void Every_exported_type_is_a_public_static_class_in_Shouldly_with_all_three_class_attributes()
    {
        ExportedTypes.ShouldAllBe(type => type.Namespace == "Shouldly");
        ExportedTypes.ShouldAllBe(type => type.IsClass && type.IsPublic && type.IsAbstract && type.IsSealed);
        ExportedTypes.ShouldAllBe(type => HasAttribute(type, "Shouldly.ShouldlyMethodsAttribute"));
        ExportedTypes.ShouldAllBe(type => HasAttribute(type, "System.Diagnostics.DebuggerStepThroughAttribute"));
        ExportedTypes.ShouldAllBe(type => HasAttribute(type, "System.ComponentModel.EditorBrowsableAttribute"));
    }

    [Fact]
    public void Every_public_method_follows_the_method_shape()
    {
        foreach (var method in PublicMethods)
        {
            method.Name.ShouldStartWith("Should");
            method.ReturnType.ShouldBe(typeof(void));

            HasAttribute(method, "System.Runtime.CompilerServices.ExtensionAttribute")
                .ShouldBeTrue($"{method.DeclaringType!.Name}.{method.Name} is not an extension method");

            var parameters = method.GetParameters();
            (parameters.Length >= 2).ShouldBeTrue($"{method.DeclaringType!.Name}.{method.Name} has fewer than two parameters");
            parameters[0].Name.ShouldBe("actual");
            parameters[0].ParameterType.ShouldBe(typeof(HttpResponseMessage));

            var customMessage = parameters[^2];
            customMessage.Name.ShouldBe("customMessage");
            customMessage.ParameterType.ShouldBe(typeof(string));
            customMessage.HasDefaultValue.ShouldBeTrue();
            customMessage.DefaultValue.ShouldBeNull();

            var actualExpression = parameters[^1];
            actualExpression.Name.ShouldBe("actualExpression");
            actualExpression.ParameterType.ShouldBe(typeof(string));
            actualExpression.HasDefaultValue.ShouldBeTrue();
            actualExpression.DefaultValue.ShouldBeNull();

            var callerArgumentExpression = actualExpression.GetCustomAttributesData()
                .SingleOrDefault(data => data.AttributeType.FullName ==
                    "System.Runtime.CompilerServices.CallerArgumentExpressionAttribute");
            callerArgumentExpression.ShouldNotBeNull(
                $"{method.DeclaringType!.Name}.{method.Name} misses CallerArgumentExpression");
            callerArgumentExpression!.ConstructorArguments[0].Value.ShouldBe("actual");
        }
    }

    [Fact]
    public void The_public_method_names_equal_the_expected_list()
    {
        var actualNames = PublicMethods.Select(method => method.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
        var expectedNames = ExpectedNames
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        actualNames.ShouldBe(expectedNames);
    }

    [Fact]
    public void No_type_in_an_Internal_namespace_is_public()
    {
        var internalTypes = ShouldlyWebAssembly.GetTypes()
            .Where(type => type.Namespace?.Split('.').Contains("Internal") == true)
            .ToArray();

        internalTypes.ShouldNotBeEmpty();
        internalTypes.ShouldAllBe(type => !type.IsVisible);
    }

    private static bool HasAttribute(MemberInfo member, string attributeTypeFullName) =>
        member.GetCustomAttributesData().Any(data => data.AttributeType.FullName == attributeTypeFullName);
}
