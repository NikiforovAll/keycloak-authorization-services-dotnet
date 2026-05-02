namespace Aspire.Hosting;

using Aspire.Hosting.ApplicationModel;

/// <summary>
/// Provides extension methods for adding Keycloak resources to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class KeycloakBuilderExtensions
{
    private const string UserEnvVarName = "KEYCLOAK_ADMIN";
    private const string PasswordEnvVarName = "KEYCLOAK_ADMIN_PASSWORD";
    private const int DefaultContainerPort = 8080;

    /// <summary>
    /// Adds a Keycloak resource to the application model. A container is used for local development.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource. </param>
    /// <param name="tag"></param>
    /// <param name="userName">The parameter used to provide the user name for the Keycloak resource.</param>
    /// <param name="password">The parameter used to provide the administrator password for the Keycloak resource. </param>
    /// <param name="port">The host port used when launching the container.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> AddKeycloakContainer(
        this IDistributedApplicationBuilder builder,
        string name,
        string? tag = null,
        IResourceBuilder<ParameterResource>? userName = null,
        IResourceBuilder<ParameterResource>? password = null,
        int? port = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        var server = new KeycloakResource(name, userName?.Resource, password?.Resource);

        return builder
            .AddResource(server)
            .WithHttpEndpoint(
                port: port ?? DefaultContainerPort,
                targetPort: DefaultContainerPort,
                name: KeycloakResource.PrimaryEndpointName
            )
            .WithImage(KeycloakContainerImageTags.Image, tag ?? KeycloakContainerImageTags.Tag)
            .WithImageRegistry(KeycloakContainerImageTags.Registry)
            .WithEnvironment(context =>
            {
                context.EnvironmentVariables[UserEnvVarName] = server.UserNameReference;
                context.EnvironmentVariables[PasswordEnvVarName] = server.PasswordReference;
            })
            .WithAnnotation(
                new CommandLineArgsCallbackAnnotation(args =>
                {
                    args.Clear();

                    if (builder.ExecutionContext.IsRunMode)
                    {
                        args.Add("start-dev");
                    }
                    else
                    {
                        args.Add("start");
                    }
                    args.Add("--import-realm");
                })
            );
    }

    /// <summary>
    /// Adds a Realm to the application model.
    /// </summary>
    /// <param name="builder">The Keycloak server resource builder.</param>
    /// <param name="name">The name of the resource. </param>
    /// <param name="realm">The name of the Realm.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakRealmResource> AddRealm(
        this IResourceBuilder<KeycloakResource> builder,
        string name,
        string? realm = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        var realmResource = new KeycloakRealmResource(name, realm ?? name, builder.Resource);

        return builder.ApplicationBuilder.AddResource(realmResource).ExcludeFromManifest();
    }

    /// <summary>
    /// Adds a Realm to the application model.
    /// </summary>
    /// <param name="builder">The Keycloak server resource builder.</param>
    /// <param name="import"></param>
    /// <param name="isReadOnly"></param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> WithImport(
        this IResourceBuilder<KeycloakResource> builder,
        string import,
        bool isReadOnly = false
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Resource.WithImport(import);

        if (Directory.Exists(import))
        {
            builder.WithBindMount(import, "/opt/keycloak/data/import", isReadOnly);
        }
        else
        {
            var fileName = Path.GetFileName(import);

            builder.WithBindMount(import, $"/opt/keycloak/data/import/{fileName}", isReadOnly);
        }

        return builder;
    }

    /// <summary>
    /// Adds a named volume for the data folder to a PostgreSQL container resource.
    /// </summary>
    /// <param name="builder">The resource builder.</param>
    /// <param name="name">The name of the volume. Defaults to an auto-generated name based on the application and resource names.</param>
    /// <param name="isReadOnly">A flag that indicates if this is a read-only volume.</param>
    /// <returns>The <see cref="IResourceBuilder{T}"/>.</returns>
    public static IResourceBuilder<KeycloakResource> WithDataVolume(
        this IResourceBuilder<KeycloakResource> builder,
        string? name = null,
        bool isReadOnly = false
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.WithVolume(
            name ?? VolumeNameGenerator.Generate(builder, "data"),
            "/opt/keycloak/data",
            isReadOnly
        );
    }

    /// <summary>
    /// Pins the Keycloak server hostname so issued tokens contain a stable <c>iss</c> claim,
    /// regardless of which network identity (e.g. <c>localhost</c> vs. <c>host.docker.internal</c>)
    /// the Keycloak container is reached on.
    /// </summary>
    /// <remarks>
    /// Sets the <c>KC_HOSTNAME</c> environment variable. Use this when API consumers run inside
    /// containers and need the issuer to match a single, fixed URL.
    /// </remarks>
    /// <param name="builder">The Keycloak resource builder.</param>
    /// <param name="hostnameUrl">The hostname URL Keycloak should advertise (e.g. <c>http://localhost:8080/</c>).</param>
    public static IResourceBuilder<KeycloakResource> WithHostname(
        this IResourceBuilder<KeycloakResource> builder,
        string hostnameUrl
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(hostnameUrl);

        return builder.WithEnvironment("KC_HOSTNAME", hostnameUrl);
    }

    /// <summary>
    /// Configures the Keycloak container to use an external PostgreSQL database for persistence.
    /// </summary>
    /// <param name="builder">The Keycloak resource builder.</param>
    /// <param name="database">An Aspire database resource exposing a PostgreSQL connection string.</param>
    public static IResourceBuilder<KeycloakResource> WithPostgresDatabase(
        this IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database
    ) => WithDatabaseCore(builder, database, KeycloakDbVendor.Postgres);

    /// <summary>
    /// Configures the Keycloak container to use an external MySQL database for persistence.
    /// </summary>
    public static IResourceBuilder<KeycloakResource> WithMySqlDatabase(
        this IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database
    ) => WithDatabaseCore(builder, database, KeycloakDbVendor.MySql);

    /// <summary>
    /// Configures the Keycloak container to use an external MariaDB database for persistence.
    /// </summary>
    public static IResourceBuilder<KeycloakResource> WithMariaDbDatabase(
        this IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database
    ) => WithDatabaseCore(builder, database, KeycloakDbVendor.MariaDb);

    /// <summary>
    /// Configures the Keycloak container to use an external Microsoft SQL Server database for persistence.
    /// </summary>
    public static IResourceBuilder<KeycloakResource> WithMsSqlDatabase(
        this IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database
    ) => WithDatabaseCore(builder, database, KeycloakDbVendor.MsSql);

    /// <summary>
    /// Configures the Keycloak container to use an external Oracle database for persistence.
    /// </summary>
    public static IResourceBuilder<KeycloakResource> WithOracleDatabase(
        this IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database
    ) => WithDatabaseCore(builder, database, KeycloakDbVendor.Oracle);

    private static IResourceBuilder<KeycloakResource> WithDatabaseCore(
        IResourceBuilder<KeycloakResource> builder,
        IResourceBuilder<IResourceWithConnectionString> database,
        KeycloakDbVendor vendor
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(database);

        builder.WaitFor(database);

        return builder.WithEnvironment(async ctx =>
        {
            var connectionString = await database.Resource.GetConnectionStringAsync(
                ctx.CancellationToken
            );

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Database resource '{database.Resource.Name}' did not provide a connection string."
                );
            }

            var settings = KeycloakDbConnectionStringTranslator.Translate(vendor, connectionString);

            ctx.EnvironmentVariables["KC_DB"] = settings.KcDb;
            ctx.EnvironmentVariables["KC_DB_URL"] = settings.JdbcUrl;
            if (settings.Username is not null)
            {
                ctx.EnvironmentVariables["KC_DB_USERNAME"] = settings.Username;
            }
            if (settings.Password is not null)
            {
                ctx.EnvironmentVariables["KC_DB_PASSWORD"] = settings.Password;
            }
        });
    }

    /// <summary>
    /// Adds a reference to a Keycloak resource to the specified resource builder.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource builder.</typeparam>
    /// <param name="builder">The resource builder to add the reference to.</param>
    /// <param name="keycloakBuilder">The Keycloak resource builder to reference.</param>
    /// <param name="configurationPrefix"></param>
    /// <returns>The resource builder with the added reference.</returns>
    public static IResourceBuilder<TResource> WithReference<TResource>(
        this IResourceBuilder<TResource> builder,
        IResourceBuilder<KeycloakRealmResource> keycloakBuilder,
        string configurationPrefix = "Keycloak"
    )
        where TResource : IResourceWithEnvironment
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(keycloakBuilder);

        builder.WithEnvironment(
            $"{configurationPrefix}__AuthServerUrl",
            keycloakBuilder.Resource.Parent.PrimaryEndpoint
        );

        builder.WithEnvironment($"{configurationPrefix}__Realm", keycloakBuilder.Resource.Realm);

        return builder;
    }
}

internal static class KeycloakContainerImageTags
{
    public const string Registry = "quay.io";
    public const string Image = "keycloak/keycloak";
    public const string Tag = "26.6.1";
}
