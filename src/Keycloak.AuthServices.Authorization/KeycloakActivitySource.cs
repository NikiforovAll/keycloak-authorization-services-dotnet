namespace Keycloak.AuthServices.Authorization;

using System.Diagnostics;

/// <summary>
/// Represents the activity source for the Keycloak.AuthServices.Authorization namespace.
/// </summary>
internal static class KeycloakActivitySource
{
    private const string Version = "1.0.0";

    /// <summary>
    /// Gets the default activity source instance for Keycloak.AuthServices.Authorization.
    /// </summary>
    public static readonly ActivitySource Default =
        new("Keycloak.AuthServices.Authorization", Version);
}

internal static class ActivityConstants
{
    public const string Namespace = "keycloak.authservices";

    public static class Events
    {
        /// <summary>
        /// Represents the event when verification is started.
        /// </summary>
        public const string VerificationStarted = "Verification Started";

        /// <summary>
        /// Represents the event when verification is completed.
        /// </summary>
        public const string VerificationCompleted = "Verification Completed";
    }

    public static class Activities
    {
        /// <summary>
        /// Represents the activity for a parameterized protected resource requirement.
        /// </summary>
        public const string ProtectedResourceRequirement =
            "ParameterizedProtectedResourceRequirement";

        /// <summary>
        /// Represents the activity for protected resource verification.
        /// </summary>
        public const string ProtectedResourceVerification = "ProtectedResource";

        public const string DecisionRequirement = "ProtectedResource";

    }

    public static class Tags
    {
        /// <summary>
        /// Represents the tag for the resource.
        /// </summary>
        public const string Resource = $"{Namespace}.resource";

        /// <summary>
        /// Represents the tag for the scopes.
        /// </summary>
        public const string Scopes = $"{Namespace}.scopes";

        /// <summary>
        /// Represents the tag for the outcome.
        /// </summary>
        public const string Outcome = $"{Namespace}.outcome";
    }
}
