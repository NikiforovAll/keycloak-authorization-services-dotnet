namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

/// <summary>
/// Organization authorization extension methods for <see cref="IEndpointConventionBuilder"/>.
/// </summary>
public static class OrganizationEndpointConventionBuilderExtensions
{
    /// <summary>
    /// Requires the user to be a member of any Keycloak organization.
    /// </summary>
    /// <typeparam name="TBuilder">The type of endpoint convention builder.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireOrganizationMembership<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Add(endpointBuilder =>
        {
            AddOrganizationPolicy(endpointBuilder);
            endpointBuilder.Metadata.Add(new OrganizationRequirement());
        });

        return builder;
    }

    /// <summary>
    /// Requires the user to be a member of the specified Keycloak organization.
    /// Supports <c>{param}</c> route templates that are resolved at evaluation time.
    /// </summary>
    /// <typeparam name="TBuilder">The type of endpoint convention builder.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="organization">The organization alias, id, or route template (e.g., "{orgId}").</param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireOrganizationMembership<TBuilder>(
        this TBuilder builder,
        string organization
    )
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(organization);

        builder.Add(endpointBuilder =>
        {
            AddOrganizationPolicy(endpointBuilder);
            endpointBuilder.Metadata.Add(new OrganizationRequirement(organization));
        });

        return builder;
    }

    /// <summary>
    /// Requires the user to be a member of the specified Keycloak organization,
    /// using a custom <see cref="IParameterResolver"/> to resolve the organization template parameter.
    /// </summary>
    /// <typeparam name="TBuilder">The type of endpoint convention builder.</typeparam>
    /// <typeparam name="TResolver">The type of <see cref="IParameterResolver"/> to use.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="organization">The organization alias, id, or template (e.g., "{X-Organization}").</param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireOrganizationMembership<TBuilder, TResolver>(
        this TBuilder builder,
        string organization
    )
        where TBuilder : IEndpointConventionBuilder
        where TResolver : IParameterResolver
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(organization);

        builder.Add(endpointBuilder =>
        {
            AddOrganizationPolicy(endpointBuilder);
            endpointBuilder.Metadata.Add(
                new OrganizationRequirement(organization) { ResolverType = typeof(TResolver) }
            );
        });

        return builder;
    }

    /// <summary>
    /// Requires the user to be a member of the specified Keycloak organization,
    /// using a custom <see cref="IParameterResolver"/> to resolve the organization template parameter.
    /// </summary>
    /// <typeparam name="TBuilder">The type of endpoint convention builder.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="organization">The organization alias, id, or template (e.g., "{X-Organization}").</param>
    /// <param name="resolverType">The type of <see cref="IParameterResolver"/> to use.</param>
    /// <returns>The original convention builder parameter.</returns>
    public static TBuilder RequireOrganizationMembership<TBuilder>(
        this TBuilder builder,
        string organization,
        Type resolverType
    )
        where TBuilder : IEndpointConventionBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(organization);
        ValidateResolverType(resolverType);

        builder.Add(endpointBuilder =>
        {
            AddOrganizationPolicy(endpointBuilder);
            endpointBuilder.Metadata.Add(
                new OrganizationRequirement(organization) { ResolverType = resolverType }
            );
        });

        return builder;
    }

    private static void ValidateResolverType(Type resolverType)
    {
        ArgumentNullException.ThrowIfNull(resolverType);

        if (!typeof(IParameterResolver).IsAssignableFrom(resolverType))
        {
            throw new ArgumentException(
                $"Type '{resolverType}' does not implement {nameof(IParameterResolver)}.",
                nameof(resolverType)
            );
        }
    }

    private static void AddOrganizationPolicy(EndpointBuilder endpointBuilder)
    {
        if (!endpointBuilder.Metadata.Any(m => m is OrganizationRequirement))
        {
            endpointBuilder.Metadata.Add(
                new AuthorizeAttribute(OrganizationRequirement.OrganizationPolicy)
            );
        }
    }
}
