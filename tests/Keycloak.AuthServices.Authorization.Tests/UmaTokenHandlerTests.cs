namespace Keycloak.AuthServices.Authorization.Tests;

using System.Net;
using System.Security.Claims;
using System.Text;
using Keycloak.AuthServices.Authorization.Uma;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;

public class UmaTokenHandlerTests
{
    private const string ApiUrl = "http://api.test/resource";

    private static UmaTokenHandler CreateHandler(
        MockHttpMessageHandler mockHttp,
        IUmaTicketExchangeClient umaClient,
        string? accessToken = "test-access-token"
    )
    {
        var httpContext = new DefaultHttpContext();
        var services = new ServiceCollection();
        services.AddSingleton<IAuthenticationService>(new FakeAuthenticationService(accessToken));
        httpContext.RequestServices = services.BuildServiceProvider();

        var httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };
        var logger = NullLogger<UmaTokenHandler>.Instance;

        return new UmaTokenHandler(httpContextAccessor, umaClient, logger)
        {
            InnerHandler = mockHttp,
        };
    }

    private static HttpClient CreateClient(UmaTokenHandler handler) =>
        new(handler) { BaseAddress = new Uri("http://api.test/") };

    private static HttpResponseMessage UmaChallenge(string ticket) =>
        new HttpResponseMessage(HttpStatusCode.Unauthorized)
        {
            Headers =
            {
                { "WWW-Authenticate", $@"UMA as_uri=""http://keycloak/"", ticket=""{ticket}""" },
            },
        };

    [Fact]
    public async Task SendAsync_NonUnauthorizedResponse_ReturnsResponseDirectly()
    {
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(HttpStatusCode.OK);
        var umaClient = new FakeUmaTicketExchangeClient("rpt-token");
        var handler = CreateHandler(mockHttp, umaClient);
        using var client = CreateClient(handler);

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        mockHttp.VerifyNoOutstandingExpectation();
        umaClient.ExchangeCallCount.Should().Be(0);
    }

    [Fact]
    public async Task SendAsync_UnauthorizedWithUmaChallenge_RetriesWithRpt()
    {
        const string ticket = "permission-ticket-xyz";
        const string rpt = "rpt-token-xyz";
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(_ => UmaChallenge(ticket));
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(HttpStatusCode.OK);
        var umaClient = new FakeUmaTicketExchangeClient(rpt);
        var handler = CreateHandler(mockHttp, umaClient, accessToken: "initial-token");
        using var client = CreateClient(handler);

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        umaClient.ExchangeCallCount.Should().Be(1);
        umaClient.LastUsedTicket.Should().Be(ticket);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_UnauthorizedWithUmaChallenge_SendsRptInRetry()
    {
        const string ticket = "permission-ticket-abc";
        const string rpt = "rpt-token-abc";
        string? capturedAuthHeader = null;

        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(_ => UmaChallenge(ticket));
        mockHttp
            .Expect(HttpMethod.Get, ApiUrl)
            .With(req =>
            {
                capturedAuthHeader = req.Headers.Authorization?.ToString();
                return true;
            })
            .Respond(HttpStatusCode.OK);
        var umaClient = new FakeUmaTicketExchangeClient(rpt);
        var handler = CreateHandler(mockHttp, umaClient, accessToken: "initial-token");
        using var client = CreateClient(handler);

        await client.GetAsync("resource");

        capturedAuthHeader.Should().Be($"Bearer {rpt}");
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_UnauthorizedWithUmaChallenge_PreservesRequestBody()
    {
        const string ticket = "ticket-body-test";
        const string rpt = "rpt-body-test";
        const string requestBody = """{"action":"create"}""";
        string? capturedBody = null;

        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Post, ApiUrl).Respond(_ => UmaChallenge(ticket));
        mockHttp
            .Expect(HttpMethod.Post, ApiUrl)
            .With(req =>
            {
                capturedBody = req.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.Created);
        var umaClient = new FakeUmaTicketExchangeClient(rpt);
        var handler = CreateHandler(mockHttp, umaClient, accessToken: "initial-token");
        using var client = CreateClient(handler);

        using var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("resource", content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        capturedBody.Should().Be(requestBody);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_UnauthorizedNoAccessToken_DoesNotExchange()
    {
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(_ => UmaChallenge("some-ticket"));
        var umaClient = new FakeUmaTicketExchangeClient("rpt");
        var handler = CreateHandler(mockHttp, umaClient, accessToken: null);
        using var client = CreateClient(handler);

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        umaClient.ExchangeCallCount.Should().Be(0);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_UnauthorizedWithoutUmaChallenge_DoesNotExchange()
    {
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .Expect(HttpMethod.Get, ApiUrl)
            .Respond(_ => new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Headers = { { "WWW-Authenticate", @"Bearer realm=""api""" } },
            });
        var umaClient = new FakeUmaTicketExchangeClient("rpt");
        var handler = CreateHandler(mockHttp, umaClient);
        using var client = CreateClient(handler);

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        umaClient.ExchangeCallCount.Should().Be(0);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_RptExchangeFails_ReturnsOriginal401()
    {
        const string ticket = "ticket-exchange-fail";
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(_ => UmaChallenge(ticket));
        var umaClient = new FakeUmaTicketExchangeClient(rpt: null);
        var handler = CreateHandler(mockHttp, umaClient);
        using var client = CreateClient(handler);

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        umaClient.ExchangeCallCount.Should().Be(1);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendAsync_NoHttpContext_ReturnsDelegateResponse()
    {
        using var mockHttp = new MockHttpMessageHandler();
        mockHttp.Expect(HttpMethod.Get, ApiUrl).Respond(HttpStatusCode.OK);

        var httpContextAccessor = new HttpContextAccessor { HttpContext = null };
        var umaClient = new FakeUmaTicketExchangeClient("rpt");
        var logger = NullLogger<UmaTokenHandler>.Instance;
        var handler = new UmaTokenHandler(httpContextAccessor, umaClient, logger)
        {
            InnerHandler = mockHttp,
        };
        using var client = new HttpClient(handler) { BaseAddress = new Uri("http://api.test/") };

        var response = await client.GetAsync("resource");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        umaClient.ExchangeCallCount.Should().Be(0);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    private sealed class FakeUmaTicketExchangeClient(string? rpt) : IUmaTicketExchangeClient
    {
        public int ExchangeCallCount { get; private set; }
        public string? LastUsedTicket { get; private set; }

        public Task<string?> ExchangeTicketForRptAsync(
            string accessToken,
            string ticket,
            CancellationToken cancellationToken = default
        )
        {
            this.ExchangeCallCount++;
            this.LastUsedTicket = ticket;
            return Task.FromResult(rpt);
        }

        public Task<bool> SubmitPermissionRequestAsync(
            string accessToken,
            string ticket,
            CancellationToken cancellationToken = default
        ) => Task.FromResult(false);
    }

    private sealed class FakeAuthenticationService(string? accessToken) : IAuthenticationService
    {
        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
        {
            if (accessToken is null)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var props = new AuthenticationProperties();
            props.StoreTokens([
                new AuthenticationToken { Name = "access_token", Value = accessToken },
            ]);
            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(),
                props,
                scheme ?? string.Empty
            );
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        public Task ChallengeAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task ForbidAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task SignInAsync(
            HttpContext context,
            string? scheme,
            ClaimsPrincipal principal,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task SignOutAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;
    }
}
