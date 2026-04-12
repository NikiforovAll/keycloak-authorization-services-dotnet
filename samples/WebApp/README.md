# An ASP.NET Core Web app signing-in users with Keycloak.AuthServices

## Scenario

This sample shows how to build a .NET Core MVC Web app that uses OpenID Connect to sign in users. It demonstrates:

- Cookie-based authentication via OIDC (`AddKeycloakWebAppAuthentication`)
- Authorization policies based on Keycloak realm roles
- **Cookie-based access token retrieval** — the `CookieAccessTokenProvider` is auto-detected at startup and retrieves the access token from the cookie session (stored via `SaveTokens = true`), enabling `[ProtectedResource]` and `IAuthorizationServerClient` to work in a Web App context without requiring a Bearer token in request headers.
- **Protected resources via Keycloak Authorization Server** — the `[ProtectedResource]` attribute triggers an RPT (Requesting Party Token) evaluation against Keycloak, demonstrating fine-grained resource-scope authorization.

### Authentication Flow

1. User accesses the app → redirected to Keycloak login page
2. User authenticates → Keycloak issues authorization code
3. App exchanges code for ID token + access token at Keycloak's token endpoint
4. App validates the ID token and creates a cookie session (`SaveTokens = true` persists the tokens in the cookie)
5. Subsequent requests use the session cookie — no Bearer header needed
6. When calling protected APIs, `CookieAccessTokenProvider` reads the access token from the cookie

## Getting Started

### 1. Start Keycloak

```bash
docker compose up -d
```

This starts Keycloak at `http://localhost:8080` and automatically imports the `Test` realm with a pre-configured `test-client` and test users.

### 2. Run the application

```bash
dotnet run
```

Or press **F5** in Visual Studio / Rider. The app listens on `https://localhost:44321`.

### 3. Test users

| Username | Password | Realm Role | Can access           |
|----------|----------|------------|----------------------|
| `admin`  | `test`   | `Admin`    | All pages            |
| `user`   | `test`   | _(none)_   | Home/Index only      |

### 4. Try the flows

| Page | URL | Access |
|------|-----|--------|
| Public | `/Home/Public` | Anyone |
| Home (requires login) | `/` | Any authenticated user |
| Privacy (requires Admin role) | `/Home/Privacy` | `admin` only |
| Workspaces list (RPT: `workspace:list`) | `/Workspaces` | `admin` only |
| Workspace details (RPT: `workspace:read`) | `/Workspaces/Details/1` | `admin` only |
| Sign in | `/Account/SignIn` | Redirects to Keycloak |
| Sign out | `/Account/SignOut` | Ends session and redirects |

Log in as `user` and navigate to **Privacy** or **Workspaces** — you'll see the Access Denied page.
Log in as `admin` and navigate to **Privacy** or **Workspaces** — access is granted.

The Workspaces pages use `[ProtectedResource("workspace", "workspace:list")]` / `[ProtectedResource("workspace", "workspace:read")]`
which cause the middleware to call Keycloak's Authorization Server, exchange the cookie access token for an RPT, and evaluate the resource policies before granting access.

## Keycloak Configuration

The realm is imported automatically from `KeycloakConfiguration/`:

| File | Contents |
|------|----------|
| `Test-realm.json` | Realm settings, `test-client` (confidential, authorization services enabled), `Admin` role, `workspace` resource with `workspace:list`/`workspace:read` scopes and role-based policies |
| `Test-users-0.json` | Test users with plaintext passwords (dev only) |

To export updated realm config from inside the running container:

```bash
docker exec -it <container-id> /opt/keycloak/bin/kc.sh export --dir /opt/keycloak/data/import --realm Test
```

## Keycloak Admin Console

Open `http://localhost:8080` and log in with `admin` / `admin`.

