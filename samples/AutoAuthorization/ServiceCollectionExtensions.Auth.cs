namespace Microsoft.Extensions.DependencyInjection;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakAuthentication(configuration);

        services.AddAuthorization(authorizationOptions => authorizationOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build())
                .AddKeycloakAuthorization(configuration);

        return services;
    }
}

public static class AuthorizationConstants
{
    public static class Roles
    {
        public const string AspNetCoreRole = "realm-role";

        public const string RealmRole = "realm-role";

        public const string ClientRole = "client-role";
    }

    public static class Policies
    {
        public const string RequireAspNetCoreRole = nameof(RequireAspNetCoreRole);

        public const string RequireRealmRole = nameof(RequireRealmRole);

        public const string RequireClientRole = nameof(RequireClientRole);

        public const string RequireToBeInKeycloakGroupAsReader = nameof(RequireToBeInKeycloakGroupAsReader);
    }
}
