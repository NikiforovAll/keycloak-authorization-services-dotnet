namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Keycloak.AuthServices.Sdk.Utils;
using RichardSzalay.MockHttp;

public class KeycloakPolicyClientTests
{
    private const string BaseAddress = "http://localhost:8080";
    private const string CurrentRealm = "master";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakPolicyClient keycloakPolicyClient;

    public KeycloakPolicyClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakPolicyClient = new KeycloakProtectionClient(httpClient);
    }

    [Fact]
    public async Task GetPoliciesShouldCallCorrectEndpoint()
    {
        var permissionTickets = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid().ToString();
                var name = Guid.NewGuid().ToString();
                return (Id: id, Response: GetPolicyResponse(id, name));
            })
            .ToArray();

        var response = $"[{string.Join(",", permissionTickets.Select(u => u.Response))}]";

        this.handler.Expect(HttpMethod.Get, $"/realms/{CurrentRealm}/authz/protection/uma-policy")
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakPolicyClient.GetPoliciesAsync(CurrentRealm);

        result.Select(u => u.Id).Should().BeEquivalentTo(permissionTickets.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPoliciesShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getPoliciesRequestParameters = new GetPoliciesRequestParameters
        {
            ResourceId = "my_id",
            First = 0,
            Max = 1,
            PermissionName = "my_permission_name",
            Scope = "my_scope"
        };

        var url = $"/realms/{CurrentRealm}/authz/protection/uma-policy";
        var queryBuilder = new QueryBuilder
        {
            { "name", "my_permission_name" },
            { "resource", "my_id" },
            { "scope", "my_scope" },
            { "first", "0" },
            { "max", "1" }
        };

        var response =
            $"[{GetPolicyResponse(getPoliciesRequestParameters.ResourceId, getPoliciesRequestParameters.PermissionName)}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .Respond(HttpStatusCode.OK, "application/json", response);

        _ = await this.keycloakPolicyClient.GetPoliciesAsync(
            CurrentRealm,
            getPoliciesRequestParameters
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPolicyShouldCallCorrectEndpoint()
    {
        var policyId = Guid.NewGuid();
        var response = GetPolicyResponse(policyId.ToString(), policyId.ToString());

        this.handler.Expect(
                HttpMethod.Get,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{policyId}"
            )
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakPolicyClient.GetPolicyAsync(
            CurrentRealm,
            policyId.ToString()
        );

        result.Id.Should().BeEquivalentTo(policyId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePolicyShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid().ToString();

        this.handler.Expect(
                HttpMethod.Post,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{resourceId}"
            )
            .Respond(HttpStatusCode.Created);

        await this.keycloakPolicyClient.CreatePolicyAsync(
            CurrentRealm,
            resourceId,
            new() { Name = "PolicyName" }
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePolicyShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Policy name is missing\"}";
        var resourceId = Guid.NewGuid().ToString();

        this.handler.Expect(
                HttpMethod.Post,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{resourceId}"
            )
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakPolicyClient.CreatePolicyAsync(CurrentRealm, resourceId, new Policy())
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        exception.Response.Error.Should().Be("Policy name is missing");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePolicyShouldCallCorrectEndpoint()
    {
        var policyId = Guid.NewGuid();

        this.handler.Expect(
                HttpMethod.Put,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{policyId}"
            )
            .Respond(HttpStatusCode.Created);

        await this.keycloakPolicyClient.UpdatePolicyAsync(
            CurrentRealm,
            policyId.ToString(),
            new() { Name = "PolicyName" }
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePolicyShouldThrowNotFoundApiExceptionWhenPolicyDoesNotExist()
    {
        var policyId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Policy name is missing\"}";

        this.handler.Expect(
                HttpMethod.Put,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{policyId}"
            )
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakPolicyClient.UpdatePolicyAsync(
                    CurrentRealm,
                    policyId.ToString(),
                    new Policy()
                )
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.Response.Error.Should().Be("Policy name is missing");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeletePolicyShouldCallCorrectEndpoint()
    {
        var policyId = Guid.NewGuid();

        this.handler.Expect(
                HttpMethod.Delete,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{policyId}"
            )
            .Respond(HttpStatusCode.OK);

        await this.keycloakPolicyClient.DeletePolicyAsync(CurrentRealm, policyId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeletePolicyShouldThrowApiNotFoundExceptionWhenPolicyDoesNotExist()
    {
        var policyId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Policy not found\"}";

        this.handler.Expect(
                HttpMethod.Delete,
                $"/realms/{CurrentRealm}/authz/protection/uma-policy/{policyId}"
            )
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () => this.keycloakPolicyClient.DeletePolicyAsync(CurrentRealm, policyId.ToString())
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.Response.Error.Should().Be("Policy not found");
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetPolicyResponse(string id, string name) =>
        $@"
    {{
        ""id"":""{id}"",
        ""name"":""{name}""
    }}";
}
