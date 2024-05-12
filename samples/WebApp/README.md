# An ASP.NET Core Web app signing-in users with Keycloak.AuthServices

## Scenario

This sample shows how to build a .NET Core MVC Web app that uses OpenID Connect to sign in users. In a typical cookie-based authentication scenario using OpenID Connect, the following components and flow are involved:

Components:

* User: The end-user who wants to authenticate.
* Client: The application that wants to authenticate the user (e.g., a web application).
* Authorization Server: The server that performs the authentication and issues tokens (e.g., Google, Facebook).
* Resource Server: The server hosting the protected resources that the client wants to access.

Authentication Flow:

* Step 1: The user accesses the client application and requests to log in.
* Step 2: The client redirects the user to the authorization server's login page, where the user enters their credentials.
* Step 3: Upon successful authentication, the authorization server redirects the user back to the client application with an authorization code.
* Step 4: The client exchanges the authorization code for an ID token and an access token at the authorization server's token endpoint.
* Step 5: The authorization server validates the authorization code, and if valid, issues the ID token and access token.
* Step 6: The client validates the ID token and retrieves the user's identity information.
* Step 7: The client creates a session for the user and stores the session identifier in a secure, HTTP-only cookie.
* Step 8: The user's subsequent requests to the client include the session cookie, which the client uses to identify the user and maintain the authenticated session.
* Step 9: If the client needs to access protected resources from a resource server, it can use the access token to authenticate the requests.

