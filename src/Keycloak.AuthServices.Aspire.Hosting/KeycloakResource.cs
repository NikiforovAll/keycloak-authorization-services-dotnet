namespace Aspire.Hosting;

using Aspire.Hosting.ApplicationModel;

/// <summary>
/// A resource that represents a PostgreSQL container.
/// </summary>
public class KeycloakResource
    : ContainerResource,
        IResourceWithEnvironment,
        IResourceWithServiceDiscovery
{
    internal const string PrimaryEndpointName = "http";
    private const string DefaultUserName = "admin";
    private const string DefaultPassword = "admin";

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakResource"/> class.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="userName">A parameter that contains the Keycloak user name</param>
    /// <param name="password">A parameter that contains the Keycloak server password.</param>
    public KeycloakResource(string name, ParameterResource? userName, ParameterResource? password)
        : base(name)
    {
        this.PrimaryEndpoint = new(this, PrimaryEndpointName);
        this.UserNameParameter = userName;
        this.PasswordParameter = password;
    }

    /// <summary>
    /// Gets the primary endpoint for the Keycloak server.
    /// </summary>
    public EndpointReference PrimaryEndpoint { get; }

    /// <summary>
    /// Gets the parameter that contains the Keycloak server user name.
    /// </summary>
    public ParameterResource? UserNameParameter { get; }

    internal ReferenceExpression UserNameReference =>
        this.UserNameParameter is not null
            ? ReferenceExpression.Create($"{this.UserNameParameter}")
            : ReferenceExpression.Create($"{DefaultUserName}");

    /// <summary>
    /// Gets the parameter that contains the Keycloak server password.
    /// </summary>
    public ParameterResource? PasswordParameter { get; }

    internal ReferenceExpression PasswordReference =>
        this.PasswordParameter is not null
            ? ReferenceExpression.Create($"{this.PasswordParameter}")
            : ReferenceExpression.Create($"{DefaultPassword}");

    /// <summary>
    /// </summary>
    public IList<string> Imports { get; } = [];

    /// <summary>
    /// Adds an import to the list of imports for this Keycloak realm.
    /// </summary>
    /// <param name="import">The import to add.</param>
    /// <returns>The current Keycloak realm instance.</returns>
    public KeycloakResource WithImport(string import)
    {
        this.Imports.Add(import);

        return this;
    }
}

/// <summary>
/// A resource that represents a Realm. This is a child resource of a <see cref="KeycloakResource"/>.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="realmName">The Realm name.</param>
/// <param name="postgresParentResource">The PostgreSQL parent resource associated with this database.</param>
public class KeycloakRealmResource(
    string name,
    string realmName,
    KeycloakResource postgresParentResource
) : Resource(name), IResourceWithParent<KeycloakResource>
{
    /// <summary>
    /// Gets the parent PostgresSQL container resource.
    /// </summary>
    public KeycloakResource Parent { get; } = postgresParentResource;

    /// <summary>
    /// </summary>
    public string Realm { get; } = realmName;
}
