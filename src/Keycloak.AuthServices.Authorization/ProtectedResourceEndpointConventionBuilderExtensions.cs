namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Builder;

/// <summary>
/// Authorization extension methods for <see cref="IEndpointConventionBuilder"/>.
/// </summary>
public static class ProtectedResourceEndpointConventionBuilderExtensions
{
    /// <summary>
    /// Adds authorization policies with the specified names to the endpoint(s).
    /// </summary>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="resource"></param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireProtectedResource<TBuilder>(
        this TBuilder builder,
        string resource
    )
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(resource);

        RequireAuthorizationCore(
            builder,
            new ProtectedResourceAttribute[] { new(resource, string.Empty) }
        );

        return builder;
    }

    /// <summary>
    /// Adds authorization policies with the specified names to the endpoint(s).
    /// </summary>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireProtectedResource<TBuilder>(
        this TBuilder builder,
        string resource,
        string scope
    )
        where TBuilder : IEndpointConventionBuilder =>
        builder.RequireProtectedResource(resource, new string[] { scope });

    /// <summary>
    /// Adds authorization policies with the specified names to the endpoint(s).
    /// </summary>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="resource"></param>
    /// <param name="scopes"></param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireProtectedResource<TBuilder>(
        this TBuilder builder,
        string resource,
        string[] scopes
    )
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scopes);

        RequireAuthorizationCore(
            builder,
            new ProtectedResourceAttribute[] { new(resource, scopes) }
        );

        return builder;
    }

    private static void RequireAuthorizationCore<TBuilder>(
        TBuilder builder,
        IEnumerable<IProtectedResourceData> authorizeData
    )
        where TBuilder : IEndpointConventionBuilder
    {
        builder.RequireAuthorization(
            DynamicProtectedResourceRequirement.DynamicProtectedResourcePolicy
        );

        builder.Add(endpointBuilder =>
        {
            foreach (var data in authorizeData)
            {
                endpointBuilder.Metadata.Add(data);
            }
        });
    }
}
