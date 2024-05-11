namespace Keycloak.AuthServices.Sdk.Tests.Extensions;

using RichardSzalay.MockHttp;

public static class MockedRequestExtensions
{
    public static MockedRequest WithAcceptHeader(this MockedRequest request) =>
        request.WithHeaders("Accept", "application/json");

    public static MockedRequest WithAcceptAndContentTypeHeaders(this MockedRequest request) =>
        request.WithHeaders(
            new Dictionary<string, string>
            {
                ["Accept"] = "application/json",
                ["Content-Type"] = "application/json"
            }
        );
}
