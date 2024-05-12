# WebApp MVC

<!--@include: @/../samples/WebApp/README.md-->

### Role Mapping

> [!WARNING]
> By default Keycloak doesn't map roles to **id_token**, so we need an **access_token** in this case, **access_token** is **NOT** available in all OAuth flows. For example, "Implicit Flow" is based on id_token and **access_token** is not retrieved at all.

## Code

<<< @/../samples/WebApp/Program.cs

<<< @/../samples/WebApp/Controllers/AccountController.cs

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/WebApp](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/WebApp)
