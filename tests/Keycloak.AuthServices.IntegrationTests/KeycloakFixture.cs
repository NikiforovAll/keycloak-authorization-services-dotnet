namespace Keycloak.AuthServices.IntegrationTests;

using Testcontainers.Keycloak;

public class KeycloakFixture : IAsyncLifetime
{
    public KeycloakContainer Keycloak { get; } =
        new KeycloakBuilder()
            .WithImage("quay.io/keycloak/keycloak:24.0.3")
            .WithBindMount(
                Path.Combine(Directory.GetCurrentDirectory(), "Test-realm.json"),
                "/opt/keycloak/data/import/Test-realm.json"
            )
            .WithBindMount(
                Path.Combine(Directory.GetCurrentDirectory(), "Test-users-0.json"),
                "/opt/keycloak/data/import/Test-users-0.json"
            )
            .WithCommand("--import-realm", "--verbose")
            .Build();

    public string BaseAddress => this.Keycloak.GetBaseAddress();
    public string ContainerId => $"{this.Keycloak.Id}";

    public Task InitializeAsync()
    {
        return this.Keycloak.StartAsync();
    }

    public Task DisposeAsync() => this.Keycloak.DisposeAsync().AsTask();
}

[CollectionDefinition(nameof(AuthenticationCollection))]
public class AuthenticationCollection : ICollectionFixture<KeycloakFixture>;

[Collection(nameof(AuthenticationCollection))]
public abstract class AuthenticationScenario(KeycloakFixture fixture)
{
    public KeycloakContainer Keycloak { get; } = fixture.Keycloak;
}

[CollectionDefinition(nameof(AuthenticationCollectionNoKeycloak))]
public class AuthenticationCollectionNoKeycloak;

[Collection(nameof(AuthenticationCollectionNoKeycloak))]
public abstract class AuthenticationScenarioNoKeycloak;
