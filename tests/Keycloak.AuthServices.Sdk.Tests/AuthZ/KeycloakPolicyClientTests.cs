namespace Keycloak.AuthServices.Sdk.Tests.AuthZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using FluentAssertions;
using Keycloak.AuthServices.Sdk.Admin.Requests.Policy;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Models.Policies;
using Sdk.AuthZ;

public class KeycloakPolicyClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakPolicyClient keycloakPolicyClient;

    public KeycloakPolicyClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakPolicyClient = RestService.For<IKeycloakPolicyClient>(httpClient,
            ServiceCollectionExtensions.GetKeycloakClientRefitSettings());
    }

    [Fact]
    public async Task GetPoliciesShouldCallCorrectEndpoint()
    {
        var permissionTickets = Enumerable.Range(0, 3).Select(_ =>
        {
            var id = Guid.NewGuid().ToString();
            var name = Guid.NewGuid().ToString();
            return (Id: id, Response: GetPolicyResponse(id, name));
        }).ToArray();

        var response = $"[{string.Join(",", permissionTickets.Select(u => u.Response))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakPolicyClient.GetPolicies("master");

        result.Select(u => u.Id).Should().BeEquivalentTo(permissionTickets.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPolicyShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getPoliciesRequestParameters = new GetPoliciesRequestParameters
        {
            ResourceId = "my_id",
            First = 0,
            Max = 1,
            PermissionName = "my_permission_name",
            Scope = "my_scope"
        };

        var url = $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy";
        var queryBuilder = new QueryBuilder
        {
            {"name", "my_permission_name"},
            {"resource", "my_id"},
            {"scope", "my_scope"},
            {"first", "0"},
            {"max", "1"}
        };

        var response = $"[{GetPolicyResponse(getPoliciesRequestParameters.ResourceId, getPoliciesRequestParameters.PermissionName)}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        _ = await this.keycloakPolicyClient.GetPolicies("master", getPoliciesRequestParameters);

        this.handler.VerifyNoOutstandingExpectation();
    }


    [Fact]
    public async Task CreatePolicyShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        await this.keycloakPolicyClient.CreatePolicy("master", new()
        {
            Name = "PolicyName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePolicyShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Policy name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var response = await this.keycloakPolicyClient.CreatePolicy("master", new Policy());

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(errorMessage);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePolicyShouldCallCorrectEndpoint()
    {
        var policyId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy/{policyId}")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        await this.keycloakPolicyClient.UpdatePolicy("master", policyId.ToString(), new()
        {
            Name = "PolicyName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePolicyShouldThrowNotFoundApiExceptionWhenPolicyDoesNotExist()
    {
        var policyId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Policy name is missing\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/authz/protection/uma-policy/{policyId}")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            this.keycloakPolicyClient.UpdatePolicy("master", policyId.ToString(), new Policy()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetPolicyResponse(string id, string name) => $@"
    {{
        ""id"":""{id}"",
        ""name"":""{name}""
    }}";

    private static Policy GetPolicyRepresentation(Guid id) => new() { Name = "name", Id = id.ToString() };
}
