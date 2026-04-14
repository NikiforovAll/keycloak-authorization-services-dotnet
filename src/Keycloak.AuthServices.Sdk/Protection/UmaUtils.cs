namespace Keycloak.AuthServices.Sdk.Protection;

/// <summary>
/// Utility methods for UMA (User-Managed Access) protocol handling.
/// </summary>
public static class UmaUtils
{
    /// <summary>
    /// Extracts the UMA permission ticket from a <c>WWW-Authenticate: UMA</c> response header.
    /// </summary>
    /// <param name="wwwAuthenticateHeader">The WWW-Authenticate header value.</param>
    /// <returns>The ticket string, or <c>null</c> if not found.</returns>
    public static string? ExtractUmaTicket(string wwwAuthenticateHeader)
    {
        if (string.IsNullOrEmpty(wwwAuthenticateHeader) || !wwwAuthenticateHeader.Contains("UMA"))
        {
            return null;
        }

        return ExtractParameter(wwwAuthenticateHeader, "ticket");
    }

    private static string? ExtractParameter(string header, string parameter)
    {
        var prefix = $"{parameter}=\"";
        var start = header.IndexOf(prefix, StringComparison.Ordinal);
        if (start < 0)
        {
            return null;
        }

        start += prefix.Length;
        var end = header.IndexOf('"', start);
        return end < 0 ? null : header[start..end];
    }
}
