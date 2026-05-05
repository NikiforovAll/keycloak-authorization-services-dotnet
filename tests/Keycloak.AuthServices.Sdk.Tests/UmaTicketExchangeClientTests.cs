namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;

public class UmaTicketExchangeClientTests
{
    private const string AuthServerUrl = "http://localhost:8080/";
    private const string Realm = "TestRealm";
    private const string ClientId = "my-client";
    private const string TokenEndpoint =
        $"{AuthServerUrl}realms/{Realm}/protocol/openid-connect/token";

    private readonly MockHttpMessageHandler handler = new();
    private readonly UmaTicketExchangeClient sut;

    public UmaTicketExchangeClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        var options = Options.Create(
            new KeycloakProtectionClientOptions
            {
                AuthServerUrl = AuthServerUrl,
                Realm = Realm,
                Resource = ClientId,
            }
        );
        this.sut = new UmaTicketExchangeClient(
            httpClient,
            options,
            NullLogger<UmaTicketExchangeClient>.Instance
        );
    }

    [Fact]
    public async Task ExchangeTicketForRptAsync_Success_ReturnsRpt()
    {
        const string rpt = "rpt-access-token-xyz";
        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .Respond(HttpStatusCode.OK, "application/json", $$"""{"access_token":"{{rpt}}"}""");

        var result = await this.sut.ExchangeTicketForRptAsync("my-access-token", "my-ticket");

        result.Should().Be(rpt);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task ExchangeTicketForRptAsync_Failure_ReturnsNull()
    {
        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .Respond(
                HttpStatusCode.Forbidden,
                "application/json",
                """{"error":"access_denied","error_description":"not_authorized"}"""
            );

        var result = await this.sut.ExchangeTicketForRptAsync("my-access-token", "my-ticket");

        result.Should().BeNull();
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task ExchangeTicketForRptAsync_SendsCorrectFormParameters()
    {
        const string ticket = "permission-ticket-abc";
        string? capturedBody = null;

        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .With(req =>
            {
                capturedBody = req.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", """{"access_token":"rpt-token"}""");

        await this.sut.ExchangeTicketForRptAsync("bearer-token", ticket);

        capturedBody
            .Should()
            .Contain("grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-type%3Auma-ticket");
        capturedBody.Should().Contain($"ticket={ticket}");
        capturedBody.Should().Contain($"audience={ClientId}");
    }

    [Fact]
    public async Task ExchangeTicketForRptAsync_SendsBearerAuthorizationHeader()
    {
        const string accessToken = "user-access-token";
        string? capturedAuth = null;

        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .With(req =>
            {
                capturedAuth = req.Headers.Authorization?.ToString();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", """{"access_token":"rpt-token"}""");

        await this.sut.ExchangeTicketForRptAsync(accessToken, "ticket");

        capturedAuth.Should().Be($"Bearer {accessToken}");
    }

    [Fact]
    public async Task SubmitPermissionRequestAsync_SuccessfulRpt_ReturnsFalse()
    {
        // 200 OK means we got an RPT directly — no owner-approval request was needed
        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .Respond(HttpStatusCode.OK, "application/json", """{"access_token":"rpt-token"}""");

        var result = await this.sut.SubmitPermissionRequestAsync("bearer-token", "ticket");

        result.Should().BeFalse();
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SubmitPermissionRequestAsync_RequestSubmitted_ReturnsTrue()
    {
        // 403 with error=request_submitted means the permission request was queued for owner approval
        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .Respond(
                HttpStatusCode.Forbidden,
                "application/json",
                """{"error":"access_denied","error_description":"request_submitted"}"""
            );

        var result = await this.sut.SubmitPermissionRequestAsync("bearer-token", "ticket");

        result.Should().BeTrue();
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SubmitPermissionRequestAsync_OtherForbidden_ReturnsFalse()
    {
        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .Respond(
                HttpStatusCode.Forbidden,
                "application/json",
                """{"error":"access_denied","error_description":"not_authorized"}"""
            );

        var result = await this.sut.SubmitPermissionRequestAsync("bearer-token", "ticket");

        result.Should().BeFalse();
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SubmitPermissionRequestAsync_SendsSubmitRequestParameter()
    {
        string? capturedBody = null;

        this.handler.Expect(HttpMethod.Post, TokenEndpoint)
            .With(req =>
            {
                capturedBody = req.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", """{"access_token":"rpt-token"}""");

        await this.sut.SubmitPermissionRequestAsync("bearer-token", "ticket");

        capturedBody.Should().Contain("submit_request=true");
    }
}
