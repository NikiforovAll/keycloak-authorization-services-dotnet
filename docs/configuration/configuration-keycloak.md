# Configure Keycloak

This section contains a general instruction of how to configure Keyclaok to be used for .NET applications.

*Table of Contents*:
[[toc]]

## Create Realm

1. Open the Keycloak admin console in your browser. The URL is usually `http://localhost:8080`.
2. Click on the `Select Realm` dropdown button on the top left corner of the page.
3. Click on `Add Realm`.
4. In the `Create Realm` form, enter the name of your realm in the `Name` field.
5. Click on `Create`.

You have now created a new realm in Keycloak.

## Create User

1. In the Keycloak admin console, select your newly created realm.
2. In the left-hand menu, click on `Users`.
3. Click on `Add user`.
4. In the `Add user` form, fill in the required fields such as `Username`, `Email`, `FirstName`, `LastName`.
5. Click on `Save`.
You have now created a new user in your realm.

## Set Password

1. After creating a new user, click on the `Credentials` tab.
2. In the `Password` field, enter the new password.
3. Confirm the password in the `Password Confirmation` field.
4. Set the `Temporary` switch to `OFF` if you don't want the user to change their password at the next login.
5. Click on `Set Password`.
You have now set a password for the new user in your realm.

## Create Client

1. In the Keycloak admin console, select your realm.
2. In the left-hand menu, click on `Clients`.
3. Click on `Create`.
4. In the `Add Client` form, fill in the required fields such as `Client ID`, `Client Protocol`.
5. Click on `Save`.
You have now created a new client in your realm.

## Add Audience Mapper

Client Scopes in Keycloak are used to define a set of permissions that a client has. They are a way to limit the access of a client to certain resources or actions.

Mappers in Keycloak are used to map claims from the token to a user session and user profile. They can be used to add additional information to the token, such as user roles or other attributes.

1. In the Keycloak admin console, select your realm.
2. In the left-hand menu, click on `Clients` and selected required client.
3. Navigate to `Client Scopes`
4. Select `{client_id}-dedicated`, where client_id is the name of your client.
5. Click on the `Mappers` tab.
6. Click on `Configure a new mapper` and Select `Audience`
7. Specify the name of the mapper, e.g.: **Audience**
8. In the `Name` field, enter a name for the mapper.
9. In the `Included Client Audience` field, enter the client ID of the client you want to include in the audience.
10. Click on `Save`.
You have now added an audience mapper to a client scope in your realm.

## Download Adapter Config

1. In the Keycloak admin console, select your realm.
2. In the left-hand menu, click on `Clients`.
3. Select the client for which you want to download the adapter config.
4. Click on the `Action` dropdown on the top-right corner.
5. Click on `Download adapter config`.
You have now downloaded the adapter config for your client.

Note: *Instructions are provided for Keycloak of version 24.0.3*
