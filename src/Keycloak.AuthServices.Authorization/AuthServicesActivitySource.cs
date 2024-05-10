namespace Keycloak.AuthServices.Authorization;

using System.Diagnostics;

public static class AuthServicesActivitySource
{
    private const string Version = "1.0.0";

    public static readonly ActivitySource Default =
        new("Keycloak.AuthServices.Authorization", Version);
}

internal static class ActivityConstants
{
    public const string Namespace = "keycloak.authservices";

    public static class Events
    {
        public const string VerificationStarted = "Verification Started";
        public const string VerificationCompleted = "Verification Completed";
    }

    public static class Activities
    {
        public const string ProtectedResourceRequirement =
            "ParameterizedProtectedResourceRequirement";
        public const string ProtectedResourceVerification = "ProtectedResource";
    }

    public static class Tags
    {
        public const string Resource = $"{Namespace}.resource";
        public const string Scopes = $"{Namespace}.scopes";
        public const string Outcome = $"{Namespace}.outcome";
    }
}
