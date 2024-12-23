using EstiPro.Api.IntegrationTests.Fixtures;

namespace EstiPro.Api.IntegrationTests.Controllers;

[CollectionDefinition(nameof(ControllerCollection))]
public class ControllerCollection : ICollectionFixture<WebApiFixture>;
