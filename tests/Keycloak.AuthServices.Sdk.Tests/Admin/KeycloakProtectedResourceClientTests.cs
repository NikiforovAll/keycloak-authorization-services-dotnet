namespace Keycloak.AuthServices.Sdk.Tests.Admin;

using System;
using System.Net;
using Extensions;
using FluentAssertions;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Models.Resources;
using Sdk.Admin.Requests.Resources;

public class KeycloakProtectedResourceClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakProtectedResourceClient keycloakProtectedResourceClient;

    public KeycloakProtectedResourceClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakProtectedResourceClient = RestService.For<IKeycloakProtectedResourceClient>(httpClient,
            ServiceCollectionExtensions.GetKeycloakClientRefitSettings());
    }

    [Fact]
    public async Task GetResourcesShouldCallCorrectEndpoint()
    {
        var resources = Enumerable.Range(0, 3).Select(_ =>
        {
            var id = Guid.NewGuid();
            return (Id: id.ToString(), Representation: GetResourceRepresentation(id));
        }).ToArray();

        var response = $"[{string.Join(",", resources.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/realms/master/authz/protection/resource_set")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakProtectedResourceClient.GetResources("master");

        result.Should().BeEquivalentTo(resources.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourcesShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getResourcesRequestParameters = new GetResourcesRequestParameters
        {
            Name = "name",
            ExactName = true,
            Scope = "scope",
            Owner = "owner",
            ResourceType = "resource_type",
            Uri = "/my/uri"
        };

        var url = $"{BaseAddress}/realms/master/authz/protection/resource_set";
        var queryBuilder = new QueryBuilder
        {
            {"name", getResourcesRequestParameters.Name },
            {"exactName", getResourcesRequestParameters.ExactName.Value.ToString() },
            {"scope", getResourcesRequestParameters.Scope },
            {"owner", getResourcesRequestParameters.Owner },
            {"type", getResourcesRequestParameters.ResourceType },
            {"uri", getResourcesRequestParameters.Uri }
        };

        var resources = Enumerable.Range(0, 3).Select(_ =>
        {
            var id = Guid.NewGuid();
            return (Id: id.ToString(), Representation: GetResourceRepresentation(id));
        }).ToArray();

        var response = $"[{string.Join(",", resources.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakProtectedResourceClient.GetResources("master", getResourcesRequestParameters);

        result.Should().BeEquivalentTo(resources.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourcesShouldThrowNotFoundApiExceptionWhenResourceDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"error\":\"User not found\"}";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakProtectedResourceClient.GetResource("master", resourceId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();
        var response = GetResourceJson(resourceId);

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.Created, "application/json", response);

        await this.keycloakProtectedResourceClient.GetResource("master", resourceId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();
        var response = GetResourceJson(resourceId);

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/realms/master/authz/protection/resource_set")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created, "application/json", response);

        await this.keycloakProtectedResourceClient.CreateResource("master", GetResource(resourceId));

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateResourceShouldReturnBadRequestWhenRequestIsInvalid()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/realms/master/authz/protection/resource_set")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakProtectedResourceClient.CreateResource("master", GetResource(resourceId)));

        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakProtectedResourceClient.UpdateResource("master", resourceId.ToString(), GetResource(resourceId));

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateResourceShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Some value is missing\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            this.keycloakProtectedResourceClient.UpdateResource("master", resourceId.ToString(), GetResource(resourceId)));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .Respond(HttpStatusCode.OK);

        await this.keycloakProtectedResourceClient.DeleteResource("master", resourceId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteResourceShouldThrowApiNotFoundExceptionWhenGroupDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = "{\"error\":\"Resource not found\"}";

        this.handler.Expect(HttpMethod.Delete, $"{BaseAddress}/realms/master/authz/protection/resource_set/{resourceId}")
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakProtectedResourceClient.DeleteResource("master", resourceId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    // The GET method seemingly only returns the ID of the resource
    private static string GetResourceRepresentation(Guid id) => $@"""{id}""";

    private static string GetResourceJson(Guid id) => $@"{{
        ""name"": ""{id}"",
        ""type"": ""http://www.example.com/rsrcs/socialstream/140-compatible"",
        ""owner"": {{
            ""id"": ""{id}""
        }},
        ""ownerManagedAccess"": true,
        ""attributes"": {{}},
        ""_id"": ""{id}"",
        ""uris"": [],
        ""resource_scopes"": [
            {{
                ""name"": ""update""
            }},
            {{
                ""name"": ""retrieve""
            }}
        ],
        ""scopes"": [
            {{
                ""name"": ""update""
            }},
            {{
                ""name"": ""retrieve""
            }}
        ]
    }}";

    private static Resource GetResource(Guid id) => new(id.ToString(), new[] { "scope_one", "scope_two" })
    {
        DisplayName = "display_name",
        Type = "http://www.example.com/rsrcs/socialstream/140-compatible",
        Attributes = { { "my_custom_attribute_key", "my_custom_attribute_value" } }
    };
}
