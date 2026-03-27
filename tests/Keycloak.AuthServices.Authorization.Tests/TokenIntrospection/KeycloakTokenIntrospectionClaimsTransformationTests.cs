namespace Keycloak.AuthServices.Authorization.Tests.TokenIntrospection;

using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Authorization.TokenIntrospection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class KeycloakTokenIntrospectionClaimsTransformationTests
{
    private const string BearerToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.test-token";

    [Fact]
    public async Task TransformAsync_SkipsUnauthenticatedPrincipal()
    {
        var sut = CreateSut(out _);
        var principal = new ClaimsPrincipal(new ClaimsIdentity());

        var result = await sut.TransformAsync(principal);

        result.Should().BeSameAs(principal);
    }

    [Fact]
    public async Task TransformAsync_SkipsWhenResourceAccessPresent()
    {
        var sut = CreateSut(out _);
        var identity = new ClaimsIdentity("Bearer");
        identity.AddClaim(new Claim("resource_access", "{}", "JSON"));
        var principal = new ClaimsPrincipal(identity);

        var result = await sut.TransformAsync(principal);

        result.Should().BeSameAs(principal);
    }

    [Fact]
    public async Task TransformAsync_SkipsWhenRealmAccessPresent()
    {
        var sut = CreateSut(out _);
        var identity = new ClaimsIdentity("Bearer");
        identity.AddClaim(new Claim("realm_access", "{}", "JSON"));
        var principal = new ClaimsPrincipal(identity);

        var result = await sut.TransformAsync(principal);

        result.Should().BeSameAs(principal);
    }

    [Fact]
    public async Task TransformAsync_SkipsWhenNoHttpContext()
    {
        var sut = CreateSut(out _, httpContext: null);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        result.Should().BeSameAs(principal);
    }

    [Fact]
    public async Task TransformAsync_SkipsWhenNoBearerToken()
    {
        var httpContext = new DefaultHttpContext();
        var sut = CreateSut(out _, httpContext: httpContext);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        result.Should().BeSameAs(principal);
    }

    [Fact]
    public async Task TransformAsync_SkipsWhenTokenNotActive()
    {
        var httpContext = CreateHttpContextWithBearer();
        var client = new FakeIntrospectionClient(new TokenIntrospectionResponse { Active = false });
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        result.Identity!.As<ClaimsIdentity>().FindFirst("preferred_username").Should().BeNull();
    }

    [Fact]
    public async Task TransformAsync_EnrichesWithScalarClaims()
    {
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["preferred_username"] = JsonDocument.Parse("\"admin\"").RootElement,
            ["email"] = JsonDocument.Parse("\"admin@test.com\"").RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        var identity = result.Identity as ClaimsIdentity;
        identity!.FindFirst("preferred_username")!.Value.Should().Be("admin");
        identity.FindFirst("email")!.Value.Should().Be("admin@test.com");
    }

    [Fact]
    public async Task TransformAsync_EnrichesWithJsonClaims()
    {
        var realmAccess = /*lang=json,strict*/
            """{"roles":["Admin","user"]}""";
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["realm_access"] = JsonDocument.Parse(realmAccess).RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        var identity = result.Identity as ClaimsIdentity;
        var claim = identity!.FindFirst("realm_access");
        claim.Should().NotBeNull();
        claim!.ValueType.Should().Be("JSON");
        claim.Value.Should().Contain("Admin");
    }

    [Fact]
    public async Task TransformAsync_EnrichesWithArrayClaims()
    {
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["groups"] = JsonDocument.Parse("""["/admins","/users"]""").RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        var identity = result.Identity as ClaimsIdentity;
        identity!.FindAll("groups").Should().HaveCount(2);
        identity.FindAll("groups").Select(c => c.Value).Should().Contain("/admins");
    }

    [Fact]
    public async Task TransformAsync_SkipsInfrastructureClaims()
    {
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["iss"] = JsonDocument.Parse("\"http://kc/realms/test\"").RootElement,
            ["sub"] = JsonDocument.Parse("\"user-id\"").RootElement,
            ["exp"] = JsonDocument.Parse("9999999999").RootElement,
            ["custom_claim"] = JsonDocument.Parse("\"value\"").RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        var identity = result.Identity as ClaimsIdentity;
        identity!.FindFirst("custom_claim")!.Value.Should().Be("value");
        // iss, sub, exp should not be duplicated (they're in skip list)
        identity.FindAll("iss").Should().BeEmpty();
        identity.FindAll("exp").Should().BeEmpty();
    }

    [Fact]
    public async Task TransformAsync_DoesNotDuplicateExistingClaims()
    {
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["existing_claim"] = JsonDocument.Parse("\"new_value\"").RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var identity = new ClaimsIdentity("Bearer");
        identity.AddClaim(new Claim("sub", "user-123"));
        identity.AddClaim(new Claim("existing_claim", "original_value"));
        var principal = new ClaimsPrincipal(identity);

        var result = await sut.TransformAsync(principal);

        var resultIdentity = result.Identity as ClaimsIdentity;
        resultIdentity!.FindAll("existing_claim").Should().HaveCount(1);
        resultIdentity.FindFirst("existing_claim")!.Value.Should().Be("original_value");
    }

    [Fact]
    public async Task TransformAsync_InvokesOnTokenIntrospectedCallback()
    {
        var callbackInvoked = false;
        var claims = new Dictionary<string, JsonElement>
        {
            ["active"] = JsonDocument.Parse("true").RootElement,
            ["department"] = JsonDocument.Parse("\"engineering\"").RootElement,
        };
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = claims }
        );
        var httpContext = CreateHttpContextWithBearer();
        var options = new KeycloakTokenIntrospectionOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = "test",
            Resource = "client",
            Credentials = new() { Secret = "secret" },
            OnTokenIntrospected = (identity, rawClaims) =>
            {
                callbackInvoked = true;
                identity.AddClaim(new Claim("callback_marker", "true"));
            },
        };
        var sut = CreateSut(out _, httpContext: httpContext, client: client, options: options);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        callbackInvoked.Should().BeTrue();
        (result.Identity as ClaimsIdentity)!
            .FindFirst("callback_marker")!
            .Value.Should()
            .Be("true");
    }

    [Fact]
    public async Task TransformAsync_UsesJtiForCacheKey()
    {
        var client = new FakeIntrospectionClient(
            new TokenIntrospectionResponse
            {
                Active = true,
                Claims = new()
                {
                    ["active"] = JsonDocument.Parse("true").RootElement,
                    ["custom"] = JsonDocument.Parse("\"cached\"").RootElement,
                },
            }
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);

        var identity = new ClaimsIdentity("Bearer");
        identity.AddClaim(new Claim("sub", "user-123"));
        identity.AddClaim(new Claim("jti", "unique-token-id"));
        var principal = new ClaimsPrincipal(identity);

        // Call twice — second call should hit cache
        await sut.TransformAsync(principal);

        var identity2 = new ClaimsIdentity("Bearer");
        identity2.AddClaim(new Claim("sub", "user-123"));
        identity2.AddClaim(new Claim("jti", "unique-token-id"));
        var principal2 = new ClaimsPrincipal(identity2);
        await sut.TransformAsync(principal2);

        client.CallCount.Should().Be(1);
    }

    [Fact]
    public async Task TransformAsync_HandlesIntrospectionFailureGracefully()
    {
        var client = new FakeIntrospectionClient(
            exception: new HttpRequestException("Connection refused")
        );
        var httpContext = CreateHttpContextWithBearer();
        var sut = CreateSut(out _, httpContext: httpContext, client: client);
        var principal = CreateLightweightPrincipal();

        var result = await sut.TransformAsync(principal);

        // Should not throw, just return principal unchanged
        result.Should().BeSameAs(principal);
    }

    #region Helpers

    private static ClaimsPrincipal CreateLightweightPrincipal()
    {
        var identity = new ClaimsIdentity("Bearer");
        identity.AddClaim(new Claim("sub", "user-123"));
        return new ClaimsPrincipal(identity);
    }

    private static DefaultHttpContext CreateHttpContextWithBearer()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers.Authorization = $"Bearer {BearerToken}";
        return httpContext;
    }

    private static KeycloakTokenIntrospectionClaimsTransformation CreateSut(
        out ServiceProvider serviceProvider,
        DefaultHttpContext? httpContext = null,
        IKeycloakTokenIntrospectionClient? client = null,
        KeycloakTokenIntrospectionOptions? options = null
    )
    {
        httpContext ??= CreateHttpContextWithBearer();
        client ??= new FakeIntrospectionClient(
            new TokenIntrospectionResponse { Active = true, Claims = new() }
        );
        options ??= new KeycloakTokenIntrospectionOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = "test",
            Resource = "client",
            Credentials = new() { Secret = "secret" },
        };

        var httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };

        var services = new ServiceCollection();
        services.AddHybridCache();
        serviceProvider = services.BuildServiceProvider();

        return new KeycloakTokenIntrospectionClaimsTransformation(
            client,
            serviceProvider.GetRequiredService<HybridCache>(),
            httpContextAccessor,
            Options.Create(options),
            NullLogger<KeycloakTokenIntrospectionClaimsTransformation>.Instance
        );
    }

    private sealed class FakeIntrospectionClient : IKeycloakTokenIntrospectionClient
    {
        private readonly TokenIntrospectionResponse? response;
        private readonly Exception? exception;

        public int CallCount { get; private set; }

        public FakeIntrospectionClient(
            TokenIntrospectionResponse? response = null,
            Exception? exception = null
        )
        {
            this.response = response;
            this.exception = exception;
        }

        public Task<TokenIntrospectionResponse?> IntrospectTokenAsync(
            string token,
            CancellationToken cancellationToken = default
        )
        {
            this.CallCount++;
            if (this.exception is not null)
            {
                throw this.exception;
            }

            return Task.FromResult(this.response);
        }
    }

    #endregion
}
