namespace Keycloak.AuthServices.Authorization.Claims;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

/// <summary>
/// Composes multiple <see cref="IClaimsTransformation"/> instances and runs them in order.
/// ASP.NET Core only resolves a single IClaimsTransformation, so this wrapper
/// chains multiple transformations sequentially.
/// </summary>
internal sealed class CompositeClaimsTransformation : IClaimsTransformation
{
    private readonly IClaimsTransformation[] transformations;

    public CompositeClaimsTransformation(IEnumerable<IClaimsTransformation> transformations) =>
        this.transformations = transformations.ToArray();

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        foreach (var transformation in this.transformations)
        {
            principal = await transformation.TransformAsync(principal);
        }

        return principal;
    }
}
