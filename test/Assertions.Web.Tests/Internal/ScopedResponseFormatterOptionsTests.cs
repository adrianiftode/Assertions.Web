namespace Assertions.Web.Tests.Internal;

using System.Net;

[Collection("Assertions Web Config Tests")]
public class ScopedResponseFormatterOptionsTests
{
    [Fact]
    public void Given_a_scope_Then_the_response_formatter_options_are_the_scoped_ones_until_disposed()
    {
        var global = AssertionsWebConfig.ResponseFormatterOptions;
        var scoped = new HttpResponseFormatterOptions();

        using (AssertionsWebConfig.UseResponseFormatterOptions(scoped))
        {
            AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(scoped);
        }

        AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(global);
    }

    [Fact]
    public void Given_a_scope_Then_setting_the_response_formatter_options_changes_the_global_ones_only()
    {
        var global = AssertionsWebConfig.ResponseFormatterOptions;
        var scoped = new HttpResponseFormatterOptions();
        var newGlobal = new HttpResponseFormatterOptions();

        try
        {
            using (AssertionsWebConfig.UseResponseFormatterOptions(scoped))
            {
                AssertionsWebConfig.ResponseFormatterOptions = newGlobal;

                AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(scoped);
            }

            AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(newGlobal);
        }
        finally
        {
            AssertionsWebConfig.ResponseFormatterOptions = global;
        }
    }

    [Fact]
    public void Given_nested_scopes_Then_disposing_restores_the_previous_scoped_response_formatter_options()
    {
        var outer = new HttpResponseFormatterOptions();
        var inner = new HttpResponseFormatterOptions();

        using (AssertionsWebConfig.UseResponseFormatterOptions(outer))
        {
            using (AssertionsWebConfig.UseResponseFormatterOptions(inner))
            {
                AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(inner);
            }

            AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(outer);
        }
    }

    [Fact]
    public async Task Given_a_scope_Then_it_flows_across_await()
    {
        var scoped = new HttpResponseFormatterOptions();

        using (AssertionsWebConfig.UseResponseFormatterOptions(scoped))
        {
            await Task.Delay(10);
            await Task.Yield();

            AssertionsWebConfig.ResponseFormatterOptions.Should().BeSameAs(scoped);
        }
    }

    [Fact]
    public async Task Given_a_scope_Then_it_does_not_leak_to_a_parallel_async_flow()
    {
        var global = AssertionsWebConfig.ResponseFormatterOptions;
        var scoped = new HttpResponseFormatterOptions();
        var scopeEntered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var parallelChecked = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var scopedFlow = Task.Run(async () =>
        {
            using (AssertionsWebConfig.UseResponseFormatterOptions(scoped))
            {
                scopeEntered.SetResult(true);
                await parallelChecked.Task;
                return AssertionsWebConfig.ResponseFormatterOptions;
            }
        });

        var parallelFlow = Task.Run(async () =>
        {
            await scopeEntered.Task;
            var seen = AssertionsWebConfig.ResponseFormatterOptions;
            parallelChecked.SetResult(true);
            return seen;
        });

        (await parallelFlow).Should().BeSameAs(global);
        (await scopedFlow).Should().BeSameAs(scoped);
    }

    [Fact]
    public void Given_null_response_formatter_options_Then_using_it_throws_ArgumentNullException()
    {
        Action act = () => AssertionsWebConfig.UseResponseFormatterOptions(null!);

        act.Should().Throw<ArgumentNullException>().WithMessage("*<null> response formatter options*");
    }

    [Fact]
    public void Given_a_scope_Then_formatting_a_response_uses_the_scoped_options()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("""{ "stackTrace" : "at SomeMethod()\r\nat OtherMethod()" }""")
        };
        var scoped = new HttpResponseFormatterOptions
        {
            PostFormat = formatted => formatted.Replace("\\r\\n", Environment.NewLine)
        };

        using (AssertionsWebConfig.UseResponseFormatterOptions(scoped))
        {
            var formatted = response.Format(AssertionsWebConfig.ResponseFormatterOptions);

            formatted.Should().Contain($"at SomeMethod(){Environment.NewLine}at OtherMethod()");
        }

        var formattedOutsideTheScope = response.Format(AssertionsWebConfig.ResponseFormatterOptions);
        formattedOutsideTheScope.Should().Contain("\\r\\n");
    }
}
