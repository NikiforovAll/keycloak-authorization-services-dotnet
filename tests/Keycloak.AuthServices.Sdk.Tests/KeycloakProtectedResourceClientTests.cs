namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Keycloak.AuthServices.Sdk.Utils;
using RichardSzalay.MockHttp;

public class KeycloakProtectedResourceClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakProtectedResourceClient keycloakProtectedResourceClient;

    public KeycloakProtectedResourceClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakProtectedResourceClient = new KeycloakProtectionClient(httpClient);
    }

    [Fact]
    public async Task GetResourcesIdsShouldCallCorrectEndpoint()
    {
        var resources = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetResourceRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", resources.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"/realms/master/authz/protection/resource_set")
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakProtectedResourceClient.GetResourcesIdsAsync("master");

        result.Should().BeEquivalentTo(resources.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourcesIdsAsyncShouldCallCorrectEndpointWithOptionalQueryParameters()
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

        var url = $"/realms/master/authz/protection/resource_set";
        var queryBuilder = new QueryBuilder
        {
            { "name", getResourcesRequestParameters.Name },
            { "exactName", getResourcesRequestParameters.ExactName.Value.ToString() },
            { "scope", getResourcesRequestParameters.Scope },
            { "owner", getResourcesRequestParameters.Owner },
            { "type", getResourcesRequestParameters.ResourceType },
            { "uri", getResourcesRequestParameters.Uri }
        };

        var resources = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetResourceRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", resources.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakProtectedResourceClient.GetResourcesIdsAsync(
            "master",
            getResourcesRequestParameters
        );

        result.Should().BeEquivalentTo(resources.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourcesShouldThrowNotFoundApiExceptionWhenResourceDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"User not found\"}";

        this.handler.Expect(
                HttpMethod.Get,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakProtectedResourceClient.GetResourceAsync(
                    "master",
                    resourceId.ToString()
                )
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.Response.Error.Should().Be("User not found");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();
        var response = GetResourceJson(resourceId);

        this.handler.Expect(
                HttpMethod.Get,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.Created, "application/json", response);

        await this.keycloakProtectedResourceClient.GetResourceAsync(
            "master",
            resourceId.ToString()
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();
        var response = GetResourceJson(resourceId);

        this.handler.Expect(HttpMethod.Post, $"/realms/master/authz/protection/resource_set")
            .Respond(HttpStatusCode.Created, "application/json", response);

        await this.keycloakProtectedResourceClient.CreateResourceAsync(
            "master",
            GetResource(resourceId)
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateResourceShouldReturnBadRequestWhenRequestIsInvalid()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"/realms/master/authz/protection/resource_set")
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakProtectedResourceClient.CreateResourceAsync(
                    "master",
                    GetResource(resourceId)
                )
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        exception.Response.Error.Should().Be("User name is missing");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();

        this.handler.Expect(
                HttpMethod.Put,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakProtectedResourceClient.UpdateResourceAsync(
            "master",
            resourceId.ToString(),
            GetResource(resourceId)
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateResourceShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Some value is missing\"}";

        this.handler.Expect(
                HttpMethod.Put,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakProtectedResourceClient.UpdateResourceAsync(
                    "master",
                    resourceId.ToString(),
                    GetResource(resourceId)
                )
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.Response.Error.Should().Be("Some value is missing");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteResourceShouldCallCorrectEndpoint()
    {
        var resourceId = Guid.NewGuid();

        this.handler.Expect(
                HttpMethod.Delete,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.OK);

        await this.keycloakProtectedResourceClient.DeleteResourceAsync(
            "master",
            resourceId.ToString()
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteResourceShouldThrowApiNotFoundExceptionWhenGroupDoesNotExist()
    {
        var resourceId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Resource not found\"}";

        this.handler.Expect(
                HttpMethod.Delete,
                $"/realms/master/authz/protection/resource_set/{resourceId}"
            )
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<KeycloakHttpClientException>(
            () =>
                this.keycloakProtectedResourceClient.DeleteResourceAsync(
                    "master",
                    resourceId.ToString()
                )
        );

        exception.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.Response.Error.Should().Be("Resource not found");
        this.handler.VerifyNoOutstandingExpectation();
    }

    // The GET method seemingly only returns the ID of the resource
    private static string GetResourceRepresentation(Guid id) => $@"""{id}""";

    private static string GetResourceJson(Guid id) =>
        $@"{{
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

    private static Resource GetResource(Guid id) =>
        new(id.ToString(), ["scope_one", "scope_two"])
        {
            DisplayName = "display_name",
            Type = "http://www.example.com/rsrcs/socialstream/140-compatible",
            Attributes = { { "my_custom_attribute_key", "my_custom_attribute_value" } }
        };
}
