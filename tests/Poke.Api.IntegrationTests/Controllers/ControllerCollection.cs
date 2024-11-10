using Poke.Api.IntegrationTests.Fixtures;

namespace Poke.Api.IntegrationTests.Controllers;

[CollectionDefinition(nameof(ControllerCollection))]
public class ControllerCollection : ICollectionFixture<WebApiFixture>;
