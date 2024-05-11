namespace Keycloak.AuthServices.Authorization.Tests;

using FluentAssertions;
using Keycloak.AuthServices.Sdk.AuthZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Moq;

public class UriBasedResourceProtectionMiddlewareTests
{
    [Fact]
    public void Constructor_DelegateNull_ThrowArgumentNullException()
    {
        // Arrange
        var clientMock = new Mock<IKeycloakProtectionClient>();
        Action intialization = () => _ = new UriBasedResourceProtectionMiddleware(null, clientMock.Object);

        // Act & Assert
        intialization.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_KeycloakClientNull_ThrowArgumentNullException()
    {
        // Arrange
        var requestDelegateMock = new Mock<RequestDelegate>();
        Action intialization = () => _ = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, null);

        // Act & Assert
        intialization.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async void EvaluateAuthorization_NoAdditionalAttributesGiven_CallAuthorizationClientWithExpectedParameters()
    {
        // Arrange
        var resourceName = "/resourceName";
        var scope = "GET";

        var clientMock = new Mock<IKeycloakProtectionClient>();
        var requestDelegateMock = new Mock<RequestDelegate>();
        clientMock.Setup(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(x => x.Path).Returns(resourceName);
        httpRequestMock.Setup(x => x.Method).Returns(scope);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(httpRequestMock.Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        clientMock.Verify(x => x.VerifyAccessToResource(resourceName, scope, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async void EvaluateAuthorization_DisableAttributesGiven_DontCallAuthorizationClientButCallDelegate()
    {
        // Arrange
        var clientMock = new Mock<IKeycloakProtectionClient>();
        var requestDelegateMock = new Mock<RequestDelegate>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(new Mock<HttpRequest>().Object);
        SetAttributesOnContextMock(httpContextMock,
                                    new List<Attribute>() { new ExplicitResourceProtectionAttribute(true) },
                                    requestDelegateMock.Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        clientMock.Verify(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        requestDelegateMock.Verify(x => x.Invoke(httpContextMock.Object), Times.Once);
    }

    [Fact]
    public async void EvaluateAuthorization_AllowAnonymousAttributesGiven_DontCallAuthorizationClientButCallDelegate()
    {
        // Arrange
        var clientMock = new Mock<IKeycloakProtectionClient>();
        var requestDelegateMock = new Mock<RequestDelegate>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(new Mock<HttpRequest>().Object);
        SetAttributesOnContextMock(httpContextMock,
                                    new List<Attribute>() { new AllowAnonymousAttribute() },
                                    requestDelegateMock.Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        clientMock.Verify(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        requestDelegateMock.Verify(x => x.Invoke(httpContextMock.Object), Times.Once);
    }

    [Fact]
    public async void EvaluateAuthorization_ExplicitAuthAttributesGiven_CallAuthorizationClientWithAttributeProperties()
    {
        // Arrange
        var resourceName = "/resourceName";
        var scope = "GET";
        var resourceNameFromAttribute = "/resourceNameFromAttribute";
        var scopeFromAttribute = "GET";

        var clientMock = new Mock<IKeycloakProtectionClient>();
        clientMock.Setup(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var requestDelegateMock = new Mock<RequestDelegate>();

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(x => x.Path).Returns(resourceName);
        httpRequestMock.Setup(x => x.Method).Returns(scope);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(httpRequestMock.Object);
        SetAttributesOnContextMock(httpContextMock,
                                    new List<Attribute>() {
                                        new ExplicitResourceProtectionAttribute(resourceNameFromAttribute, scopeFromAttribute)
                                        },
                                    requestDelegateMock.Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        clientMock.Verify(x => x.VerifyAccessToResource(resourceNameFromAttribute, scopeFromAttribute, CancellationToken.None), Times.Once);
        clientMock.Verify(x => x.VerifyAccessToResource(resourceName, scope, CancellationToken.None), Times.Never);
    }

    [Fact]
    public async void EvaluateAuthorization_ClientReturnsTrue_CallDelegate()
    {
        // Arrange
        var clientMock = new Mock<IKeycloakProtectionClient>();
        clientMock.Setup(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var requestDelegateMock = new Mock<RequestDelegate>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(new Mock<HttpRequest>().Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        requestDelegateMock.Verify(x => x.Invoke(httpContextMock.Object), Times.Once);
    }

    [Fact]
    public async void EvaluateAuthorization_ClientReturnsFalse_DontCallDelegateAndSet401()
    {
        // Arrange

        var clientMock = new Mock<IKeycloakProtectionClient>();
        clientMock.Setup(x => x.VerifyAccessToResource(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var requestDelegateMock = new Mock<RequestDelegate>();

        var httpResponseMock = new Mock<HttpResponse>();
        httpResponseMock.SetupAllProperties();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.Request).Returns(new Mock<HttpRequest>().Object);
        httpContextMock.Setup(x => x.Response).Returns(httpResponseMock.Object);

        // Act
        var target = new UriBasedResourceProtectionMiddleware(requestDelegateMock.Object, clientMock.Object);
        await target.InvokeAsync(httpContextMock.Object);

        // Assert
        requestDelegateMock.Verify(x => x.Invoke(httpContextMock.Object), Times.Never);
        httpContextMock.Object.Response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    private static void SetAttributesOnContextMock(Mock<HttpContext> httpContextMock, IEnumerable<Attribute> attributes, RequestDelegate requestDelegate)
    {
        var endpoint = new Endpoint(requestDelegate, new EndpointMetadataCollection(attributes), null);

        var endpointFeatureMock = new Mock<IEndpointFeature>();
        endpointFeatureMock.Setup(x => x.Endpoint).Returns(endpoint);

        var featuresMock = new Mock<IFeatureCollection>();
        featuresMock.Setup(x => x.Get<IEndpointFeature>()).Returns(endpointFeatureMock.Object);

        httpContextMock.Setup(x => x.Features).Returns(featuresMock.Object);
    }
}