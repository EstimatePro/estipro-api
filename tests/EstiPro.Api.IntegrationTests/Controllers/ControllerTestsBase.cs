using EstiPro.Api.IntegrationTests.Factories;
using EstiPro.Api.IntegrationTests.Fixtures;
using Xunit.Abstractions;

namespace EstiPro.Api.IntegrationTests.Controllers;

[Collection(nameof(ControllerCollection))]
public abstract class ControllerTestsBase : IAsyncLifetime
{
    protected ControllerTestsBase(ITestOutputHelper testOutputHelper, WebApiFixture webApiFixture)
    {
        WebApiFixture = webApiFixture;
        webApiFixture.Configure(testOutputHelper);
    }

    protected WebApiFixture WebApiFixture { get; }

    private bool ClearBeforeEachRun => true;

    public virtual async Task InitializeAsync()
    {
        if (ClearBeforeEachRun)
        {
            await TruncateDBsAsync();
        }
    }

    public virtual Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private async Task TruncateDBsAsync()
    {
        await SqlDatabaseHelper.Truncate(WebApiFixture);
    }
}
