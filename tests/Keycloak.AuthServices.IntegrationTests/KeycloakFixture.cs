namespace Keycloak.AuthServices.IntegrationTests;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Testcontainers.Keycloak;

public class KeycloakFixture : IAsyncLifetime
{
    private const string AdminApiClientId = "admin-api";
    private const string AdminApiClientSecret = "k9LYTWKfbNOyfzFt2ZZsFl3Z4x4aAecf";

    public KeycloakContainer Keycloak { get; } =
        new KeycloakBuilder()
            .WithImage("quay.io/keycloak/keycloak:26.4.2")
            .WithBindMount(
                Path.Combine(Directory.GetCurrentDirectory(), "KeycloakConfiguration"),
                "/opt/keycloak/data/import/"
            )
            .WithCommand("--import-realm", "--verbose")
            .Build();

    public string BaseAddress => this.Keycloak.GetBaseAddress();
    public string ContainerId => $"{this.Keycloak.Id}";

    public async Task InitializeAsync()
    {
        await this.Keycloak.StartAsync();
        await this.CreateAdminApiClientAsync();
    }

    public Task DisposeAsync() => this.Keycloak.DisposeAsync().AsTask();

    private async Task CreateAdminApiClientAsync()
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri(this.BaseAddress) };

        var token = await this.GetAdminTokenAsync(httpClient);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

        var clientInternalId = await this.CreateClientAsync(httpClient);
        var serviceAccountUserId = await this.GetServiceAccountUserIdAsync(
            httpClient,
            clientInternalId
        );
        var adminRoleId = await this.GetAdminRoleIdAsync(httpClient);
        await this.AssignAdminRoleAsync(httpClient, serviceAccountUserId, adminRoleId);
    }

    private async Task<string> GetAdminTokenAsync(HttpClient httpClient)
    {
        var tokenRequest = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = "admin-cli",
                ["username"] = KeycloakBuilder.DefaultUsername,
                ["password"] = KeycloakBuilder.DefaultPassword,
            }
        );

        var response = await httpClient.PostAsync(
            "/realms/master/protocol/openid-connect/token",
            tokenRequest
        );
        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return tokenResponse!.AccessToken;
    }

    private async Task<string> CreateClientAsync(HttpClient httpClient)
    {
        var clientRepresentation = new
        {
            clientId = AdminApiClientId,
            name = "Admin API Client",
            enabled = true,
            clientAuthenticatorType = "client-secret",
            secret = AdminApiClientSecret,
            publicClient = false,
            serviceAccountsEnabled = true,
            standardFlowEnabled = false,
            directAccessGrantsEnabled = true,
            protocol = "openid-connect",
        };

        var response = await httpClient.PostAsJsonAsync(
            "/admin/realms/master/clients",
            clientRepresentation
        );
        response.EnsureSuccessStatusCode();

        var locationHeader = response.Headers.Location?.ToString();
        var clientInternalId =
            locationHeader?.Split('/').Last()
            ?? throw new InvalidOperationException("Failed to get client ID from Location header");

        return clientInternalId;
    }

    private async Task<string> GetServiceAccountUserIdAsync(
        HttpClient httpClient,
        string clientInternalId
    )
    {
        var response = await httpClient.GetAsync(
            $"/admin/realms/master/clients/{clientInternalId}/service-account-user"
        );
        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<UserResponse>();
        return user!.Id;
    }

    private async Task<string> GetAdminRoleIdAsync(HttpClient httpClient)
    {
        var response = await httpClient.GetAsync("/admin/realms/master/roles/admin");
        response.EnsureSuccessStatusCode();

        var role = await response.Content.ReadFromJsonAsync<RoleResponse>();
        return role!.Id;
    }

    private async Task AssignAdminRoleAsync(HttpClient httpClient, string userId, string roleId)
    {
        var roles = new[] { new { id = roleId, name = "admin" } };

        var response = await httpClient.PostAsJsonAsync(
            $"/admin/realms/master/users/{userId}/role-mappings/realm",
            roles
        );
        response.EnsureSuccessStatusCode();
    }

    private sealed record TokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken
    );

    private sealed record UserResponse([property: JsonPropertyName("id")] string Id);

    private sealed record RoleResponse([property: JsonPropertyName("id")] string Id);
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
