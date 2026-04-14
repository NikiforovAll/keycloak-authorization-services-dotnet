using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapLoginAndLogout(this RouteGroupBuilder group)
    {
        group.MapGet("/login", () => TypedResults.Challenge(new() { RedirectUri = "/" }));

        group.MapPost(
            "/logout",
            () =>
                TypedResults.SignOut(
                    new() { RedirectUri = "/" },
                    [
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        OpenIdConnectDefaults.AuthenticationScheme,
                    ]
                )
        );

        return group;
    }
}
