namespace Keycloak.AuthServices.Authentication.Tests;

using System.Text;
using Keycloak.AuthServices.Authentication.Configuration;

public class KeycloakConfigurationProviderTests
{
    [Fact]
    public void Load_WithSomePayload_Success()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""realm"": ""Test"",
    ""auth-server-url"": ""http://localhost:8088/auth/"",
    ""ssl-required"": ""external"",
    ""resource"": ""example-client"",
    ""verify-token-audience"": true,
    ""credentials"": {
        ""secret"": ""secret-value""
    }
}";

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        var provider = new KeycloakConfigurationProvider(
            new KeycloakConfigurationSource(),
            "Keycloak"
        );

        provider.Load(stream);

        provider.TryGet("Keycloak:realm", out var realm);
        provider.TryGet("Keycloak:credentials:secret", out var secret);

        realm.Should().Be("Test");
        secret.Should().Be("secret-value");
    }
}
