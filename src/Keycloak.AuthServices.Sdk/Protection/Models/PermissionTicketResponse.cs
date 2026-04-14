namespace Keycloak.AuthServices.Sdk.Protection.Models;

using System.Text.Json.Serialization;

/// <summary>
/// The response from the permission endpoint containing the ticket.
/// </summary>
public class PermissionTicketResponse
{
    /// <summary>
    /// The permission ticket string.
    /// </summary>
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; } = default!;
}
