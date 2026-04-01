namespace Keycloak.AuthServices.Authentication.Tests;

using System.Text;
using Keycloak.AuthServices.Authentication.Configuration;

public class KeycloakConfigurationProviderTests
{
    private static KeycloakConfigurationProvider CreateProvider(
        string jsonString,
        string prefix = "Keycloak"
    )
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        var provider = new KeycloakConfigurationProvider(new KeycloakConfigurationSource(), prefix);
        provider.Load(stream);
        return provider;
    }

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

        var provider = CreateProvider(jsonString);

        provider.TryGet("Keycloak:realm", out var realm);
        provider.TryGet("Keycloak:credentials:secret", out var secret);

        realm.Should().Be("Test");
        secret.Should().Be("secret-value");
    }

    [Fact]
    public void Load_KebabCaseKey_NormalizesToPascalCase()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""auth-server-url"": ""http://localhost:8080/"",
    ""ssl-required"": ""none"",
    ""verify-token-audience"": true
}";

        var provider = CreateProvider(jsonString);

        provider.TryGet("Keycloak:AuthServerUrl", out var authServerUrl);
        provider.TryGet("Keycloak:SslRequired", out var sslRequired);
        provider.TryGet("Keycloak:VerifyTokenAudience", out var verifyTokenAudience);

        authServerUrl.Should().Be("http://localhost:8080/");
        sslRequired.Should().Be("none");
        verifyTokenAudience.Should().Be("True");
    }

    [Fact]
    public void Load_NestedObject_NormalizesNestedKey()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""credentials"": {
        ""secret"": ""my-secret-value""
    }
}";

        var provider = CreateProvider(jsonString);

        provider.TryGet("Keycloak:Credentials:Secret", out var secret);

        secret.Should().Be("my-secret-value");
    }

    [Fact]
    public void Load_CustomConfigurationPrefix_UsesGivenPrefix()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""realm"": ""my-realm""
}";

        var provider = CreateProvider(jsonString, prefix: "KeycloakCustom");

        provider.TryGet("KeycloakCustom:realm", out var realm);

        realm.Should().Be("my-realm");
    }

    [Fact]
    public void Load_StandardKeycloakInstallationJson_AllPropertiesAccessible()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""realm"": ""Test"",
    ""auth-server-url"": ""http://localhost:8080/"",
    ""ssl-required"": ""external"",
    ""resource"": ""test-client"",
    ""verify-token-audience"": true,
    ""credentials"": {
        ""secret"": ""s3cr3t""
    },
    ""confidential-port"": 0
}";

        var provider = CreateProvider(jsonString);

        provider.TryGet("Keycloak:Realm", out var realm);
        provider.TryGet("Keycloak:AuthServerUrl", out var authServerUrl);
        provider.TryGet("Keycloak:SslRequired", out var sslRequired);
        provider.TryGet("Keycloak:Resource", out var resource);
        provider.TryGet("Keycloak:VerifyTokenAudience", out var verifyTokenAudience);
        provider.TryGet("Keycloak:Credentials:Secret", out var secret);
        provider.TryGet("Keycloak:ConfidentialPort", out var confidentialPort);

        realm.Should().Be("Test");
        authServerUrl.Should().Be("http://localhost:8080/");
        sslRequired.Should().Be("external");
        resource.Should().Be("test-client");
        verifyTokenAudience.Should().Be("True");
        secret.Should().Be("s3cr3t");
        confidentialPort.Should().Be("0");
    }

    [Fact]
    public void Load_LookupIsCaseInsensitive()
    {
        var jsonString =
            /*lang=json,strict*/
            @"{
    ""auth-server-url"": ""http://localhost:8080/""
}";

        var provider = CreateProvider(jsonString);

        provider.TryGet("keycloak:authserverurl", out var lower);
        provider.TryGet("KEYCLOAK:AUTHSERVERURL", out var upper);
        provider.TryGet("Keycloak:AuthServerUrl", out var pascal);

        lower.Should().Be("http://localhost:8080/");
        upper.Should().Be("http://localhost:8080/");
        pascal.Should().Be("http://localhost:8080/");
    }
}
