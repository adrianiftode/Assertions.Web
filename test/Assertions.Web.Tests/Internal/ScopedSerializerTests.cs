namespace Assertions.Web.Tests.Internal;

[Collection("Assertions Web Config Tests")]
public class ScopedSerializerTests
{
    private sealed class MarkerSerializer : ISerializer
    {
        public Task<object?> Deserialize(Stream content, Type modelType) => Task.FromResult<object?>(null);
    }

    [Fact]
    public void Given_a_scope_Then_the_serializer_is_the_scoped_one_until_disposed()
    {
        var global = AssertionsWebConfig.Serializer;
        var scoped = new MarkerSerializer();

        using (AssertionsWebConfig.UseSerializer(scoped))
        {
            AssertionsWebConfig.Serializer.Should().BeSameAs(scoped);
        }

        AssertionsWebConfig.Serializer.Should().BeSameAs(global);
    }

    [Fact]
    public void Given_a_scope_Then_setting_the_serializer_changes_the_global_one_only()
    {
        var global = AssertionsWebConfig.Serializer;
        var scoped = new MarkerSerializer();
        var newGlobal = new MarkerSerializer();

        try
        {
            using (AssertionsWebConfig.UseSerializer(scoped))
            {
                AssertionsWebConfig.Serializer = newGlobal;

                AssertionsWebConfig.Serializer.Should().BeSameAs(scoped);
            }

            AssertionsWebConfig.Serializer.Should().BeSameAs(newGlobal);
        }
        finally
        {
            AssertionsWebConfig.Serializer = global;
        }
    }

    [Fact]
    public void Given_nested_scopes_Then_disposing_restores_the_previous_scoped_serializer()
    {
        var outer = new MarkerSerializer();
        var inner = new MarkerSerializer();

        using (AssertionsWebConfig.UseSerializer(outer))
        {
            using (AssertionsWebConfig.UseSerializer(inner))
            {
                AssertionsWebConfig.Serializer.Should().BeSameAs(inner);
            }

            AssertionsWebConfig.Serializer.Should().BeSameAs(outer);
        }
    }

    [Fact]
    public async Task Given_a_scope_Then_it_flows_across_await()
    {
        var scoped = new MarkerSerializer();

        using (AssertionsWebConfig.UseSerializer(scoped))
        {
            await Task.Delay(10);
            await Task.Yield();

            AssertionsWebConfig.Serializer.Should().BeSameAs(scoped);
        }
    }

    [Fact]
    public async Task Given_a_scope_Then_it_does_not_leak_to_a_parallel_async_flow()
    {
        var global = AssertionsWebConfig.Serializer;
        var scoped = new MarkerSerializer();
        var scopeEntered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var parallelChecked = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        var scopedFlow = Task.Run(async () =>
        {
            using (AssertionsWebConfig.UseSerializer(scoped))
            {
                scopeEntered.SetResult(true);
                await parallelChecked.Task;
                return AssertionsWebConfig.Serializer;
            }
        });

        var parallelFlow = Task.Run(async () =>
        {
            await scopeEntered.Task;
            var seen = AssertionsWebConfig.Serializer;
            parallelChecked.SetResult(true);
            return seen;
        });

        (await parallelFlow).Should().BeSameAs(global);
        (await scopedFlow).Should().BeSameAs(scoped);
    }

    [Fact]
    public void Given_a_null_serializer_Then_using_it_throws_ArgumentNullException()
    {
        Action act = () => AssertionsWebConfig.UseSerializer(null!);

        act.Should().Throw<ArgumentNullException>().WithMessage("*<null> serializer*");
    }

    [Fact]
    public void Given_a_scope_Then_reading_a_response_model_uses_the_scoped_serializer()
    {
        using var response = new HttpResponseMessage { Content = new StringContent("""{ "first_name" : "Ada" }""") };
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        using (AssertionsWebConfig.UseSerializer(new SystemTextJsonSerializer(options)))
        {
            var (success, _, model) = response.TryReadModel(typeof(Person));

            success.Should().BeTrue();
            model.Should().BeOfType<Person>().Which.FirstName.Should().Be("Ada");
        }
    }

    private sealed class Person
    {
        public string? FirstName { get; set; }
    }
}
