# Getting Started 🚀

In this guide, we will walk through the process of using **Keycloak.AuthServices.Authentication** to secure a Web API. 

We will be setting up a .NET Core web API project and use Keycloak to handle authentication.

---

Create an empty web project:

```bash
dotnet new web -n GettingStarted
```

Install **Keycloak.AuthServices.Authentication** by running:

```bash
dotnet add package Keycloak.AuthServices.Authentication
dotnet add package Keycloak.AuthServices.Common
```

Replace the content of **Program.cs** with:

```csharp
using Keycloak.AuthServices.Authentication; // [!code focus]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration); // [!code focus]
builder.Services.AddAuthorization(); // [!code focus]

var app = builder.Build();

app.UseAuthentication(); // [!code focus]
app.UseAuthorization(); // [!code focus]

app.MapGet("/", () => "Hello World!").RequireAuthorization(); // [!code focus]

app.Run();
```

> [!TIP]
> 💡 For more detailed explanation of how to configure *Authentication* visit [Configuration/Authentication](/configuration/configuration-authentication)

## Configure Keycloak

Run docker image locally, as described in the [documentation](https://www.keycloak.org/getting-started/getting-started-docker).

```bash
docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.4.2 start-dev
```

For Keycloak configuration instructions see: [Keycloak Configuration](/configuration/configuration-keycloak)

Here is high level overview of what we need to do to configure Keycloak for our demo:

1. Login with **admin:admin**
2. [Create Realm](/configuration/configuration-keycloak#create-realm)
   1. Name: **Test**
3. [Create a User](/configuration/configuration-keycloak#create-user)
   1. Login: **Test**
   2. First Name: **Test**
   3. Last Name: **Test**
   4. Email: **test@test.com**
4. [Set Password](/configuration/configuration-keycloak#set-password)
   1. Password: **test**
5. [Create a Client](/configuration/configuration-keycloak#create-client):
   1. Name: **test-client**
   2. Configure Client authentication - **On** (We will need it in the future to get access token via username:password)
6. [Configure the Audience Mapper](/configuration/configuration-keycloak#add-audience-mapper)
   1. ❗This is important, by default **Keycloak.AuthServices.Authentication** assume that the name of resource is the intended Audience / client. Otherwise, you will get **401** status code. 
   2. Alternatively, you can specify `KeycloakAuthenticationOptions.VerifyTokenAudience=false`.

## Configure API

[Download adapter config](/configuration/configuration-keycloak#download-adapter-config) so we can use it for seamless integration with Keycloak. All you need to do is to add the content of Adapter Config to the **appsettings.Development.json** "Keycloak" section like this:

```jsonc
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Keycloak": { // [!code ++]
    "realm": "Test", // [!code ++]
    "auth-server-url": "http://localhost:8080/", // [!code ++]
    "ssl-required": "none", // [!code ++]
    "resource": "test-client", // [!code ++]
    "credentials": { // [!code ++]
      "secret": "IMoSbQIplDPmagMU6ZCb8DkQMkkZ0OiI" // [!code ++]
    }, // [!code ++]
    "confidential-port": 0 // [!code ++]
  } // [!code ++]
}
```

## Demo

Run the application:

```bash
dotnet run
# Building...
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5228
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
# info: Microsoft.Hosting.Lifetime[0]
#       Hosting environment: Development
# info: Microsoft.Hosting.Lifetime[0]
#       Content root path: C:\Users\Oleksii_Nikiforov\dev\keycloak-authorization-services-dotnet\samples\GettingStarted
```

Run HTTP request without token:

```bash
curl http://localhost:5228 -v
# *   Trying 127.0.0.1:5228...
# * Connected to localhost (127.0.0.1) port 5228 (#0)
# > GET / HTTP/1.1
# > Host: localhost:5228
# > User-Agent: curl/8.1.2
# > Accept: */*
# >
# < HTTP/1.1 401 Unauthorized
# < Content-Length: 0
# < Date: Thu, 25 Apr 2024 15:04:41 GMT
# < Server: Kestrel
# < WWW-Authenticate: Bearer
# <
# * Connection #0 to host localhost left intact
```

Request a token based on [Resource Owner Flow](https://www.rfc-editor.org/rfc/rfc6749#page-9) (we enabled it for the client previously):

```bash
curl --data "grant_type=password&client_id=test-client&username=test&password=test&client_secret=IMoSbQIplDPmagMU6ZCb8DkQMkkZ0OiI&scope=roles" \
    http://localhost:8080/realms/Test/protocol/openid-connect/token
# {"access_token":"eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJrUkdGSFpGZWNaU0VHWV9mOERvc1YxSWpEY01VaFpIczZ4YXlhTks5Uk5zIn0.eyJleHAiOjE3MTQwNTc1MDQsImlhdCI6MTcxNDA1NzIwNCwianRpIjoiNzI1MGQyYTktZTVhMS00NDJmLTllNzYtNWU2Yjc4YmIyNzYwIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9UZXN0IiwiYXVkIjpbInRlc3QtY2xpZW50IiwiYWNjb3VudCJdLCJzdWIiOiJiZjBiMzM3MS1jY2RjLTQ0ZjYtODg2MS1jZTI1Y2JmY2FjMzkiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJ0ZXN0LWNsaWVudCIsInNlc3Npb25fc3RhdGUiOiI1NjMzMzJkMi0xMTFhLTRlZjItYjZhMC1lYmMxZDNhZTlhMWUiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIi8qIl0sInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJkZWZhdWx0LXJvbGVzLXRlc3QiLCJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIl19LCJyZXNvdXJjZV9hY2Nlc3MiOnsiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19fSwic2NvcGUiOiJwcm9maWxlIGVtYWlsIiwic2lkIjoiNTYzMzMyZDItMTExYS00ZWYyLWI2YTAtZWJjMWQzYWU5YTFlIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJuYW1lIjoiVGVzdCBUZXN0IiwicHJlZmVycmVkX3VzZXJuYW1lIjoidGVzdCIsImdpdmVuX25hbWUiOiJUZXN0IiwiZmFtaWx5X25hbWUiOiJUZXN0IiwiZW1haWwiOiJ0ZXN0QHRlc3QuY29tIn0.n2juqBgFtDtxErYMp5iGzFzeE6MeXVzyXUoAd9B4pxvLp8CDEE2OnbnjGfhwlrB70F6KCSxDfHv4HxKPKxWQ6KrPftKWwAOPMY7gGcVmlr0-vek8-bEWxnN2cSRY2mTeHMby4xlppEeq4hK1pBnL5YETn3WnXZ0GkRw7QDODhkcAgKmJUs5yw0Fit0dhq-TCMF9K1lBl-oac3nXBfzIYjZjzqNN9DZWoJMio2movlomsKKl57JrOUpaGTc7GlId4FSLqS0Kdh1gqqme2UG2ErvEs-wROIGfObLX-lL4gFoUW5sCCppUaZN6IkzJhebBK85iiTCUYmAsJruPWX2P_hw","expires_in":300,"refresh_expires_in":1800,"refresh_token":"eyJhbGciOiJIUzUxMiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI2NmQ1ZjZlNi1iMDQ1LTQzOWUtYjNiZC02ZDdjMWVhNzBiN2MifQ.eyJleHAiOjE3MTQwNTkwMDQsImlhdCI6MTcxNDA1NzIwNCwianRpIjoiNDI2MGY2MTEtY2VmZi00ODE2LWFjM2QtZjBmNGRlZTVkMzRlIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9UZXN0IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9UZXN0Iiwic3ViIjoiYmYwYjMzNzEtY2NkYy00NGY2LTg4NjEtY2UyNWNiZmNhYzM5IiwidHlwIjoiUmVmcmVzaCIsImF6cCI6InRlc3QtY2xpZW50Iiwic2Vzc2lvbl9zdGF0ZSI6IjU2MzMzMmQyLTExMWEtNGVmMi1iNmEwLWViYzFkM2FlOWExZSIsInNjb3BlIjoicHJvZmlsZSBlbWFpbCIsInNpZCI6IjU2MzMzMmQyLTExMWEtNGVmMi1iNmEwLWViYzFkM2FlOWExZSJ9.psZvxBRXsG7CbrV27R7OgcoJVGcgy0yiufUaDKQipwOz_lBI0S3OFEo6EU-JjOlCyIqNvRwXOGJw0BxfBObyuQ","token_type":"Bearer","not-before-policy":0,"session_state":"563332d2-111a-4ef2-b6a0-ebc1d3ae9a1e","scope":"profile email"}
```

```bash
curl -H "Authorization: Bearer YOUR_ACCESS_TOKEN" http://localhost:5228/
# *   Trying 127.0.0.1:5228...
# * Connected to localhost (127.0.0.1) port 5228 (#0)
# > GET / HTTP/1.1
# > Host: localhost:5228
# > User-Agent: curl/8.1.2
# > Accept: */*
# > Authorization: Bearer YOUR_ACCESS_TOKEN
# >
# < HTTP/1.1 200 OK
# < Content-Type: text/plain; charset=utf-8
# < Date: Thu, 25 Apr 2024 15:13:36 GMT
# < Server: Kestrel
# < Transfer-Encoding: chunked
# <
# Hello World!* Connection #0 to host localhost left intact
```

🙌 Hooray, congratulations! You know how to configure Web API Authentication we Keycloak using **Keycloak.AuthServices.Authentication**.

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/GettingStarted](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/GettingStarted)
