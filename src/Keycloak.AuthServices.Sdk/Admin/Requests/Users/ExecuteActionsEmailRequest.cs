namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

/// <summary>
/// Send an email to the user with a link they can click to execute particular actions.
/// </summary>
public class ExecuteActionsEmailRequest
{
    /// <summary>
    /// Client id
    /// </summary>
    public string? ClientId { get; init; }

    /// <summary>
    /// Number of seconds after which the generated token expires
    /// </summary>
    public int? Lifespan { get; init; }

    /// <summary>
    /// Redirect uri
    /// </summary>
    public string? RedirectUri { get; init; }

    /// <summary>
    /// </summary>
    public IList<string>? Actions { get; init; }
}
