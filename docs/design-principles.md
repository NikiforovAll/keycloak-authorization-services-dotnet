# Design V2

## Goals

### Minimum dependencies

Currently, we use Refit for client generation, but it is completely unnecessary because it add dependency on `Refit` and therefore the clients are taking this dependency also. Retrospectively, it was bad decision

### Composable and Open For Extension

* Composable API that allows to configuration Keycloak integration for Web API scenarios.
  * It should be possible to configure pretty much every aspect of Keycloak integration, meaning we should avoid static "variables and sealed settings"
