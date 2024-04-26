namespace Keycloak.AuthServices.IntegrationTests;

using Testcontainers.Keycloak;

public class KeycloakFixture : IAsyncLifetime
{
    public KeycloakContainer Keycloak { get; } = new KeycloakBuilder()
        .WithImage("quay.io/keycloak/keycloak:24.0.3")
        .Build();

    public string BaseAddress => this.Keycloak.GetBaseAddress();
    public string ContainerId => $"{this.Keycloak.Id}";

    public Task InitializeAsync() => this.Keycloak.StartAsync();

    public Task DisposeAsync() => this.Keycloak.DisposeAsync().AsTask();
}

[CollectionDefinition(nameof(AuthenticationCollection))]
public class AuthenticationCollection : ICollectionFixture<KeycloakFixture>;

[Collection(nameof(AuthenticationCollection))]
public abstract class AuthenticationScenario(KeycloakFixture fixture)
{
    public KeycloakContainer Keycloak { get; } = fixture.Keycloak;
}
