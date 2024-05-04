namespace Keycloak.AuthServices.Sdk;

using System.Globalization;

/// <summary>
/// Represents an exception that is thrown when an HTTP request to the Keycloak fails.
/// </summary>
public partial class KeycloakHttpClientException : Exception
{
    /// <summary>
    /// Gets the status code of the HTTP response.
    /// </summary>
    public int StatusCode { get; private set; }

    /// <summary>
    /// Gets the raw HTTP response.
    /// </summary>
    public string HttpResponse { get; private set; }

    /// <summary>
    /// Gets the deserialized error response from the Keycloak server.
    /// </summary>
    public ErrorResponse Response { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakHttpClientException"/> class with the specified parameters.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The status code of the HTTP response.</param>
    /// <param name="httpResponse">The raw HTTP response.</param>
    /// <param name="response">The deserialized error response from the Keycloak server.</param>
    /// <param name="innerException">The inner exception that caused this exception, or <c>null</c> if no inner exception is specified.</param>
    public KeycloakHttpClientException(
        string message,
        int statusCode,
        string httpResponse,
        ErrorResponse response,
        Exception? innerException
    )
        : base(message + "\n\nStatus: " + statusCode, innerException)
    {
        this.StatusCode = statusCode;
        this.HttpResponse = httpResponse;
        this.Response = response;
    }

    /// <summary>
    /// Returns a string that represents the current object, including the HTTP response.
    /// </summary>
    /// <returns>A string that represents the current object, including the HTTP response.</returns>
    public override string ToString() =>
        string.Format(
            CultureInfo.InvariantCulture,
            "HTTP Response: \n\n{0}\n\n{1}",
            this.HttpResponse,
            base.ToString()
        );
}
